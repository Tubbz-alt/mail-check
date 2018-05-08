﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dmarc.Common.Data;
using Dmarc.Common.Interface.Tls.Domain;
using Dmarc.DomainStatus.Api.Domain;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;

namespace Dmarc.DomainStatus.Api.Dao.DomainStatus
{
    public interface IDomainStatusDao
    {
        Task<Domain.Domain> GetDomain(int id);

        Task<DomainTlsEvaluatorResults> GetDomainTlsEvaluatorResults(int id);

        Task<string> GetSpfReadModel(int id);

        Task<DmarcReadModel> GetDmarcReadModel(int id);

        Task<DmarcReadModel> GetDmarcReadModel(string domainName);

        Task<int> GetAggregateReportTotalEmailCount(int domainId, DateTime startDate, DateTime endDate);

        Task<SortedDictionary<DateTime, AggregateSummaryItem>> GetAggregateReportSummary(int domainId, DateTime startDate, DateTime endDate);

        Task<List<AggregateReportExportItem>> GetAggregateReportExport(int domainId, DateTime date);
    }

    internal class DomainStatusDao : IDomainStatusDao
    {
        private readonly IConnectionInfoAsync _connectionInfo;
        private readonly ILogger<DomainStatusDao> _log;

        public DomainStatusDao(IConnectionInfoAsync connectionInfo, ILogger<DomainStatusDao> log)
        {
            _connectionInfo = connectionInfo;
            _log = log;
        }

        public Task<Domain.Domain> GetDomain(int id)
        {
            return Db.ExecuteReaderSingleResultTimed(_connectionInfo, DomainStatusDaoResources.SelectDomainById,
                _ => _.AddWithValue("domainId", id), CreateDomain, _ => _log.LogDebug(_), nameof(GetDomain));
        }

        public Task<DomainTlsEvaluatorResults> GetDomainTlsEvaluatorResults(int id)
        {
            Func<DbDataReader, Task<DomainTlsEvaluatorResults>> createTlsResults = _ => CreateDomainTlsEvaluatorResults(id, _);

            return Db.ExecuteReaderTimed(_connectionInfo, DomainStatusDaoResources.SelectTlsEvaluatorResults,
                _ => _.AddWithValue("domainId", id), createTlsResults, _ => _log.LogDebug(_), nameof(GetDomainTlsEvaluatorResults));
        }

        public Task<string> GetSpfReadModel(int id)
        {
            return Db.ExecuteScalarTimed<string>(_connectionInfo, DomainStatusDaoResources.SelectSpfReadModel,
                _ => _.AddWithValue("domainId", id), _ => _log.LogDebug(_), nameof(GetSpfReadModel));
        }

        public Task<DmarcReadModel> GetDmarcReadModel(int id)
        {
            return Db.ExecuteReaderSingleResultTimed(_connectionInfo, DomainStatusDaoResources.SelectDmarcReadModelByDomainId,
                _ => _.AddWithValue("domainId", id), CreateDmarcReadModel, _ => _log.LogDebug(_), nameof(GetDmarcReadModel));
        }

        public Task<DmarcReadModel> GetDmarcReadModel(string domainName)
        {
            return Db.ExecuteReaderSingleResultTimed(_connectionInfo, DomainStatusDaoResources.SelectDmarcReadModelByDomainName,
               _ => _.AddWithValue("domainName", domainName), CreateDmarcReadModel, _ => _log.LogDebug(_), nameof(GetDmarcReadModel));
        }

        public Task<int> GetAggregateReportTotalEmailCount(int domainId, DateTime startDate, DateTime endDate)
        {
            Action<MySqlParameterCollection> addParameters = parameterCollection =>
            {
                parameterCollection.AddWithValue("domainId", domainId);
                parameterCollection.AddWithValue("startDate", startDate.ToString("yyyy-MM-dd"));
                parameterCollection.AddWithValue("endDate", endDate.ToString("yyyy-MM-dd"));
            };

            return Db.ExecuteScalarTimed(_connectionInfo, DomainStatusDaoResources.SelectAggregateReportTotalEmailCount,
               addParameters, _ => _ == DBNull.Value ? 0 : (int)(decimal)_, _ => _log.LogDebug(_), nameof(GetAggregateReportTotalEmailCount));
        }

        public Task<SortedDictionary<DateTime, AggregateSummaryItem>> GetAggregateReportSummary(int domainId, DateTime startDate, DateTime endDate)
        {
            Action<MySqlParameterCollection> addParameters = parameterCollection =>
            {
                parameterCollection.AddWithValue("domainId", domainId);
                parameterCollection.AddWithValue("startDate", startDate.ToString("yyyy-MM-dd"));
                parameterCollection.AddWithValue("endDate", endDate.ToString("yyyy-MM-dd"));
            };

            return Db.ExecuteReaderTimed(_connectionInfo, DomainStatusDaoResources.SelectAggregateReportSummary,
                addParameters, CreateAggegateReportSummary, _ => _log.LogDebug(_), nameof(GetAggregateReportSummary));
        }

        public Task<List<AggregateReportExportItem>> GetAggregateReportExport(int domainId, DateTime date)
        {
            Action<MySqlParameterCollection> addParameters = parameterCollection =>
            {
                parameterCollection.AddWithValue("domainId", domainId);
                parameterCollection.AddWithValue("date", date.ToString("yyyy-MM-dd"));
            };

            return Db.ExecuteReaderListResultTimed(_connectionInfo, DomainStatusDaoResources.SelectAggregateExportData, addParameters,
                CreateAggregateReportExportItem, _ => _log.LogDebug(_), nameof(GetAggregateReportExport));
        }

        private DmarcReadModel CreateDmarcReadModel(DbDataReader reader)
        {
            return new DmarcReadModel(
                CreateDomain(reader),
                reader.GetBoolean("has_dmarc"),
                reader.GetString("read_model"));
        }

        private Domain.Domain CreateDomain(DbDataReader reader)
        {
            return new Domain.Domain(
                reader.GetInt32("domain_id"),
                reader.GetString("domain_name"));
        }

        private async Task<DomainTlsEvaluatorResults> CreateDomainTlsEvaluatorResults(int id, DbDataReader reader)
        {
            if (!reader.HasRows)
            {
                return new DomainTlsEvaluatorResults(id , pending: true);
            }

            List<MxTlsEvaluatorResults> mxTlsEvaluatorResults = new List<MxTlsEvaluatorResults>();
            string domainHost = null;

            while (await reader.ReadAsync())
            {
                domainHost = domainHost ?? reader.GetString("name");

                var results = GetTlsEvaluatorResults(reader);

                if (!results.All(_ => _.Result == null && _.Description == null))
                {
                    mxTlsEvaluatorResults.Add(
                    new MxTlsEvaluatorResults(
                        reader.GetInt32("mx_record_id"),
                        reader.GetString("hostname"),
                        results.Where(_ => _.Result == EvaluatorResult.WARNING).Select(_ => _.Description).ToList(),
                        results.Where(_ => _.Result == EvaluatorResult.FAIL).Select(_ => _.Description).ToList(),
                        results.Where(_ => _.Result == EvaluatorResult.INCONCLUSIVE).Select(_ => _.Description).ToList()
                    ));
                }
            }

            return new DomainTlsEvaluatorResults(id, domainHost, mxTlsEvaluatorResults);
        }

        private List<TlsEvaluatorResult> GetTlsEvaluatorResults(DbDataReader reader)
        {
            var results = new List<TlsEvaluatorResult>();

            for (int i = 1; i <= 13; i++)
            {
                results.Add(new TlsEvaluatorResult((EvaluatorResult?)reader.GetUInt16Nullable($"test{i}_result"), reader.GetString($"test{i}_description")));
            }

            return results;
        }

        private async Task<SortedDictionary<DateTime, AggregateSummaryItem>> CreateAggegateReportSummary(DbDataReader reader)
        {
            SortedDictionary<DateTime, AggregateSummaryItem> results = new SortedDictionary<DateTime, AggregateSummaryItem>();
            while (await reader.ReadAsync())
            {
                results.Add(reader.GetDateTime("effective_date"), CreateAggregateInfoItem(reader));
            }
            return results;
        }

        private AggregateSummaryItem CreateAggregateInfoItem(DbDataReader reader)
        {
            return new AggregateSummaryItem(
                reader.GetInt32("fully_trusted"),
                reader.GetInt32("partially_trusted"),
                reader.GetInt32("untrusted"),
                reader.GetInt32("quarantined"),
                reader.GetInt32("rejected")
                );
        }

        private AggregateReportExportItem CreateAggregateReportExportItem(DbDataReader reader)
        {
            return new AggregateReportExportItem(
                reader.GetString("header_from"),
                reader.GetString("source_ip"),
                reader.GetString("ptr"),
                reader.GetInt32("count"),
                reader.GetString("spf"),
                reader.GetString("dkim"),
                reader.GetString("disposition"),
                reader.GetString("org_name"),
                reader.GetDateTime("effective_date"));
        }
    }
}

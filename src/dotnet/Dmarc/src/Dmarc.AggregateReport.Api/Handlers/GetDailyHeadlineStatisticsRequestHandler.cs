using System.Threading.Tasks;
using Dmarc.AggregateReport.Api.Dao;
using Dmarc.AggregateReport.Api.Dao.Entities;
using Dmarc.AggregateReport.Api.Messages;
using Dmarc.AggregateReport.Api.Messages.Factory;
using Dmarc.AggregateReport.Api.Validation;
using Dmarc.Common.Logging;

namespace Dmarc.AggregateReport.Api.Handlers
{
    internal interface IGetDailyHeadlineStatisticsRequestHandler : IRequestHandler { }

    internal class GetDailyHeadlineStatisticsRequestHandler : DateRangeDomainRequestHandler, IGetDailyHeadlineStatisticsRequestHandler
    {
        public GetDailyHeadlineStatisticsRequestHandler(ILogger log,
            IValidator<DateRangeDomainRequest> dateRangeDomainRequestValidator,
            IDateRangeDomainRequestFactory dateRangeDomainRequestFactory,
            IAggregateReportApiDao aggregateReportApiDao)
            : base(log, dateRangeDomainRequestValidator, dateRangeDomainRequestFactory, aggregateReportApiDao)
        {
        }

        protected override async Task<Response> CreateInternalResponseAsync(
            DateRangeDomainRequest request)
        {
            DailyStatistics dailyStatistics = await AggregateReportApiDao
                .GetDailyHeadlineStatisticsAsync(request.BeginDateUtc.Value, request.EndDateUtc.Value,
                    request.DomainId);
            return new DailyStatisticsResponse(dailyStatistics.Values);
        }
    }
}
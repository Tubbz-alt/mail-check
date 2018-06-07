﻿using Amazon.SimpleSystemsManagement;
using Dmarc.Common.Api.Handlers;
using Dmarc.Common.Api.Identity.Dao;
using Dmarc.Common.Api.Identity.Domain;
using Dmarc.Common.Api.Identity.Middleware;
using Dmarc.Common.Api.Middleware;
using Dmarc.Common.Data;
using Dmarc.Common.Encryption;
using Dmarc.Common.Interface.PublicSuffix;
using Dmarc.Common.PublicSuffix;
using Dmarc.Common.Validation;
using Dmarc.DomainStatus.Api.Config;
using Dmarc.DomainStatus.Api.Dao.DomainStatus;
using Dmarc.DomainStatus.Api.Dao.DomainStatusList;
using Dmarc.DomainStatus.Api.Dao.Permission;
using Dmarc.DomainStatus.Api.Domain;
using Dmarc.DomainStatus.Api.Services;
using Dmarc.DomainStatus.Api.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dmarc.DomainStatus.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath);

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public static IConfigurationRoot Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            HttpClient httpClient = new HttpClient();

            services.AddHealthChecks(checks =>
                {
                    checks.AddValueTaskCheck("HTTP Endpoint", () => new ValueTask<IHealthCheckResult>(HealthCheckResult.Healthy("Ok")));
                })
            .AddTransient<IPersistanceConnectionTester, DatabaseConnectionTester>()
            .AddTransient<IValidator<DomainRequest>, DomainRequestValidator>()
            .AddTransient<IValidator<DomainsRequest>, DomainsRequestValidator>()
            .AddTransient<IValidator<DateRangeDomainRequest>, DateRangeDomainRequestValidator>()
            .AddTransient<IDomainStatusDao, DomainStatusDao>()
            .AddTransient<IDomainStatusListDao, DomainStatusListDao>()
            .AddTransient<IConnectionInfo>(p => new StringConnectionInfo(Environment.GetEnvironmentVariable("ConnectionString")))
            .AddTransient<IParameterStoreRequest, ParameterStoreRequest>()
            .AddTransient<IAmazonSimpleSystemsManagement>(p => new AmazonSimpleSystemsManagementClient())
            .AddTransient<IConnectionInfoAsync, ConnectionInfoAsync>()
            .AddTransient<IDomainValidator, DomainValidator>()
            .AddTransient<IIdentityDao, IdentityDao>()
            .AddTransient<IPermissionDao, PermissionDao>()
            .AddTransient<IReverseDnsApi, ReverseDnsApi>()
            .AddTransient<IReverseDnsApiConfig, ReverseDnsApiConfig>()
            .AddSingleton<HttpClient>(httpClient)
            .AddSingleton<IOrganisationalDomainProvider, OrganisationDomainProvider>()
            .AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            })
            .AddAuthorization(options =>
            {
                options.AddPolicy(PolicyType.Admin, policy => policy.RequireAssertion(context => context.User.Claims.Any(_ => _.Type == ClaimTypes.Role && _.Value == RoleType.Admin)));
            })
            .AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole((st, logLevel) => logLevel >= LogLevel.Debug);

            app.UseMiddleware<IdentityMiddleware>()
                .UseMiddleware<UnhandledExceptionLoggingMiddleware>()
                .UseCors("CorsPolicy")
                .UseMvc();
        }
    }
}

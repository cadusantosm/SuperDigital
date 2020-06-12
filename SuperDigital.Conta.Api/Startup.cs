using App.Metrics;
using App.Metrics.Extensions.Configuration;
using App.Metrics.Formatters.Prometheus;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SuperDigital.Conta.Api.Middlewares;
using SuperDigital.Conta.Applicacao;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation.AspNetCore;
using SuperDigital.Conta.Api.Validadores;

namespace SuperDigital.Conta.Api
{
    public class Startup
    {
        public readonly IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationServices(Configuration);

            services
                .AddControllers()
                .AddControllersAsServices()
                .AddFluentValidation()
                .AddMetrics();

            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });


            services.AddValidators();

            services.AddHttpContextAccessor();

            var secret = Encoding.ASCII.GetBytes(Configuration["Authentication:Secret"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors();

            services.AddHealthChecks();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "SuperDigital Conta Corrente Api", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT via header usando bearer scheme. Exemplo: \"bearer {token}\"",
                    In = ParameterLocation.Header,
                    Name = "Autorizacao",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration.ReadFrom(Configuration)
                .OutputMetrics.AsPrometheusPlainText()
                .Build();

            services.AddMetrics(metrics);
            services.AddMetricsEndpoints(endpointsOptions => endpointsOptions.MetricsEndpointOutputFormatter = endpointsOptions.MetricsOutputFormatters.OfType<MetricsPrometheusTextOutputFormatter>().First());
            services.AddMetricsReportingHostedService();
            services.AddMetricsTrackingMiddleware(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseRouting();

            app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseMetricsEndpoint();
            app.UseMetricsAllMiddleware();
            app.UseErrorHandlingMiddleware();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "/internal/swagger/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "internal/swagger";
                c.SwaggerEndpoint("/internal/swagger/v1/swagger.json", "SuperDigital Conta Corrente Api v1");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

                endpoints.MapHealthChecks("/internal/healthcheck");
            });
        }
    }
}

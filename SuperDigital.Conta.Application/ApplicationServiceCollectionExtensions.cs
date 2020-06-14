using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuperDigital.Conta.Applicacao.Servicos;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Dominio.Servicos;
using SuperDigital.Conta.Infraestrutura.DocumentStore;
using SuperDigital.Conta.Infraestrutura.Repositorios;
using System;
using System.Linq;

namespace SuperDigital.Conta.Applicacao
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            var superDigitalAssemblies = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(a => a.FullName?.StartsWith("SuperDigital") == true)
                .ToArray();

            services.AddSingleton<IDocumentStoreResolver>(_ =>
                new DocumentStoreResolver(
                    configuration.GetConnectionString("Readable"),
                    configuration.GetConnectionString("Writable")));

            services.AddSingleton<IContaCorrenteRepositorio, ContaCorrenteRepositorio>();
            services.AddSingleton<ILancamentoRepositorio, LancamentoRepositorio>();
            services.AddScoped<ILancamentoServico, LancamentoServico>();
            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();

            services.AddAutoMapper(superDigitalAssemblies);
            services.AddMediatR(superDigitalAssemblies);
            services.AddMediatorAspNetCoreLogging();
            services.AddMediatorMetrics();

            return services;
        }
    }
}

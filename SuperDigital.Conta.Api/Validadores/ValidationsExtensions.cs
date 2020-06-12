using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SuperDigital.Conta.Api.Contracts.Autenticar;
using SuperDigital.Conta.Api.Contracts.CriarCliente;
using SuperDigital.Conta.Api.Contracts.CriarConta;
using SuperDigital.Conta.Api.Contracts.CriarLancamento;

namespace SuperDigital.Conta.Api.Validadores
{
    public static class ValidationsExtensions
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<AutenticarClienteParameters>, PostAutenticarClienteParametersValidation>();
            services.AddTransient<IValidator<CriarClienteParameters>, PostCriarClienteParametersValidation>();
            services.AddTransient<IValidator<CriarContaCorrenteParameters>, PostCriarContaCorrenteParametersValidation>();
            services.AddTransient<IValidator<CriarLancamentoParameters>, PostCriarLancamentoParametersValidation>();


            return services;
        }
    }
}

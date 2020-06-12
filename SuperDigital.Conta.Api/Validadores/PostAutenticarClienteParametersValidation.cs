using FluentValidation;
using SuperDigital.Conta.Api.Contracts.Autenticar;
using SuperDigital.Conta.Infraestrutura.Util;

namespace SuperDigital.Conta.Api.Validadores
{
    public class PostAutenticarClienteParametersValidation : AbstractValidator<AutenticarClienteParameters>
    {
        public PostAutenticarClienteParametersValidation()
        {

            RuleFor(x => x.Documento)
                .NotEmpty()
                .WithMessage("Documento (CPF) é obrigatório.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatório.");

            RuleFor(r => r.Documento)
                .Must(StringExtensions.IsCpf)
                .WithMessage("Documento não é válido, deve ser um CPF.");
        }
    }
}

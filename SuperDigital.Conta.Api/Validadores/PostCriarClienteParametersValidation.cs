using FluentValidation;
using SuperDigital.Conta.Api.Contracts.CriarCliente;
using SuperDigital.Conta.Infraestrutura.Util;

namespace SuperDigital.Conta.Api.Validadores
{
    public class PostCriarClienteParametersValidation : AbstractValidator<CriarClienteParameters>
    {
        public PostCriarClienteParametersValidation()
        {

            RuleFor(x => x.Documento)
                .NotEmpty()
                .WithMessage("Documento (CPF) é obrigatório.");

            RuleFor(r => r.Documento)
                .Must(StringExtensions.IsCpf)
                .WithMessage("Documento não é válido, deve ser um CPF.");

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage("Senha é obrigatório.");

            RuleFor(x => x.Senha)
                .MinimumLength(8)
                .MaximumLength(8)
                .WithMessage("Senha deve conter 8 caracteres.");
            
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.");
            
           
        }
    }
}

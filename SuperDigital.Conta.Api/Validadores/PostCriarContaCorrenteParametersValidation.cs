using FluentValidation;
using SuperDigital.Conta.Api.Contracts.CriarConta;
using SuperDigital.Conta.Infraestrutura.Util;
using static System.Int32;

namespace SuperDigital.Conta.Api.Validadores
{
    public class PostCriarContaCorrenteParametersValidation : AbstractValidator<CriarContaCorrenteParameters>
    {
        public PostCriarContaCorrenteParametersValidation()
        {
            RuleFor(x => x.Agencia)
                .NotEmpty()
                .WithMessage("Número da agência é obrigatório.");

            RuleFor(x => x.Agencia)
                .Must(BeANumber)
                .WithMessage("Número da agência deve conter apenas números.");

            RuleFor(x => x.Agencia)
                .MaximumLength(5)
                .WithMessage("Número da agência deve conter até 5 números");

            RuleFor(x => x.NumeroConta)
                .NotEmpty()
                .WithMessage("Número da conta é obrigatório.");

            RuleFor(x => x.NumeroConta)
                .Must(BeANumber)
                .WithMessage("Número da conta deve conter apenas números.");

            RuleFor(x => x.NumeroConta)
                .MaximumLength(13)
                .WithMessage("Número da conta deve conter até 13 números.");

            RuleFor(x => x.NumeroConta)
                .MinimumLength(5)
                .WithMessage("Número da conta deve conter no mínimo 5 números.");

            RuleFor(x => x.DigitoVerificadorConta)
                .LessThan(99)
                .WithMessage("Digíto da conta deve conter apenas dois números.");

            RuleFor(x => x.DigitoVerificadorConta)
                .GreaterThan(0)
                .WithMessage("Digíto da conta deve ser informado.");

            RuleFor(x => x.DigitoVerificadorConta)
                .GreaterThan(0)
                .WithMessage("Digíto da conta deve ser informado.");

            RuleFor(x => x.NumeroDocumento)
                .NotEmpty()
                .WithMessage("Documento (CPF) é obrigatório.");

            RuleFor(r => r.NumeroDocumento)
                .Must(StringExtensions.IsCpf)
                .WithMessage("Documento não é válido, deve ser um CPF.");

            RuleFor(r => r.NomeLegal)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.");

            RuleFor(r => r.Valor)
                .GreaterThan(0)
                .WithMessage("É preciso fazer um primeiro depósito para abrir a conta.");

        }

        private bool BeANumber(string value)
        {
            return TryParse(value, out _);
        }
    }
}

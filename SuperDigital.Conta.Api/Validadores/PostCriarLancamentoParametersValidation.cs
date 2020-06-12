using FluentValidation;
using SuperDigital.Conta.Api.Contracts.CriarLancamento;
using SuperDigital.Conta.Infraestrutura.Util;
using static System.Int32;


namespace SuperDigital.Conta.Api.Validadores
{
    public class PostCriarLancamentoParametersValidation : AbstractValidator<CriarLancamentoParameters>
    {
        public PostCriarLancamentoParametersValidation()
        {
            
            
            RuleFor(x => x.AgenciaFavorecido)
                .NotEmpty()
                .WithMessage("Número da agência do favorecido é obrigatório.");

            RuleFor(x => x.AgenciaFavorecido)
                .Must(BeANumber)
                .WithMessage("Número da agência do favorecido deve conter apenas números.");

            RuleFor(x => x.AgenciaFavorecido)
                .MaximumLength(5)
                .WithMessage("Número da agência do favorecido deve conter até 5 números");

            RuleFor(x => x.NumeroContaFavorecido)
                .NotEmpty()
                .WithMessage("Número da conta do favorecido é obrigatório.");

            RuleFor(x => x.NumeroContaFavorecido)
                .Must(BeANumber)
                .WithMessage("Número da conta do favorecido deve conter apenas números.");

            RuleFor(x => x.NumeroContaFavorecido)
                .MaximumLength(13)
                .WithMessage("Número da conta do favorecido deve conter até 13 números.");

            RuleFor(x => x.NumeroContaFavorecido)
                .MinimumLength(5)
                .WithMessage("Número da conta do favorecido deve conter no mínimo 5 números.");

            RuleFor(x => x.DigitoVerificadorFavorecido)
                .LessThan(99)
                .WithMessage("Digíto da conta do favorecido deve conter apenas dois números.");

            RuleFor(x => x.DigitoVerificadorFavorecido)
                .GreaterThan(0)
                .WithMessage("Digíto da conta do favorecido deve ser informado.");

            RuleFor(x => x.DigitoVerificadorFavorecido)
                .GreaterThan(0)
                .WithMessage("Digíto da conta do favorecido deve ser informado.");

            RuleFor(x => x.NumeroDocumentoFavorecido)
                .NotEmpty()
                .WithMessage("Documento do favorecido (CPF) é obrigatório.");

            RuleFor(r => r.NumeroDocumentoFavorecido)
                .Must(StringExtensions.IsCpf)
                .WithMessage("Documento do favorecido não é válido, deve ser um CPF.");

            RuleFor(r => r.NomeLegalFavorecido)
                .NotEmpty()
                .WithMessage("Nome do favorecido é obrigatório.");

            RuleFor(r => r.ValorLancamento)
                .GreaterThan(0)
                .WithMessage("Valor do lançamento deve ser maior que 0");
        }

        private bool BeANumber(string value)
        {
            return TryParse(value, out _);
        }
    }
}

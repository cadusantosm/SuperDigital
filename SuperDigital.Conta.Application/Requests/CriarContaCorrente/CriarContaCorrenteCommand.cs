using MediatR;
using SuperDigital.Conta.Dominio.Entidades;

namespace SuperDigital.Conta.Applicacao.Requests.CriarContaCorrente
{
    public class CriarContaCorrenteCommand : IRequest<CriarContaCorrenteCommandResult>
    {
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public int DigitoVerificadorConta { get; set; }
        public string NumeroDocumento { get; set; }
        public string NomeLegal { get; set; }
        public decimal Valor { get; set; }
        public TipoConta TipoConta { get; set; }
    }
}

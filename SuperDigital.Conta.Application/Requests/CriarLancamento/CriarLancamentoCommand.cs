using MediatR;
using SuperDigital.Conta.Dominio.Entidades;
using System;

namespace SuperDigital.Conta.Applicacao.Requests.CriarLancamento
{
    public class CriarLancamentoCommand : IRequest<CriarLancamentoCommandResult>
    {
        public Guid IdentificadorContaOrigem { get; set; }
        public string AgenciaFavorecido { get; set; }
        public string NumeroContaFavorecido { get; set; }
        public int DigitoVerificadorFavorecido { get; set; }
        public string NumeroDocumentoFavorecido { get; set; }
        public string NomeLegalFavorecido { get; set; }
        public decimal ValorLancamento { get; set; }
        public TipoConta TipoContaFavorecido { get; set; }
    }
}

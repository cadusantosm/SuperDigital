using System;
using SuperDigital.Conta.Api.SharedContracts;
using SuperDigital.Conta.Dominio.Entidades;

namespace SuperDigital.Conta.Api.Contracts.CriarLancamento
{
    public class CriarLancamentoResult : Result
    {
        public string ContaOrigem { get; set; }
        public string ContaOrigemNome { get; set; }
        public string ContaDestino { get; set; }
        public string ContaDestinoNome { get; set; }
        public string NumeroDocumento { get; set; }
        public TipoConta TipoConta { get; set; }
        public TipoLancamento TipoTransacao { get; set; }
        public decimal Valor { get; set; }
        public DateTimeOffset DataDaTransacao { get; set; }
        public Guid AuthenticacaoBancaria { get; set; }
    }
}

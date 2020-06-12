using SuperDigital.Conta.Dominio.Entidades;
using System;

namespace SuperDigital.Conta.Applicacao.Requests.CriarLancamento
{
    public class CriarLancamentoCommandResult
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

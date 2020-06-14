using System;

namespace SuperDigital.Conta.Dominio.Entidades
{
    public class Lancamento
    {
        public Guid Id { get; set; }
        public Guid IdentificadorLancamento { get; set; }
        public Guid IdentificadorTransacao { get; set; }
        public Guid IdentificadorConta { get; set; }
        
        // Aqui poderia ser um tipo especializado money (struct)
        public decimal Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
        public DateTimeOffset DataLancamento { get; set; }
        public TipoLancamento TipoLancamento { get; set; }

        public Lancamento CriarLancamentoCredito(decimal valor, Guid identificadorConta)
        {
            return new Lancamento
            {
                Valor = valor,
                IdentificadorTransacao = Guid.NewGuid(),
                DataLancamento = DateTimeOffset.UtcNow,
                IdentificadorConta = identificadorConta,
                IdentificadorLancamento = Guid.NewGuid(),
                TipoLancamento = TipoLancamento.TransferenciaEntreContas,
                TipoTransacao = TipoTransacao.Credito
            };
        }

        public Lancamento CriarLancamentoDebito(decimal valor, Guid identificadorConta)
        {
            return new Lancamento
            {
                Valor = valor,
                IdentificadorTransacao = Guid.NewGuid(),
                DataLancamento = DateTimeOffset.UtcNow,
                IdentificadorConta = identificadorConta,
                IdentificadorLancamento = Guid.NewGuid(),
                TipoLancamento = TipoLancamento.TransferenciaEntreContas,
                TipoTransacao = TipoTransacao.Debito
            };
        }
    }
}
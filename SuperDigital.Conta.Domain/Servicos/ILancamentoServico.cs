using SuperDigital.Conta.Dominio.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Dominio.Servicos
{
    public interface ILancamentoServico
    {
        public Task<Guid> ExecutarTransacaoAsync(ContaCorrente contaCorrenteOrigem,
            ContaCorrente contaCorrenteDestino, TipoLancamento tipoTransacao, decimal valor, CancellationToken cancellationToken);
    }
}

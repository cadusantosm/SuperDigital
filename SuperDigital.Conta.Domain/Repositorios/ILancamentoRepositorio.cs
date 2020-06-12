using SuperDigital.Conta.Dominio.Entidades;
using System;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Dominio.Repositorios
{
    public interface ILancamentoRepositorio
    {
        Task<bool> SalvarLancamentosAsync(Lancamento[] lancamentos);
        Task<decimal> ObterSaldoContaAsync(Guid identificadorConta);
    }
}

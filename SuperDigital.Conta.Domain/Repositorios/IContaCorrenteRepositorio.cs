using SuperDigital.Conta.Dominio.Entidades;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Dominio.Repositorios
{
    public interface IContaCorrenteRepositorio
    {
        Task<ContaCorrente> CriarContaCorrenteAsync(ContaCorrente contaCorrente,
            CancellationToken cancellationToken);

        Task<ContaCorrente> ObterContaPorIdAsync(Guid identificadorConta, CancellationToken cancellationToken);

        Task<ContaCorrente> ObterContaPorDadosBancariosAsync(string agencia, string numeroConta, int digitoVerificador,
            string numeroDocumento, TipoConta tipoConta, CancellationToken cancellationToken);

        Task<bool> AtualizarSaldoContasAsync(ContaCorrente[] contas, CancellationToken cancellationToken);

        Task<ContaCorrente> ObterContaPorDocumentoAsync(string documento, CancellationToken cancellationToken);
    }
}

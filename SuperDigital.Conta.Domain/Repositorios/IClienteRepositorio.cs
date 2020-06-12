using SuperDigital.Conta.Dominio.Entidades;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Dominio.Repositorios
{
    public interface IClienteRepositorio
    {
        Task<Cliente> CriarClienteAsync(Cliente cliente, CancellationToken cancellationToken);
        Task<Cliente> ObterClientePorDocumentoAsync(string documento, CancellationToken cancellationToken);
        Task<Cliente> ObterClientePorCredenciaisAsync(string documento, string senha,
            CancellationToken cancellationToken);
    }
}

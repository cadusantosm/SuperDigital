using Marten;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Infraestrutura.DocumentStore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Infraestrutura.Repositorios
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly IDocumentStoreResolver _documentStoreResolver;

        public ClienteRepositorio(IDocumentStoreResolver documentStoreResolver)
        {
            _documentStoreResolver = documentStoreResolver;
        }

        public async Task<Cliente> CriarClienteAsync(Cliente cliente, CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.WritableDocumentStore.LightweightSession();

            session.Store(cliente);

            await session.SaveChangesAsync(cancellationToken);

            return cliente;
        }

        public async Task<Cliente> ObterClientePorDocumentoAsync(string documento, CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.WritableDocumentStore.LightweightSession();

            var cliente = await session.Query<Cliente>().Where(x => x.Documento == documento)
                .FirstOrDefaultAsync(cancellationToken);

            return cliente;
        }

        public async Task<Cliente> ObterClientePorCredenciaisAsync(string documento, string senha,
            CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.WritableDocumentStore.LightweightSession();

            var cliente = await session.Query<Cliente>().Where(x => x.Documento == documento && x.Senha == senha)
                .FirstOrDefaultAsync(cancellationToken);

            return cliente;
        }
    }
}

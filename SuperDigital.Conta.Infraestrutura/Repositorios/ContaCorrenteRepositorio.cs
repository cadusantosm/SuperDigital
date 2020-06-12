using Marten;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Infraestrutura.DocumentStore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Infraestrutura.Repositorios
{
    public class ContaCorrenteRepositorio : IContaCorrenteRepositorio
    {
        private readonly IDocumentStoreResolver _documentStoreResolver;
        public ContaCorrenteRepositorio(IDocumentStoreResolver documentStoreResolver)
        {
            _documentStoreResolver = documentStoreResolver;
        }

        public async Task<ContaCorrente> CriarContaCorrenteAsync(
            ContaCorrente contaCorrente,
            CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.WritableDocumentStore.LightweightSession();

            session.Store(contaCorrente);

            await session.SaveChangesAsync(cancellationToken);

            return contaCorrente;
        }

        public async Task<ContaCorrente> ObterContaPorIdAsync(Guid identificadorConta,
            CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.ReadableDocumentStore.LightweightSession();

            var result = await session.Query<ContaCorrente>()
                .Where(x => x.IdentificadorConta == identificadorConta)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ContaCorrente> ObterContaPorDadosBancariosAsync(string agencia, string numeroConta,
            int digitoVerificador, string numeroDocumento,
            TipoConta tipoConta,
            CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.ReadableDocumentStore.LightweightSession();

            var result = await session.Query<ContaCorrente>().Where(x =>
                    x.Agencia == agencia &&
                    x.NumeroConta == numeroConta &&
                    x.DigitoVerificadorConta == digitoVerificador &&
                    x.NumeroDocumento == numeroDocumento)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<ContaCorrente> ObterContaPorDocumentoAsync(string documento,
            CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.ReadableDocumentStore.LightweightSession();

            var result = await session.Query<ContaCorrente>().Where(x => x.NumeroDocumento == documento).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> AtualizarSaldoContasAsync(ContaCorrente[] contas,
            CancellationToken cancellationToken)
        {
            using var session = _documentStoreResolver.WritableDocumentStore.LightweightSession();

            session.Update(contas);

            await session.SaveChangesAsync(CancellationToken.None);

            return true;
        }
    }
}

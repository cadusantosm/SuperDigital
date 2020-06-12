using Marten;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Infraestrutura.DocumentStore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDigital.Conta.Infraestrutura.Repositorios
{
    public class LancamentoRepositorio : ILancamentoRepositorio
    {
        private readonly IDocumentStoreResolver _documentStoreResolver;

        public LancamentoRepositorio(IDocumentStoreResolver documentStoreResolver)
        {
            _documentStoreResolver = documentStoreResolver;
        }

        public async Task<bool> SalvarLancamentosAsync(Lancamento[] lancamentos)
        {
            using var session = _documentStoreResolver.WritableDocumentStore.LightweightSession();

            session.Store(lancamentos);

            await session.SaveChangesAsync();

            return true;
        }

        public async Task<decimal> ObterSaldoContaAsync(Guid identificadorConta)
        {
            using var sessin = _documentStoreResolver.ReadableDocumentStore.LightweightSession();

            var lancamentos = await
                sessin.Query<Lancamento>().Where(x => x.IdentificadorConta == identificadorConta)
                .ToListAsync();

            var debitos = lancamentos.Where(x => x.TipoTransacao == TipoTransacao.Debito).Sum(x => x.Valor);
            var creditos = lancamentos.Where(x => x.TipoTransacao == TipoTransacao.Credito).Sum(x => x.Valor);

            return creditos - debitos;
        }
    }
}

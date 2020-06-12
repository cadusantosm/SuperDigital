using Marten;

namespace SuperDigital.Conta.Infraestrutura.DocumentStore
{
    public interface IDocumentStoreResolver
    {
        IDocumentStore ReadableDocumentStore { get; }
        IDocumentStore WritableDocumentStore { get; }
    }
}

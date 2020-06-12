using Marten;

namespace SuperDigital.Conta.Infraestrutura.DocumentStore
{
    public class DocumentStoreResolver : IDocumentStoreResolver
    {
        //Usando o msm banco para leitura e escrita
        public IDocumentStore ReadableDocumentStore { get; }
        public IDocumentStore WritableDocumentStore { get; }

        public DocumentStoreResolver(string readableConnectionString, string writableConnectionString)
        {
            ReadableDocumentStore = Marten.DocumentStore.For(opts => Configure(readableConnectionString, opts));
            WritableDocumentStore = Marten.DocumentStore.For(opts => Configure(writableConnectionString, opts));
        }

        private static void Configure(string connectionString, StoreOptions opts)
        {
            opts.Connection(connectionString);

            opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;

            opts.Schema.For<Dominio.Entidades.ContaCorrente>()
                .Identity(x => x.IdentificadorConta);

            opts.Schema.For<Dominio.Entidades.Lancamento>()
                .Identity(x => x.Id);

            opts.Schema.For<Dominio.Entidades.Cliente>()
                .Identity(x => x.IdentificadorCliente);
        }
    }
}

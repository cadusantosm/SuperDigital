using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Contracts.CriarCliente
{
    public class CriarClienteResult : Result
    {
        public string Nome { get; set; }

        public string Documento { get; set; }
    }
}

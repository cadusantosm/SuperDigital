using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Contracts.Autenticar
{
    public class AutenticarClienteResult : Result
    {
        public string Token { get; set; }
    }
}

using MediatR;

namespace SuperDigital.Conta.Applicacao.Requests.AutenticarCliente
{
    public class AutenticarClienteCommand : IRequest<AutenticarClienteCommandResult>
    {
        public string Documento { get; set; }
        public string Senha { get; set; }
    }
}
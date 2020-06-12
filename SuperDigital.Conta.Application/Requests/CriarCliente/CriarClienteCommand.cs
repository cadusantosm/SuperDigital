using MediatR;

namespace SuperDigital.Conta.Applicacao.Requests.CriarCliente
{
    public class CriarClienteCommand : IRequest<CriarClienteCommandResult>
    {
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Senha { get; set; }
    }
}

namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ClienteJaCadastradoException : DomainException
    {
        public ClienteJaCadastradoException(string businessMessage) : base(businessMessage)
        {
        }
    }
}

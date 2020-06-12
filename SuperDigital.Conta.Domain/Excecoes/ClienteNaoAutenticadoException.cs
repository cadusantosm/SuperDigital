namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ClienteNaoAutenticadoException : DomainException
    {
        public ClienteNaoAutenticadoException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

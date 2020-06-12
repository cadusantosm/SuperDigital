namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ContaOrigemDeveSerDiferenteDaContaDestinoException : DomainException
    {
        public ContaOrigemDeveSerDiferenteDaContaDestinoException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

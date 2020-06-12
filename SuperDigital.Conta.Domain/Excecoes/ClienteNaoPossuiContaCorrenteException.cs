namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ClienteNaoPossuiContaCorrenteException : DomainException
    {
        public ClienteNaoPossuiContaCorrenteException(string businessMessage) : base(businessMessage)
        {
        }
    }
}

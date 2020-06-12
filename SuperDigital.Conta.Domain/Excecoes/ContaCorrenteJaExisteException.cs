namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ContaCorrenteJaExisteException : DomainException
    {
        public ContaCorrenteJaExisteException(string businessMessage) : base(businessMessage)
        {
        }
    }
}

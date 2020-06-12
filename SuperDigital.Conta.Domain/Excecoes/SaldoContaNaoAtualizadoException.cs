namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class SaldoContaNaoAtualizadoException : DomainException
    {
        public SaldoContaNaoAtualizadoException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

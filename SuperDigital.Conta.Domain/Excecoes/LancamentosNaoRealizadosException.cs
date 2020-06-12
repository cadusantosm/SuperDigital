namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class LancamentosNaoRealizadosException : DomainException
    {
        public LancamentosNaoRealizadosException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

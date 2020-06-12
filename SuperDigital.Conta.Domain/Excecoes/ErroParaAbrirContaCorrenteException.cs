namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ErroParaAbrirContaCorrenteException : DomainException
    {
        public ErroParaAbrirContaCorrenteException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

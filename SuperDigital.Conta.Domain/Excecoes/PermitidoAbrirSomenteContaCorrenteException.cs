namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class PermitidoAbrirSomenteContaCorrenteException : DomainException
    {
        public PermitidoAbrirSomenteContaCorrenteException(string message)
            : base(message)
        {
        }
    }
}

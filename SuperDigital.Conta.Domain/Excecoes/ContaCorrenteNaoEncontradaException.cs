namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ContaCorrenteNaoEncontradaException : DomainException
    {
        public ContaCorrenteNaoEncontradaException(string message)
            : base(message)
        {
        }
    }
}


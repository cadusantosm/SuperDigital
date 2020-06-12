namespace SuperDigital.Conta.Dominio.Excecoes
{
    public class ContaCorrenteSemSaldoParaEfetuarLancamento : DomainException
    {
        public ContaCorrenteSemSaldoParaEfetuarLancamento(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

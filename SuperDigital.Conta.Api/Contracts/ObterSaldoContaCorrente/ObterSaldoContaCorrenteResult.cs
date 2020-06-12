using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Contracts.ObterSaldoContaCorrente
{
    public class ObterSaldoContaCorrenteResult : Result
    {
        public decimal Saldo { get; set; }
        public string SaldoFormatado { get; set; }
    }
}

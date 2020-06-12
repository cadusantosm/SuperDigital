using MediatR;
using System;

namespace SuperDigital.Conta.Applicacao.Requests.ObterSaldoContaCorrente
{
    public class ObterSaldoContaCorrenteCommand : IRequest<ObterSaldoContaCorrenteCommandResult>
    {
        public Guid IdentificadorConta { get; set; }
    }
}
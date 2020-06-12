using System;
using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Contracts.CriarConta
{
    public class CriarContaCorrenteResult : Result
    {
        public Guid IdentificadorConta { get; set; }
    }
}

using System;

namespace SuperDigital.Conta.Dominio
{
    // Me soa estranho traduzir esse nome.
    public abstract class DomainException : Exception
    {
        public DomainException(string businessMessage)
            : base(businessMessage)
        {
        }
    }
}

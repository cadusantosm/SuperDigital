using System;
using System.Security.Claims;

namespace SuperDigital.Conta.Infraestrutura.Util
{
    public static class ClaimsExtensions
    {
        public static Guid ObterIdentificadorConta(this ClaimsPrincipal principal)
        {
            Guid.TryParse(principal.FindFirst(ClaimTypes.GroupSid)?.Value, out var identificadorConta);

            return identificadorConta;
        }
    }
}

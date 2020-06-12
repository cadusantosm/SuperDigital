using System;
using System.Security.Cryptography;
using System.Text;

namespace SuperDigital.Conta.Infraestrutura.Util
{
    public static class Utility
    {
        private const string Salt = "990f257b-59db-46e6-981b-92984e19ba45";

        public static string Criptografar(this string password)
        {
            var hash = MD5.Create();

            var bytes = hash.ComputeHash(Encoding.UTF32.GetBytes(Salt + password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}

using System;

namespace SuperDigital.Conta.Dominio.Entidades
{
    public class Cliente
    {
        public Guid IdentificadorCliente { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Senha { get; set; }
    }
}

using System;

namespace SuperDigital.Conta.Dominio.Entidades
{
    public class ContaCorrente
    {
        public Guid IdentificadorConta { get; set; }
        public string Agencia { get; set; }
        public string NumeroConta { get; set; }
        public int DigitoVerificadorConta { get; set; }
        public string NumeroDocumento { get; set; }
        public string NomeLegal { get; set; }


        /*
         ** Saldo deveria ser private set
         ** porém não encontrei como retornar um single value no marten
         ** para buscar apenas o saldo da conta e atualiza-lo pelo método.         
         */

        public decimal Saldo { get; set; }
        public TipoConta TipoConta { get; set; }
        public DateTimeOffset DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTimeOffset DataAlteracao;

        public void AtualizarSaldo(decimal value)
        {
            Saldo = value;
        }

        public override string ToString()
        {
            return $"{Agencia}/{NumeroConta}-{DigitoVerificadorConta}";
        }
    }
}

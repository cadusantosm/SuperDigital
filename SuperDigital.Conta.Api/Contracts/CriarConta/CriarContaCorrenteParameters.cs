using SuperDigital.Conta.Dominio.Entidades;

namespace SuperDigital.Conta.Api.Contracts.CriarConta
{
    public class CriarContaCorrenteParameters
    {
        /// <summary>
        /// Agência - 5 números
        /// </summary>
        public string Agencia { get; set; }

        /// <summary>
        /// 13 Números
        /// </summary>
        public string NumeroConta { get; set; }

        /// <summary>
        /// 2 Alfanuméricos
        /// </summary>
        public int DigitoVerificadorConta { get; set; }

        /// <summary>
        /// CPF - Com ou sem pontuações
        /// </summary>
        public string NumeroDocumento { get; set; }

        /// <summary>
        /// Máximo de 30 caracteres, somente letras
        /// </summary>
        public string NomeLegal { get; set; }

        /// <summary>
        /// Valor mínimo para abertura da conta
        /// </summary>
        public decimal Valor { get; set; }

        /// <summary>
        /// Tipo da conta
        /// </summary>
        public TipoConta TipoConta { get; set; } = TipoConta.Corrente;
    }
}
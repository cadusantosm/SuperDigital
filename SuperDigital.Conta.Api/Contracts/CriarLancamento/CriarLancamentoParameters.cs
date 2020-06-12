using SuperDigital.Conta.Dominio.Entidades;

namespace SuperDigital.Conta.Api.Contracts.CriarLancamento
{
    public class CriarLancamentoParameters
    {

        /// <summary>
        /// Agência - 5 números
        /// </summary>
        public string AgenciaFavorecido { get; set; }

        /// <summary>
        /// 13 Números
        /// </summary>
        public string NumeroContaFavorecido { get; set; }

        /// <summary>
        /// 2 Alfanuméricos
        /// </summary>
        public int DigitoVerificadorFavorecido { get; set; }

        /// <summary>
        /// CPF - Com ou sem pontuações
        /// </summary>
        public string NumeroDocumentoFavorecido { get; set; }

        /// <summary>
        /// Máximo de 30 caracteres, somente letras
        /// </summary>
        public string NomeLegalFavorecido { get; set; }

        /// <summary>
        /// Valor do lançamento a ser realizado
        /// </summary>
        public decimal ValorLancamento { get; set; }

        /// <summary>
        /// Tipo da conta
        /// </summary>
        public TipoConta TipoContaFavorecido { get; set; } = TipoConta.Corrente;
    }
}

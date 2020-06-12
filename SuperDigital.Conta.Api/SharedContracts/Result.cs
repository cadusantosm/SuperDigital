using System.Net;

namespace SuperDigital.Conta.Api.SharedContracts
{
    public class Result
    {
        /// <summary>
        /// Indica se a ação foi concluída com êxito.
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Mensagem de erro, se houver.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// O código de status final.
        /// </summary>
        public HttpStatusCode Code { get; set; } = (HttpStatusCode)200;
    }
}

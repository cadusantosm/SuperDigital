using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SuperDigital.Conta.Dominio.Excecoes;
using System;
using System.Net;
using System.Threading.Tasks;
using SuperDigital.Conta.Api.SharedContracts;

namespace SuperDigital.Conta.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ApplicationException e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);

                await HandleExceptionAsync(httpContext, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var message = exception.Message;

            switch (exception)
            {
                case ArgumentNullException _:
                case ArgumentOutOfRangeException _:
                    code = HttpStatusCode.UnprocessableEntity;
                    break;
                case NotImplementedException _:
                    code = HttpStatusCode.NotImplemented;
                    break;
                case NullReferenceException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case TimeoutException _:
                    code = HttpStatusCode.GatewayTimeout;
                    break;
                case ApplicationException _:
                    code = HttpStatusCode.BadRequest;
                    break;
                case AutoMapperMappingException _:
                    code = HttpStatusCode.BadRequest;
                    message = exception.InnerException.Message;
                    break;
                case PermitidoAbrirSomenteContaCorrenteException _:
                case ClienteJaCadastradoException _:
                case ContaCorrenteNaoEncontradaException _:
                case ContaCorrenteSemSaldoParaEfetuarLancamento _:
                case ContaOrigemDeveSerDiferenteDaContaDestinoException _:
                case ErroParaAbrirContaCorrenteException _:
                case LancamentosNaoRealizadosException _:
                case SaldoContaNaoAtualizadoException _:
                case ClienteNaoPossuiContaCorrenteException _:
                    code = HttpStatusCode.Conflict;
                    break;
                case ClienteNaoAutenticadoException _:
                    code = HttpStatusCode.Unauthorized;
                    break;
            }

            var result = JsonConvert.SerializeObject(new Result
            {
                Success = false,
                Message = message,
                Code = code
            });

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)code;

            return httpContext.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

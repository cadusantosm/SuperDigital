using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SuperDigital.Conta.Applicacao.Requests.CriarLancamento;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using SuperDigital.Conta.Dominio.Servicos;
using Xunit;

namespace SuperDigital.Conta.Applicacao.Testes.Requests
{
    public class CriarLancamentoCommandHandlerTestes
    {
        [Fact]
        public async Task Quando_Conta_Origem_Nao_For_Encontrada_Retornar_Erro()
        {
            // Arrange

            var lancamentoCommand = new CriarLancamentoCommand
            {
                AgenciaFavorecido = "0101",
                DigitoVerificadorFavorecido = 1,
                IdentificadorContaOrigem = Guid.NewGuid(),
                NomeLegalFavorecido = "Carlos Eduardo",
                NumeroContaFavorecido = "11",
                NumeroDocumentoFavorecido = "40671663895",
                TipoContaFavorecido = TipoConta.Corrente,
                ValorLancamento = 100
            };

            var cancellationToken = CancellationToken.None;
            var lancamentoServicoMock = new Mock<ILancamentoServico>(MockBehavior.Strict);
            var contaCorrenteRepositorioMock = new Mock<IContaCorrenteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Lancamento>(lancamentoCommand)).Returns(new Lancamento());

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorIdAsync(lancamentoCommand.IdentificadorContaOrigem, cancellationToken))
                .ReturnsAsync((ContaCorrente)null);

            var handler = new CriarLancamentoCommandHandler(contaCorrenteRepositorioMock.Object, lancamentoServicoMock.Object);

            // ACT

            var task = handler.Handle(lancamentoCommand, cancellationToken);

            // Assert

            await Assert.ThrowsAsync<ContaCorrenteNaoEncontradaException>(async () => await task);
        }

        [Fact]
        public async Task Quando_Conta_Destino_Nao_For_Encontrada_Retornar_Erro()
        {
            // Arrange

            var lancamentoCommand = new CriarLancamentoCommand
            {
                AgenciaFavorecido = "0101",
                DigitoVerificadorFavorecido = 1,
                IdentificadorContaOrigem = Guid.NewGuid(),
                NomeLegalFavorecido = "Carlos Eduardo",
                NumeroContaFavorecido = "11",
                NumeroDocumentoFavorecido = "40671663895",
                TipoContaFavorecido = TipoConta.Corrente,
                ValorLancamento = 100
            };

            var cancellationToken = CancellationToken.None;
            var lancamentoServicoMock = new Mock<ILancamentoServico>(MockBehavior.Strict);
            var contaCorrenteRepositorioMock = new Mock<IContaCorrenteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Lancamento>(lancamentoCommand)).Returns(new Lancamento());

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorIdAsync(lancamentoCommand.IdentificadorContaOrigem, cancellationToken))
                .ReturnsAsync(new ContaCorrente());

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorDadosBancariosAsync(
                lancamentoCommand.AgenciaFavorecido,
                lancamentoCommand.NumeroContaFavorecido,
                lancamentoCommand.DigitoVerificadorFavorecido,
                lancamentoCommand.NumeroDocumentoFavorecido,
                lancamentoCommand.TipoContaFavorecido,
                cancellationToken)).ReturnsAsync((ContaCorrente) null);

            var handler = new CriarLancamentoCommandHandler(contaCorrenteRepositorioMock.Object, lancamentoServicoMock.Object);

            // ACT

            var task = handler.Handle(lancamentoCommand, cancellationToken);

            // Assert

            await Assert.ThrowsAsync<ContaCorrenteNaoEncontradaException>(async () => await task);
        }

        [Fact]
        public async Task Quando_Conta_Origem_For_Igual_Conta_Destino_Retornar_Erro()
        {
            // Arrange

            var contaCorrente = new ContaCorrente
            {
                Agencia = "0101",
                DigitoVerificadorConta = 1,
                IdentificadorConta = Guid.NewGuid(),
                NomeLegal = "Carlos Eduardo",
                NumeroConta = "11",
                NumeroDocumento = "40671663895",
                TipoConta = TipoConta.Corrente
            };
            
            var lancamentoCommand = new CriarLancamentoCommand
            {
                AgenciaFavorecido = "0101",
                DigitoVerificadorFavorecido = 1,
                IdentificadorContaOrigem = Guid.NewGuid(),
                NomeLegalFavorecido = "Carlos Eduardo",
                NumeroContaFavorecido = "11",
                NumeroDocumentoFavorecido = "40671663895",
                TipoContaFavorecido = TipoConta.Corrente,
                ValorLancamento = 100
            };

            var cancellationToken = CancellationToken.None;
            var lancamentoServicoMock = new Mock<ILancamentoServico>(MockBehavior.Strict);
            var contaCorrenteRepositorioMock = new Mock<IContaCorrenteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Lancamento>(lancamentoCommand)).Returns(new Lancamento());

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorIdAsync(lancamentoCommand.IdentificadorContaOrigem, cancellationToken))
                .ReturnsAsync(contaCorrente);

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorDadosBancariosAsync(
                lancamentoCommand.AgenciaFavorecido,
                lancamentoCommand.NumeroContaFavorecido,
                lancamentoCommand.DigitoVerificadorFavorecido,
                lancamentoCommand.NumeroDocumentoFavorecido,
                lancamentoCommand.TipoContaFavorecido,
                cancellationToken)).ReturnsAsync(contaCorrente);

            var handler = new CriarLancamentoCommandHandler(contaCorrenteRepositorioMock.Object, lancamentoServicoMock.Object);

            // ACT

            var task = handler.Handle(lancamentoCommand, cancellationToken);

            // Assert

            await Assert.ThrowsAsync<ContaOrigemDeveSerDiferenteDaContaDestinoException>(async () => await task);
        }

        [Fact]
        public async Task Quando_Saldo_Conta_Origem_For_Menor_Que_Valor_Do_Lancamento_Retornar_Erro()
        {
             // Arrange

            var contaCorrente = new ContaCorrente
            {
                Agencia = "0101",
                DigitoVerificadorConta = 1,
                IdentificadorConta = Guid.NewGuid(),
                NomeLegal = "Carlos Eduardo",
                NumeroConta = "11",
                NumeroDocumento = "40671663895",
                TipoConta = TipoConta.Corrente,
                Saldo = 50
            };
            
            var lancamentoCommand = new CriarLancamentoCommand
            {
                AgenciaFavorecido = "0101",
                DigitoVerificadorFavorecido = 1,
                IdentificadorContaOrigem = Guid.NewGuid(),
                NomeLegalFavorecido = "Carlos Eduardo",
                NumeroContaFavorecido = "11",
                NumeroDocumentoFavorecido = "40671663895",
                TipoContaFavorecido = TipoConta.Corrente,
                ValorLancamento = 100
            };

            var cancellationToken = CancellationToken.None;
            var lancamentoServicoMock = new Mock<ILancamentoServico>(MockBehavior.Strict);
            var contaCorrenteRepositorioMock = new Mock<IContaCorrenteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<Lancamento>(lancamentoCommand)).Returns(new Lancamento());

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorIdAsync(lancamentoCommand.IdentificadorContaOrigem, cancellationToken))
                .ReturnsAsync(contaCorrente);

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorDadosBancariosAsync(
                lancamentoCommand.AgenciaFavorecido,
                lancamentoCommand.NumeroContaFavorecido,
                lancamentoCommand.DigitoVerificadorFavorecido,
                lancamentoCommand.NumeroDocumentoFavorecido,
                lancamentoCommand.TipoContaFavorecido,
                cancellationToken)).ReturnsAsync(new ContaCorrente());

            var handler = new CriarLancamentoCommandHandler(contaCorrenteRepositorioMock.Object, lancamentoServicoMock.Object);

            // ACT

            var task = handler.Handle(lancamentoCommand, cancellationToken);

            // Assert

            await Assert.ThrowsAsync<ContaCorrenteSemSaldoParaEfetuarLancamento>(async () => await task);
        }

        
    }
}

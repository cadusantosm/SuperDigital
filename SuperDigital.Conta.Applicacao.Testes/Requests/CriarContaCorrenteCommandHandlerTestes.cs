using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SuperDigital.Conta.Applicacao.Requests.CriarCliente;
using SuperDigital.Conta.Applicacao.Requests.CriarContaCorrente;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using Xunit;

namespace SuperDigital.Conta.Applicacao.Testes.Requests
{
    public class CriarContaCorrenteCommandHandlerTestes
    {
        [Fact]
        public async Task Quando_Cadastro_Diferente_De_Conta_Corrente_Deve_Retornar_Erro()
        {
            //Arrange

            var contaCorrenteCommand = new CriarContaCorrenteCommand
            {
                Valor = 100,
                TipoConta = TipoConta.Poupanca
            };

            var cancellationToken = CancellationToken.None;
            var lancamentoRepositorioMock = new Mock<ILancamentoRepositorio>(MockBehavior.Strict);
            var contaCorrenteRepositorioMock = new Mock<IContaCorrenteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<ContaCorrente>(contaCorrenteCommand)).Returns(new ContaCorrente
            {
                TipoConta = TipoConta.Poupanca
            });

            var handler = new CriarContaCorrenteCommandHandler(contaCorrenteRepositorioMock.Object, mapperMock.Object,
                lancamentoRepositorioMock.Object);

            // ACT

            var task = handler.Handle(contaCorrenteCommand,  cancellationToken);
            
            // Assert

            await Assert.ThrowsAsync<PermitidoAbrirSomenteContaCorrenteException>(async () => await task);
        }

        [Fact]
        public async Task Quando_Conta_Existe_Retornar_Erro()
        {
            //Arrange
            
            var contaCorrenteCommand = new CriarContaCorrenteCommand
            {
                Valor = 100,
                TipoConta = TipoConta.Corrente,
                Agencia = "001",
                DigitoVerificadorConta = 1,
                NumeroDocumento = "40671663895",
                NomeLegal = "Carlos Eduardo",
                NumeroConta = "001"
            };

            var cancellationToken = CancellationToken.None;
            var lancamentoRepositorioMock = new Mock<ILancamentoRepositorio>(MockBehavior.Strict);
            var contaCorrenteRepositorioMock = new Mock<IContaCorrenteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            mapperMock.Setup(x => x.Map<ContaCorrente>(contaCorrenteCommand)).Returns(new ContaCorrente
            {
                TipoConta = TipoConta.Poupanca
            });

            contaCorrenteRepositorioMock.Setup(x => x.ObterContaPorDadosBancariosAsync(contaCorrenteCommand.Agencia,
                contaCorrenteCommand.NumeroConta,
                contaCorrenteCommand.DigitoVerificadorConta,
                contaCorrenteCommand.NumeroDocumento,
                contaCorrenteCommand.TipoConta,
                cancellationToken)).ReturnsAsync(new ContaCorrente());

            var handler = new CriarContaCorrenteCommandHandler(contaCorrenteRepositorioMock.Object, mapperMock.Object,
                lancamentoRepositorioMock.Object);

            // ACT

            var task = handler.Handle(contaCorrenteCommand,  cancellationToken);
            
            // Assert

            await Assert.ThrowsAsync<ContaCorrenteJaExisteException>(async () => await task);
        }
    }
}

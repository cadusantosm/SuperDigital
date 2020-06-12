using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using SuperDigital.Conta.Applicacao.Requests.CriarCliente;
using SuperDigital.Conta.Dominio.Entidades;
using SuperDigital.Conta.Dominio.Excecoes;
using SuperDigital.Conta.Dominio.Repositorios;
using Xunit;

namespace SuperDigital.Conta.Applicacao.Testes.Requests
{
    public class CriarClienteCommandHandlerTestes
    {
        [Fact]
        public async Task Quando_Cliente_Existe_Retornar_Erro()
        {
            //Arrange

            var clienteRepositorioMock = new Mock<IClienteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();

            var cliente = new Cliente
            {
                Documento = "40671663895",
                Nome = "Carlos Eduardo",
                Senha = "123456",
                IdentificadorCliente = Guid.NewGuid()
            };

            var cancellationToken = CancellationToken.None;

            clienteRepositorioMock.Setup(x => x.ObterClientePorDocumentoAsync(cliente.Documento, cancellationToken))
                .ReturnsAsync(cliente);

            var handler = new CriarClienteCommandHandler(clienteRepositorioMock.Object, mapperMock.Object);

            // ACT

            var task = handler.Handle(new CriarClienteCommand
            {
                Documento = cliente.Documento,
                Senha = cliente.Senha,
                Nome = cliente.Nome
            }, cancellationToken);


            // Assert

            await Assert.ThrowsAsync<ClienteJaCadastradoException>(async () => await task);
        }
        
        [Fact]
        public async Task Quando_Cliente_Nao_Existe_Gravar_E_Retornar_Novo_Cliente()
        {
            //Arrange

            var cliente = new Cliente
            {
                Documento = "40671663895",
                Nome = "Carlos Eduardo",
                Senha = "123456",
                IdentificadorCliente = Guid.NewGuid()
            };

            var clienteCommand = new CriarClienteCommand
            {
                Documento = "40671663895",
                Nome = "Carlos Eduardo",
                Senha = "123456",
            };

            var cancellationToken = CancellationToken.None;

            var clienteRepositorioMock = new Mock<IClienteRepositorio>(MockBehavior.Strict);
            var mapperMock = new Mock<IMapper>();
            
            mapperMock.Setup(x => x.Map<Cliente>(clienteCommand)).Returns(cliente);
            clienteRepositorioMock.Setup(x => x.ObterClientePorDocumentoAsync("40671663895", cancellationToken)).ReturnsAsync((Cliente)null);
            clienteRepositorioMock.Setup(x => x.CriarClienteAsync(cliente,cancellationToken)).ReturnsAsync(cliente);

            var handler = new CriarClienteCommandHandler(clienteRepositorioMock.Object, mapperMock.Object);

            // ACT

            var result = await handler.Handle(clienteCommand, cancellationToken);
            
            // Assert

            Assert.NotNull(result);
            Assert.Equal(cliente.Documento,result.Documento);
            Assert.Equal(cliente.Nome,result.Nome);
            Assert.IsType<CriarClienteCommandResult>(result);
        }
    }
}

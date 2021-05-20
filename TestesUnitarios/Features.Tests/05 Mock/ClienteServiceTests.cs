using Features.Clientes;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Features.Tests._05_Mock
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceTests
    {
        private readonly ClienteTestsBogusFixture _clienteTestsFixture;

        public ClienteServiceTests(ClienteTestsBogusFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();
            var clienteRepository = new Mock<IClienteRepository>();
            var clienteMediator = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepository.Object, clienteMediator.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Once);
            clienteMediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_Adicionar_DeveFalharDevidoClienteInvalido()
        {
            //Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();
            var clienteRepository = new Mock<IClienteRepository>();
            var clienteMediator = new Mock<IMediator>();
            var clienteService = new ClienteService(clienteRepository.Object, clienteMediator.Object);

            //Act
            clienteService.Adicionar(cliente);

            // Assert
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Never);
            clienteMediator.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            //Arrange
            var clienteRepository = new Mock<IClienteRepository>();
            var clienteMediator = new Mock<IMediator>();

            clienteRepository.Setup(c => c.ObterTodos())
                .Returns(_clienteTestsFixture.ObterClientesVariados());

            var clienteService = new ClienteService(clienteRepository.Object, clienteMediator.Object);

            //Act

            var clientes = clienteService.ObterTodosAtivos();
            

            // Assert
            clienteRepository.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.False(clientes.Count(c => !c.Ativo) > 0);
        }
    }
}

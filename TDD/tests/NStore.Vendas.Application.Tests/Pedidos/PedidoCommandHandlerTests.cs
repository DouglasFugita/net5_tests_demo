using MediatR;
using Moq;
using Moq.AutoMock;
using NStore.Vendas.Application.Commands;
using NStore.Vendas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NStore.Vendas.Application.Tests.Pedidos
{
    public class PedidoCommandHandlerTests
    {

        private readonly Guid _clienteId;
        private readonly Guid _produtoId;
        private readonly Pedido _pedido;
        private readonly AutoMocker _mocker;
        private readonly PedidoCommandHandler _pedidoHandler;

        public PedidoCommandHandlerTests()
        {
            _mocker = new AutoMocker();
            _pedidoHandler = _mocker.CreateInstance<PedidoCommandHandler>();

            _clienteId = Guid.NewGuid();
            _produtoId = Guid.NewGuid();

            _pedido = Pedido.PedidoFactory.NovoPedidoRascunho(_clienteId);
        }

        [Fact(DisplayName = "Adicionar Item Novo Pedido com Sucesso")]
        [Trait("Vendas", "Pedido Command Handler")]
        public async Task AdicionarItem_NovoPedido_DeveExecutarComSucesso()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);
            _mocker.GetMock<IPedidoRepository>().Setup(p => p.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IPedidoRepository>().Verify(p => p.Adicionar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(p => p.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Novo Item Pedido Rascunho com Sucesso")]
        [Trait("Vendas", "Pedido Command Handler")]
        public async Task AdicionarItem_NovoItemAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var pedidoItemExistente = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);
            _pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionarItemPedidoCommand(_clienteId, Guid.NewGuid(), "Produto Teste 2", 2, 100);

            _mocker.GetMock<IPedidoRepository>().Setup(p => p.ObterPedidoRascunhoPorClienteId(_clienteId)).Returns(Task.FromResult(_pedido));
            _mocker.GetMock<IPedidoRepository>().Setup(p => p.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IPedidoRepository>().Verify(p => p.AdicionarItem(It.IsAny<PedidoItem>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(p => p.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(p => p.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Existente ao Pedido Rascunho com Sucesso")]
        [Trait("Vendas", "Pedido Command Handler")]
        public async Task AdicionarItem_ItemExistenteAoPedidoRascunho_DeveExecutarComSucesso()
        {
            // Arrange
            var pedidoItemExistente = new PedidoItem(_produtoId, "Produto Teste", 2, 100);
            _pedido.AdicionarItem(pedidoItemExistente);

            var pedidoCommand = new AdicionarItemPedidoCommand(_clienteId, _produtoId, "Produto Teste", 2, 100);

            _mocker.GetMock<IPedidoRepository>().Setup(r => r.ObterPedidoRascunhoPorClienteId(_clienteId)).Returns(Task.FromResult(_pedido));
            _mocker.GetMock<IPedidoRepository>().Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.True(result);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.AtualizarItem(It.IsAny<PedidoItem>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.Atualizar(It.IsAny<Pedido>()), Times.Once);
            _mocker.GetMock<IPedidoRepository>().Verify(r => r.UnitOfWork.Commit(), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Item Command Invalido")]
        [Trait("Vendas", "Pedido Command Handler")]
        public async Task AdicionarItem_CommandInvalido_DeveRetornarFalsoELancarEventosDeNotificacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty, Guid.Empty, "", 0, 0);

            // Act
            var result = await _pedidoHandler.Handle(pedidoCommand, CancellationToken.None);

            // Assert
            Assert.False(result);
            _mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Exactly(5));
        }
    }
}

using NStore.Vendas.Application.Commands;
using NStore.Vendas.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Valido")]
        [Trait("Vendas", "Pedido Commands")]
        public void AdicionarItemPedidoCommand_ComandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            var command = new AdicionarItemPedidoCommand(Guid.NewGuid(), Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            var result = command.EhValido();

            // Assert
            Assert.True(result);

        }

        [Fact(DisplayName = "Adicionar Item Command Invalido")]
        [Trait("Vendas", "Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty,
                Guid.Empty, "", 0, 0);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Item Command unidades acima do permitido")]
        [Trait("Vendas", "Pedido Commands")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesSuperiorAoPermitido_NaoDevePassarNaValidacao()
        {
            // Arrange
            var pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act
            var result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg, pedidoCommand.ValidationResult.Errors.Select(c => c.ErrorMessage));
        }
    }
}

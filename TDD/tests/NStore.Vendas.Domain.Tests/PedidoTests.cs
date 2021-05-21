using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace NStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Vendas", "Pedido Tests")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Vendas", "Pedido Tests")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadesSomarValores()
        {
            // Arrange
            var pedido =  Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", 1, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item com uniades acima do permitido")]
        [Trait("Vendas", "Pedido Tests")]
        public void AdicionarItemPedido_ItemExistenteSomaUnidadesAcimaPermitido_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 1, 100);
            var pedidoItem2 = new PedidoItem(produtoId, "Produto Teste", Pedido.MAX_UNIDADES_ITEM, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Item nao Existente")]
        [Trait("Vendas", "Pedido Tests")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            // Act - Aseert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItem));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("Vendas", "Pedido Tests")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var produtoId = Guid.NewGuid();
            var pedidoItem = new PedidoItem(produtoId, "Produto Teste", 2, 100);
            pedido.AdicionarItem(pedidoItem);

            var pedidoItemAtualizado = new PedidoItem(produtoId, "Produto Teste", 5, 100);

            //Act
            pedido.AtualizarItem(pedidoItemAtualizado);

            // Act - Aseert
            Assert.Equal(pedidoItemAtualizado.Quantidade, pedido.PedidoItems.FirstOrDefault(p => p.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Atualizar Total")]
        [Trait("Vendas", "Pedido Tests")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            
            var produto1Id = Guid.NewGuid();
            var pedidoItem1 = new PedidoItem(produto1Id, "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste 2 ", 4, 100);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var pedidoItem1Atualizado = new PedidoItem(produto1Id, "Produto Teste", 5, 100);

            var totalPedido = (pedidoItem2.Quantidade * pedidoItem2.ValorUnitario) + (pedidoItem1Atualizado.Quantidade * pedidoItem1Atualizado.ValorUnitario);

            //Act
            pedido.AtualizarItem(pedidoItem1Atualizado);

            // Act - Aseert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Remover Item Pedido Item nao Existente")]
        [Trait("Vendas", "Pedido Tests")]
        public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 5, 100);

            // Act - Aseert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem));
        }


        [Fact(DisplayName = "Remover Item Pedido Atualizar Total")]
        [Trait("Vendas", "Pedido Tests")]
        public void RemoverItemPedido_ItemExistente_DeveAtualizarValorTotal()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            var produto1Id = Guid.NewGuid();
            var pedidoItem1 = new PedidoItem(produto1Id, "Produto Teste", 2, 100);
            var pedidoItem2 = new PedidoItem(Guid.NewGuid(), "Produto Teste 2 ", 4, 100);
            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            var totalPedido = (pedidoItem2.Quantidade * pedidoItem2.ValorUnitario);

            //Act
            pedido.RemoverItem(pedidoItem1);

            // Act - Aseert
            Assert.Equal(totalPedido, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher valido")]
        [Trait("Vendas", "Pedido Tests")]
        public void Pedido_AplicarVoucherValido_DeveRetornarSemErros()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("PROMO-15-REAIS", 15, null, TipoDescontoVoucher.Porcentagem, 1, DateTime.Now.AddDays(15), true, false);

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);

        }

        [Fact(DisplayName = "Aplicar voucher tipo valor desconto")]
        [Trait("Vendas", "Pedido Tests")]
        public void Pedido_AplicarVoucherTipoValor_DeveRetornarValorComDesconto()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(15), true, false);

            // Act
            pedido.AdicionarItem(pedidoItem);
            pedido.AplicarVoucher(voucher);

            var totalComDesconto = (pedidoItem.Quantidade * pedidoItem.ValorUnitario) - voucher.ValorDesconto.Value;

            // Assert
            Assert.Equal(totalComDesconto, pedido.ValorTotal);
            Assert.Equal(15, pedido.ValorDesconto);
        }

        [Fact(DisplayName = "Aplicar voucher tipo valor desconto maior que valor total")]
        [Trait("Vendas", "Pedido Tests")]
        public void Pedido_AplicarVoucherTipoValorMaiorQueValortotal_DeveRetornarValorTotalZero()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 1, 2);
            var voucher = new Voucher("PROMO-15-REAIS", null, 15, TipoDescontoVoucher.Valor, 1, DateTime.Now.AddDays(15), true, false);

            // Act
            pedido.AdicionarItem(pedidoItem);
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(0, pedido.ValorTotal);
            Assert.Equal(2, pedido.ValorDesconto);
        }

        [Fact(DisplayName = "Aplicar voucher tipo Percentual desconto")]
        [Trait("Vendas", "Pedido Tests")]
        public void Pedido_AplicarVoucherTipoPercentual_DeveRetornarValorComDesconto()
        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var pedidoItem = new PedidoItem(Guid.NewGuid(), "Produto Teste", 2, 100);
            var voucher = new Voucher("PROMO-15-REAIS", 10, null, TipoDescontoVoucher.Porcentagem, 1, DateTime.Now.AddDays(15), true, false);

            // Act
            pedido.AdicionarItem(pedidoItem);
            pedido.AplicarVoucher(voucher);

            var totalComDesconto = (pedidoItem.Quantidade * pedidoItem.ValorUnitario);
            totalComDesconto *= (voucher.PercentualDesconto.Value / 100);

            // Assert
            Assert.Equal(totalComDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher invalido")]
        [Trait("Vendas", "Pedido Tests")]
        public void Pedido_AplicarVoucherInvalido_DeveRetornarErros()

        {
            // Arrange
            var pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            var voucher = new Voucher("", null, null, TipoDescontoVoucher.Porcentagem, 0, DateTime.Now.AddDays(-1), false, true);

            // Act
            var result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.False(result.IsValid);
        }

    }
}

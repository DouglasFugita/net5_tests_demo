using NStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Vendas.Domain
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        void Adicionar(Pedido pedido);
        void Atualizar(Pedido pedido);
        Task<Pedido> ObterPedidoRascunhoPorClienteId(Guid clietneId);

        void AdicionarItem(PedidoItem pedidoItem);
        void AtualizarItem(PedidoItem pedidoItem);
    }
}

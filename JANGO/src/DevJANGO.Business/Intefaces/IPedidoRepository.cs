using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        /* 1 Pedido : N PedidoItems
      * Obter um determinado Pedido com os seus PedidoItems */
        Task<Pedido> ObterPedidoPedidoItems(Guid id);

        // Obter uma lista de Pedidos com determinado Cliente
        Task<IEnumerable<Pedido>> ObterPedidosPorAlunoMatriculado(Guid clienteId);
        // Obter uma lista todos os Pedidos e todos os Clientes
        Task<IEnumerable<Pedido>> ObterPedidosAlunoMatriculados();

        // Obter um Pedido com o seu Cliente

        //Task<Pedido> ObterPedidoCliente(Guid id);
        Task<Pedido> ObterPedido(Guid id);
            Task<Pedido> ObterPedidoAlunoMatriculados(Guid id);
        // Obter um Pedido com todos PedidoItems e o seu Cliente
        Task<Pedido> ObterPedidoPedidoItemsAlunoMatriculado(Guid id);
        Task<Pedido> ObterPedidoPeloItems(Guid pedidoId);
        Task<IEnumerable<Pedido>> ObterPedidoPorPedidoItems(Guid pedidoId);
        
        //Task<Pedido> AdicionarAoCarrinho(Guid id, Produto produto);
    }
}

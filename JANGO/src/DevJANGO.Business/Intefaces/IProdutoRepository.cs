using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<Produto> ObterProdutoPrecoValorVenda(Guid id);
        Task<Produto> ObterProdutoPorId(Guid id);
        Task<Produto> ObterPrecoProdutoPorId(Guid id);
        Task<Produto> ObterNomeProduto(Guid id);
        Task<Produto> ObterProdutoPeloCodigo(int codigo);
    
        //----------------------------- PedidoItems -------------------------------------
        /* 1 Pedido : N PedidoItems
        * Obter um determinado Produto com os seus PedidoItems */
        Task<Produto> ObterProdutoPedidoItems(Guid id);
    }
}

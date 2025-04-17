using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Data.Repository
{
    public class PedidoItemsRepository : Repository<PedidoItem>, IPedidoItemsRepository
    {
        public PedidoItemsRepository(JangoDbContext context) : base(context) { }

        public Task<PedidoItem> AdicionarAoCarrinho(PedidoItem pedidoItem)
        {
            throw new NotImplementedException();
        }

        public async Task<PedidoItem> ObterPedidoItemProduto(Guid id)
         {
            return await Db.PedidoItems.AsNoTracking().Include(p => p.Produto)
                .FirstOrDefaultAsync(pi => pi.Id == id);
         }
        public async Task<PedidoItem> ObterPedidoItemsPeloPedido(Guid pedidoId)
        {
            return await Db.PedidoItems.AsNoTracking()
                .Include(p => p.Produto)
               .FirstOrDefaultAsync(pi => pi.PedidoId == pedidoId);
        }
   
        public async Task<IEnumerable<PedidoItem>> ObterPedidoItemsPorProduto(Guid produtoId)
        {
           return await Buscar(pi => pi.ProdutoId == produtoId);
        }

        public async Task<IEnumerable<PedidoItem>> ObterPedidoItemsPorFuncionarioCaixa(Guid funcionarioCaixaId)
        {
           return await Buscar(pi => pi.FuncionarioCaixaId == funcionarioCaixaId);
        }
        public async Task<IEnumerable<PedidoItem>> ObterPedidoItemsPedido(Guid pedidoId)
        {
            return await Db.PedidoItems.Where(pi => pi.PedidoId == pedidoId).AsNoTracking()
          .Include(m => m.Produto)
          .Include(m => m.FuncionarioCaixa)
          .OrderBy(a => a.Produto.Nome).ToListAsync();
        }
    
        public async Task<IEnumerable<PedidoItem>> ObterPedidoItemsPorPedido(Guid pedidoId)
        {
            return await Buscar(pi => pi.ProdutoId == pedidoId);
        }
        public async Task<PedidoItem> ObterPedidoItemFuncionarioCaixa(Guid id)
        {
            return await Db.PedidoItems.AsNoTracking().Include(v => v.FuncionarioCaixa)
                .FirstOrDefaultAsync(pi => pi.Id == id);
        }

        public async Task<IEnumerable<PedidoItem>> ObterPedidosItemsProdutos()
         {
            return await Db.PedidoItems.AsNoTracking()
                .Include(v => v.Produto)
                .OrderBy(pi => pi.Produto.Nome).ToListAsync();
        }

         public async Task<IEnumerable<PedidoItem>> ObterPedidosItemsFuncionarioCaixas()
         {
            return await Db.PedidoItems.AsNoTracking().Include(v => v.FuncionarioCaixa)
                 .OrderBy(pi => pi.Produto.Nome).ToListAsync();
         }
        
        //ObterPedidoItemsPorPedido
        public async Task<PedidoItem> ObterProdutoPorId(Guid id)
        {
            return await Db.PedidoItems.AsNoTracking()
              .FirstOrDefaultAsync(pi => pi.Id == id);
        }
    }
}

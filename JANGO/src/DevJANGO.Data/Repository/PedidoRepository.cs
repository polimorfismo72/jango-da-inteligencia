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
    public class PedidoRepository : Repository<Pedido>, IPedidoRepository
    {   
        public PedidoRepository(JangoDbContext context) : base(context){ }

     
        public async Task<Pedido> ObterPedido(Guid id)
        {
            return await Db.Pedidos.AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
     
        public async Task<Pedido> ObterPedidoAlunoMatriculados(Guid id)
        {
            return await Db.Pedidos.AsNoTracking()
               .Include(c => c.AlunoMatriculado)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Pedido> ObterPedidoPedidoItems(Guid id)
        {
            return await Db.Pedidos.AsNoTracking().Include(pi => pi.PedidoItems)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Pedido> ObterPedidoPeloItems(Guid pedidoId)
        {
            return await Db.Pedidos.AsNoTracking().Include(pi => pi.PedidoItems)
                 .FirstOrDefaultAsync(p => p.Id == pedidoId);
        }
        public async Task<Pedido> ObterPedidoPedidoItemsAlunoMatriculado(Guid id)
         {
            return await Db.Pedidos.AsNoTracking()
                .Include(pi => pi.PedidoItems)
                .Include(c => c.AlunoMatriculado)
                  .FirstOrDefaultAsync(p => p.Id == id);
         }
   
        public async Task<IEnumerable<Pedido>> ObterPedidoPorPedidoItems(Guid pedidoId)
        {
            return await Buscar(p => p.Id == pedidoId);
        }
        public async Task<IEnumerable<Pedido>> ObterPedidosAlunoMatriculados()
        {
            return await Db.Pedidos.AsNoTracking().Include(c => c.AlunoMatriculado)
                .OrderBy(p => p.Codigo).ToListAsync();
        }
       
        public async Task<IEnumerable<Pedido>> ObterPedidosPorAlunoMatriculado(Guid clienteId)
        {
            return await Buscar(p => p.AlunoMatriculadoId == clienteId);
        }
    }
}

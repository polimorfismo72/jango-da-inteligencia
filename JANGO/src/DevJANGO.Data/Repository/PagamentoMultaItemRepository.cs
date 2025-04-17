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
    public class PagamentoMultaItemRepository : Repository<PagamentoMultaItem>, IPagamentoMultaItemRepository
    {
        public PagamentoMultaItemRepository(JangoDbContext context) : base(context) { }

        public async Task<PagamentoMultaItem> ObterObterPagamentoMultaItemMulta(Guid id)
        {
            return await Db.PagamentoMultaItems.AsNoTracking().Include(a => a.Multa)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagamentoMultaItem> ObterObterPagamentoMultaItemPagamentoMulta(Guid id)
        {
            return await Db.PagamentoMultaItems.AsNoTracking().Include(a => a.PagamentoMulta)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagamentoMultaItem> ObterObterPagamentoMultaItemFuncionarioCaixa(Guid id)
        {
            return await Db.PagamentoMultaItems.AsNoTracking().Include(a => a.FuncionarioCaixa)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemMultas()
        {
            return await Db.PagamentoMultaItems.AsNoTracking()
        .Include(p => p.Multa)
                .OrderBy(p => p.NomeMulta).ToListAsync();
        }
        public async Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPagamentoMultas()
        {
            return await Db.PagamentoMultaItems.AsNoTracking()
        .Include(p => p.PagamentoMulta)
                .OrderBy(p => p.NomeMulta).ToListAsync();
        }
        public async Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemFuncionarioCaixas()
        {
            return await Db.PagamentoMultaItems.AsNoTracking()
        .Include(p => p.FuncionarioCaixa)
                .OrderBy(p => p.NomeMulta).ToListAsync();
        }

        public async Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPorMulta(Guid multaId)
        {
            return await Buscar(p => p.MultaId == multaId);
        }

        public async Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPorPagamentoMulta(Guid pagamentoMultaId)
        {
            return await Buscar(p => p.PagamentoMultaId == pagamentoMultaId);
        }

        public async Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPorFuncionarioCaixa(Guid funcionarioCaixaId)
        {
            return await Buscar(p => p.FuncionarioCaixaId == funcionarioCaixaId);
        }







    }
}

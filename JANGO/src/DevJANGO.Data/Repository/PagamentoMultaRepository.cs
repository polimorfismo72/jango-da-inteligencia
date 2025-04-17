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
    public class PagamentoMultaRepository : Repository<PagamentoMulta>, IPagamentoMultaRepository
    {
        public PagamentoMultaRepository(JangoDbContext context) : base(context) { }
        public async Task<PagamentoMulta> ObterPagamentoMultaAlunoMatriculado(Guid id)
        {
            return await Db.PagamentoMultas.AsNoTracking().Include(a => a.AlunoMatriculado)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PagamentoMulta>> ObterPagamentoMultasAlunoMatriculados()
        {
            return await Db.PagamentoMultas.AsNoTracking().Include(a => a.AlunoMatriculado)
                .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }

        public async Task<IEnumerable<PagamentoMulta>> ObterPagamentoMultasPorAlunoMatriculado(Guid alunoMatriculadoId)
        { return await Buscar(p => p.AlunoMatriculadoId == alunoMatriculadoId); }

    }
}

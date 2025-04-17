using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DevJANGO.Data.Repository
{
    public class PagamentoPropinaRepository : Repository<PagamentoPropina>, IPagamentoPropinaRepository
    {

        private readonly JangoDbContext _context;
        public PagamentoPropinaRepository(JangoDbContext context) : base(context) { _context = context; }

        public async Task<PagamentoPropina> ObterPagamentoPropinaAlunoMatriculado(Guid id)
        {
            return await Db.PagamentoPropinas.AsNoTracking().Include(a => a.AlunoMatriculado)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagamentoPropina> ObterPagamentoPropina(Guid id)
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                .Include(a => a.Propinas)
                .Include(a => a.AlunoMatriculado)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<PagamentoPropina> ObterPagamentoPropinaPeloAluno(Guid id)
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                .Include(a => a.Propinas)
                .Include(a => a.AlunoMatriculado)
            .FirstOrDefaultAsync(p => p.AlunoMatriculadoId == id);
        }
        public async Task<PagamentoPropina> ObterPagamentoPropinaPeloAlunoAnoLetivo(Guid id,string anoLetivo)
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                .Include(a => a.Propinas)
                .Include(a => a.AlunoMatriculado)
            .FirstOrDefaultAsync(p => p.AlunoMatriculadoId == id && p.AlunoMatriculado.AnoLetivo == anoLetivo);
        }
        public async Task<PagamentoPropina> ObterPagamentoPropinaPeloAlunoMatriculado(Guid alunoMatriculadoId)
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                .FirstOrDefaultAsync(p => p.AlunoMatriculadoId == alunoMatriculadoId);
        }
        public async Task<IEnumerable<PagamentoPropina>> ObterPagamentoPropinasAlunoMatriculados()
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                //.Include(a => a.Propinas)
                .Include(a => a.AlunoMatriculado)
                .OrderBy(p => p.Codigo).ToListAsync();
        }
        public async Task<IEnumerable<PagamentoPropina>> ObterEstatisticaDePagamentos()
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                .Include(a => a.AlunoMatriculado)
                .Include(a => a.AlunoMatriculado.NiveisDeEnsino)

                .ToListAsync();
        }

        public async Task<IEnumerable<PagamentoPropina>> ObterPagamentoPropinasPorAlunoMatriculado(Guid alunoMatriculadoId)
        { return await Buscar(p => p.AlunoMatriculadoId == alunoMatriculadoId); }
     

        public async Task<PagamentoPropina> ObterPagamentoPropinaPropinasAlunoMatriculado(Guid id)
        {
            return await Db.PagamentoPropinas.AsNoTracking()
                .Include(pi => pi.Propinas)
                .Include(c => c.AlunoMatriculado)
                  .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}

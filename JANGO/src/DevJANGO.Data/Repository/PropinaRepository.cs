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
    public class PropinaRepository : Repository<Propina>, IPropinaRepository
    {
        private readonly JangoDbContext _context;
        public PropinaRepository(JangoDbContext context) : base(context) { _context = context; }
        /* EF Relations, Lado MUITO na Entidade */
        public async Task<Propina> ObterPropinasPeloPagamento(Guid pagamentoPropinaId)
        {
            return await Db.Propinas.AsNoTracking()
            .Include(a => a.AlunoMatriculado)
               .FirstOrDefaultAsync(pi => pi.PagamentoPropinaId == pagamentoPropinaId);
        }
        public async Task<Propina> ObterPropinaAlunoMatriculado(Guid id)
        {
            return await Db.Propinas.Where(a => ((int)a.Situacao) == 4).AsNoTracking()
               .Include(a => a.AlunoMatriculado)
               .Include(m => m.Mes)
               .Include(m => m.Classe)
               .Include(m => m.Turma)
               .Include(m => m.PagamentoPropina)
                    .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Propina> ObterPropinaPaga(Guid id)
        {
            return await Db.Propinas.Where(a => ((int)a.Situacao) == 1).AsNoTracking()
               .Include(m => m.Mes)
               //.Include(m => m.Classe)
               //.Include(m => m.Turma)
               //.Include(m => m.PagamentoPropina)
                    .FirstOrDefaultAsync(p => p.PagamentoPropinaId == id);
        }
        public async Task<Propina> ObterPropinaPeloAluno(Guid idAluno)
        {
            return await Db.Propinas.AsNoTracking()
                 .Include(a => a.AlunoMatriculado)
                  .Include(m => m.Mes)
                  .Include(m => m.Classe)
                  .Include(m => m.Turma)
                  .Include(m => m.PagamentoPropina)
                    .FirstOrDefaultAsync(p => p.AlunoMatriculadoId == idAluno);
        }
        public async Task<Propina> ObterPropinaPeloAlunoAnoLetivo(Guid idAluno, string anoLetivo)
        {
            return await Db.Propinas.AsNoTracking()
                 .Include(a => a.AlunoMatriculado)
                  .Include(m => m.Mes)
                  .Include(m => m.Classe)
                  .Include(m => m.Turma)
                  .Include(m => m.PagamentoPropina)
                    .FirstOrDefaultAsync(p => p.AlunoMatriculadoId == idAluno && p.AnoLetivo == anoLetivo);
        }
        
        public async Task<IEnumerable<Propina>> ObterPropinasPagamentoPropina(Guid pagamentoPropinaId)
        {
            return await Db.Propinas.Where(pi => pi.PagamentoPropinaId == pagamentoPropinaId).AsNoTracking()
          .Include(m => m.Mes)
          .OrderBy(a => a.Mes.NomeMes).ToListAsync();
        }

        public async Task<Propina> ObterPropina(Guid idAluno)
        {
            return await Db.Propinas.AsNoTracking()
                .Include(a => a.AlunoMatriculado)
                    .FirstOrDefaultAsync(p => p.AlunoMatriculadoId == idAluno);
        }
       
        public async Task<IEnumerable<Propina>> ObterPropinasPorPagamento(Guid pagamentoPropinaId)
        {
            return await Db.Propinas.Where(pi => pi.PagamentoPropinaId == pagamentoPropinaId).AsNoTracking()
          .Include(m => m.Mes)
          .Include(m => m.Classe)
          .Include(m => m.Turma)
          .OrderBy(a => a.Mes.CodMes).ToListAsync();
        }
        public async Task<IEnumerable<Propina>> ObterPropinaMesAlunoMatriculadoClasse()
        {
            return await Db.Propinas.Where(a => ((int)a.Situacao) == 4).AsNoTracking()
            .Include(a => a.AlunoMatriculado)
            .Include(m => m.Mes)
            .OrderBy(a => a.Mes.CodMes).ToListAsync();
        }

        public async Task<IEnumerable<Propina>> ObterPropinaAluno(Guid idAluno)
        {
            return await Db.Propinas.Where(p => p.AlunoMatriculadoId == idAluno).AsNoTracking()
                .Include(a => a.AlunoMatriculado)
            .OrderBy(a => a.Mes.CodMes).ToListAsync();
        }
       
        public async Task<IEnumerable<Propina>> PropinaMesAlunoMatriculadoClasseTurma()
        {
            return await Db.Propinas.AsNoTracking()
            .Include(a => a.AlunoMatriculado)
            .Include(m => m.Mes)
            .Include(c => c.Classe)
            .Include(t => t.Turma)
                    .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Propina>> ObterPropinasPorMesAlunoMatriculadoClasseTurma(Guid mesId)
        {
            return await Buscar(m => m.MesId == mesId);
        }
        public async Task<IEnumerable<Propina>> ObterPropinasPorAlunoMatriculadoMesClasseTurma(Guid alunoMatriculadoId)
        {
            return await Buscar(a => a.AlunoMatriculadoId == alunoMatriculadoId);
        }
        public async Task<IEnumerable<Propina>> ObterPropinasPorClasseMesAlunoMatriculadoTurma(Guid classeId)
        {
            return await Buscar(c => c.ClasseId == classeId);
        }
        public async Task<IEnumerable<Propina>> ObterPropinasPorTurmaMesAlunoMatriculadoClasse(Guid turmaId)
        {
            return await Buscar(t => t.TurmaId == turmaId);
        }
    }
}

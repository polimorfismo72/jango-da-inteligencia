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
    public class TurmaRepository : Repository<Turma>, ITurmaRepository
    {
        public TurmaRepository(JangoDbContext context) : base(context) { }

        /* EF Relations, Lado MUITO na Entidade */
        public async Task<Turma> ObterTurma(Guid id)
        {
            return await Db.Turmas.AsNoTracking()
           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Turma> ObterTurmaClasse(Guid id)
        {
            return await Db.Turmas.AsNoTracking()
                .Include(n => n.Classe)
           .FirstOrDefaultAsync(a => a.Id == id);
        }
     
        public async Task<IEnumerable<Turma>> ObterTurmasClassesAreaDeConhecimentos()
        {
            return await Db.Turmas.AsNoTracking()
                .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.Classe.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasIniciacao()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "Iniciação").AsNoTracking()
                .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasEnsinoPrimario()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "Primário").AsNoTracking()
                 .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasEtapaUm()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "I Ciclo-P").AsNoTracking()
                 .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasEtapaDois()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "II Ciclo-P").AsNoTracking()
                 .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasEtapaTres()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "IIICiclo-P").AsNoTracking()
                 .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasICiclo()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "I Ciclo").AsNoTracking()
                 .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento) 
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasIICicloFb()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "IICiclo FB").AsNoTracking()
           .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
                .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmasIICicloEj()
        {
            return await Db.Turmas.Where(n => n.AreaDeConhecimento.Nome == "IICiclo EJ").AsNoTracking()
                  .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmaClasses()
        {
            return await Db.Turmas.AsNoTracking().Include(e => e.Classe)
                .OrderBy(a => a.NomeTurma).ToListAsync();
        }
        public async Task<IEnumerable<Turma>> ObterTurmaPorClasses(Guid classeId)
        {
            return await Buscar(a => a.ClasseId == classeId);
        }

        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado UM na Entidade */
        public async Task<Turma> ObterTurmaAlunoMatriculadosClasse(Guid id)
        {
            return await Db.Turmas.AsNoTracking()

                .Include(n => n.AlunoMatriculados)
                 .Include(e => e.Avaliacaos)
                .Include(c => c.Classe)

           .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Turma> ObterTurmaClasseAreaDeConhecimento(Guid id)
        {
            return await Db.Turmas.AsNoTracking()
                .Include(c => c.Classe)
                .Include(n => n.AreaDeConhecimento)
           .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Turma> ObterTurmaPropinasClasse(Guid id)
        {
            return await Db.Turmas.AsNoTracking()

                .Include(n => n.AlunoMatriculados)
                 .Include(e => e.Avaliacaos)
                .Include(c => c.Classe)
                .Include(c => c.Propinas)

           .FirstOrDefaultAsync(a => a.Id == id);
        } 
    }
}

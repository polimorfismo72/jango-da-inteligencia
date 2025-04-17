using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class ProfessorDisciplinaClasseRepository : Repository<ProfessorDisciplinaClasse>, IProfessorDisciplinaClasseRepository
    {
        public ProfessorDisciplinaClasseRepository(JangoDbContext context) : base(context){ }
      

        public async Task<ProfessorDisciplinaClasse> ObterProfessorPorDisciplinaClasseProfessor(Guid id)
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking()
                .Include(f => f.Classe)
                .Include(f => f.Disciplina)
                .Include(a => a.Professor)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClassePorProfessor(Guid id)
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking()
                .Include(a => a.Professor)
                .FirstOrDefaultAsync(a => a.ProfessorId == id);
        }
        public async Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClasseProfessor(Guid id)
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking()
                .Include(f => f.Classe)
                .Include(f => f.Disciplina)
                .Include(a => a.Professor)
                .FirstOrDefaultAsync(a => a.ProfessorId == id);
        }
        public async Task<ProfessorDisciplinaClasse> ObterNomePorDisciplina(Guid id)
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking()
                .Include(f => f.Disciplina)
                .FirstOrDefaultAsync(a => a.DisciplinaId == id);
        }
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesProfessores()
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking()
                .Include(a => a.Professor)
                .OrderBy(a => a.Professor.Nome).Distinct().ToListAsync();
        }
      
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorProfessor(Guid professorId)
        { return await Buscar(a => a.ProfessorId == professorId); }

        public async Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClasseDisciplina(Guid id)
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking().Include(t => t.Disciplina)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesDisciplina()
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking().Include(t => t.Disciplina)
                 .OrderBy(a => a.Disciplina.NomeDisciplina).ToListAsync();
        }
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorProfessores(Guid professorId)
        {
            return await Db.ProfessorDisciplinaClasses.Where(pi => pi.ProfessorId == professorId).AsNoTracking()
          .Include(m => m.Disciplina)
          .Include(m => m.Classe)
          .OrderBy(a => a.Professor.Nome).ToListAsync();
        }
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorDisciplina(Guid disciplinaId)
        { return await Buscar(t => t.DisciplinaId == disciplinaId); }

        public async Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClasseClasse(Guid id)
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking().Include(f => f.Classe)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesClasse()
        {
            return await Db.ProfessorDisciplinaClasses.AsNoTracking().Include(t => t.Classe)
              .OrderBy(a => a.Classe.Nome).ToListAsync();
        }
        public async Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorClasse(Guid classeId)
        { return await Buscar(t => t.ClasseId == classeId); }
     

    }
}

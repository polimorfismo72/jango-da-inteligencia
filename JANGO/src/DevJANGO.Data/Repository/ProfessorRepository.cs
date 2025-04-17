using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace DevJANGO.Data.Repository
{
    public class ProfessorRepository : Repository<Professor>, IProfessorRepository
    {
        private readonly JangoDbContext _context;
        public ProfessorRepository(JangoDbContext context) : base(context) { _context = context; }

        public async Task<Professor> ObterProfessor(Guid id)
        {
            return await Db.Professores.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Professor> ObterProfessorPeloTelefone(string telefoneBI)
        {
            return await Db.Professores.AsNoTracking()
                .FirstOrDefaultAsync(c => c.BI == telefoneBI || c.Telefone == telefoneBI);
        }
        public async Task<Professor> ObterProfessorDisciplina(Guid id)
        {
            return await Db.Professores.AsNoTracking()
                .Include(f => f.ProfessorDisciplinaClasses)
                
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Professor> ObterDisciplinasEClassesDoProfessor(Guid id)
        {
            return await Db.Professores.AsNoTracking()
               //.Include(c => c.Disciplina)
               //.Include(c => c.Classe)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Professor>> ObterTodosProfessores()
        {
            return await Db.Professores.AsNoTracking()
             .OrderBy(p => p.Nome).ToListAsync();
        }
        
        public async Task<IEnumerable<Professor>> ObterProfessoresDisciplinas()
        {
            return await Db.Professores.AsNoTracking() 
                .OrderBy(p => p.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Professor>> ObterProfessorPorId(Guid id)
        {
            return await Db.Professores.Where(pi => pi.Id == id).AsNoTracking()
          .OrderBy(a => a.Nome).ToListAsync();
        }
    }
}

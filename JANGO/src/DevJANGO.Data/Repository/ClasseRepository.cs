using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace DevJANGO.Data.Repository
{
    public class ClasseRepository : Repository<Classe>, IClasseRepository
    {
        private readonly JangoDbContext _context;
        public ClasseRepository(JangoDbContext context) : base(context){ _context = context; }

        public async Task<Classe> ObterClasseId()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClasseIdEtapaUm()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I").AsNoTracking()
                 .FirstOrDefaultAsync();
        }

        public async Task<Classe> ObterClasseIdEtapaDois()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II").AsNoTracking()
                 .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClasseIdEtapaTres()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III").AsNoTracking()
               .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClassePeloPreco(Guid id)
        {
            return await Db.Classes.Where(n => n.Id == id).AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClassePelaTurma(Guid id)
        {
            return await Db.Classes.AsNoTracking()
                  .Include(n => n.Turmas).Where(n => n.Id == id)
                 .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterNomeClasse(Guid id)
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Classe> ObterClassePeloId(Guid id)
        {
            return await Db.Classes
                .Include(n=>n.NiveisDeEnsino)
                .Include(n=>n.Curso)
             .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Classe> ObterClassePorId(Guid id)
        {
            return await Db.Classes
             .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Classe> ObterClasseIdEnsinoPrimario()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClasseIdICiclo()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClasseIdIICiclo()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "II Ciclo").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<Classe> ObterClasse(Guid id)
        {
            return await Db.Classes.AsNoTracking()
           .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Classe> ObterClasseNiveisDeEnsinoCurso(Guid id)
        {
            return await Db.Classes.AsNoTracking()

                .Include(n => n.NiveisDeEnsino)
                .Include(c => c.Curso)

           .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Classe>> ObterClassesIniciacao()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                 .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesEnsinoPrimario()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                 .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesICiclo()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
                 .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesCursoFb()
        {
            return await Db.Classes.Where(n => n.Curso.Nome == "Ciências Fisicas e Biologicas").AsNoTracking()
                .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesCursoEj()
        {
            return await Db.Classes.Where(n => n.Curso.Nome == "Ciências Economicas e Jurídicas").AsNoTracking()
                .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIniciacao()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoPrimario()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEtapaUm()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I").AsNoTracking()
                 .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEtapaDois()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II").AsNoTracking()
                .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEtapaTres()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III").AsNoTracking()
                .Include(n => n.NiveisDeEnsino)
                .Include(n => n.Curso)
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoICiclo()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIICiclo()
        {
            return await Db.Classes.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "II Ciclo").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIICicloFb()
        {
            return await Db.Classes.Where(n => n.Curso.Nome == "Ciências Fisicas e Biologicas").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIICicloEj()
        {
            return await Db.Classes.Where(n => n.Curso.Nome == "Ciências Economicas e Jurídicas").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
    }
}

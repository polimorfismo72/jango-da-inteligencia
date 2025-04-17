using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class DisciplinaRepository : Repository<Disciplina>, IDisciplinaRepository
    {
        public DisciplinaRepository(JangoDbContext context) : base(context){ }
        public async Task<Disciplina> ObterDisciplina(Guid id)
        {
            return await Db.Disciplinas.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsino()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsinoEnsinoPrimario()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsinoICiclo()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsinoIICiclo()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "II Ciclo").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
        
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEtapaUm()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEtapaDois()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
        public async Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEtapaTres()
        {
            return await Db.Disciplinas.Where(n => n.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III").AsNoTracking()
                .OrderBy(a => a.NomeDisciplina).ToListAsync();
        }
    }
}

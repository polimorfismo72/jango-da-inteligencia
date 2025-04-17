using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class NiveisDeEnsinoRepository : Repository<NiveisDeEnsino>, INiveisDeEnsinoRepository
    {
        public NiveisDeEnsinoRepository(JangoDbContext context) : base(context){ }
        
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoId()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
          .FirstOrDefaultAsync();
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEnsinoPrimario()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Primário").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEtapaUm()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Etapa I").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEtapaDois()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Etapa II").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEtapaTres()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Etapa III").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdICiclo()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
           .FirstOrDefaultAsync();
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdIICiclo()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "II Ciclo").AsNoTracking()
           .FirstOrDefaultAsync();
        }
     
        public async Task<NiveisDeEnsino> ObterNiveis(string nome)
        {
            return await Db.NiveisDeEnsinos.AsNoTracking()
           .FirstOrDefaultAsync(c => c.NomeNiveisDeEnsino == nome);
        }
        public async Task<NiveisDeEnsino> ObterNiveisDeEnsino(Guid id)
        {
            return await Db.NiveisDeEnsinos.AsNoTracking()
           .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<NiveisDeEnsino>ObterClasseNiveisDeEnsinoCurso(Guid id)
        {
            return await Db.NiveisDeEnsinos.AsNoTracking()
           .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinos()
        {
            return await Db.NiveisDeEnsinos
                //.Where(n => n.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                .OrderBy(a => a.NomeNiveisDeEnsino).ToListAsync();
        }
        public async Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoIniciacao()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Iniciação").AsNoTracking()
                .OrderBy(a => a.NomeNiveisDeEnsino).ToListAsync();
        }
        public async Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoEnsinoPrimario()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "Primário").AsNoTracking()
                .OrderBy(a => a.NomeNiveisDeEnsino).ToListAsync();
        }
        public async Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoICiclo()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "I Ciclo").AsNoTracking()
                .OrderBy(a => a.NomeNiveisDeEnsino).ToListAsync();
        }
        public async Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoIICiclo()
        {
            return await Db.NiveisDeEnsinos.Where(n => n.NomeNiveisDeEnsino == "II Ciclo").AsNoTracking()
                .OrderBy(a => a.NomeNiveisDeEnsino).ToListAsync();
        }

    }
}

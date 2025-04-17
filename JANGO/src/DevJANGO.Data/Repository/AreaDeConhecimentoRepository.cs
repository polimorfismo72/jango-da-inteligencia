using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class AreaDeConhecimentoRepository : Repository<AreaDeConhecimento>, IAreaDeConhecimentoRepository
    {
        public AreaDeConhecimentoRepository(JangoDbContext context) : base(context){ }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoId()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "Iniciação").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEnsinoPrimario()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "Primário").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEtapaUm()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "I Ciclo-P").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEtapaDois()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "II Ciclo-P").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEtapaTres()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "IIICiclo-P").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdICiclo()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "I Ciclo").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdIICicloFB()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "IICiclo FB").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public async Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdIICicloEJ()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "IICiclo EJ").AsNoTracking()
         .FirstOrDefaultAsync();
        }
        public  async Task<AreaDeConhecimento> ObterAreaDeConhecimento(Guid id)
        {
            return await Db.AreaDeConhecimentos.AsNoTracking()
         .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoIniciacao()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "Iniciação").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEtapaUm()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "I Ciclo-P").AsNoTracking()
         .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEtapaDois()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "II Ciclo-P").AsNoTracking()
         .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEtapaTres()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "IIICiclo-P").AsNoTracking()
         .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEnsinoPrimario()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "Primário").AsNoTracking()
         .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoICiclo()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "I Ciclo").AsNoTracking()
         .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoIICicloFB()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "IICiclo FB").AsNoTracking()
       .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoIICicloEJ()
        {
            return await Db.AreaDeConhecimentos.Where(n => n.Nome == "IICiclo EJ").AsNoTracking()
        .OrderBy(a => a.Nome).ToListAsync();
        }
    }
}

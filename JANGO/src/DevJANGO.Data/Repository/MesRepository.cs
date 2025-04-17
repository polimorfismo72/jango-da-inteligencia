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
    public class MesRepository : Repository<Mes>, IMesRepository
    {
        public MesRepository(JangoDbContext context) : base(context) { }
        public async Task<IEnumerable<Mes>> ObterMes(){
            return await Db.Meses.AsNoTracking()
                 .OrderBy(a => a.CodMes).ToListAsync();
        }
        public async Task<Mes> ObterNomeMes(Guid id)
        {
            return await Db.Meses.AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Mes> ObterMesPeloCodigo(int codigo)
        {
            return await Db.Meses.AsNoTracking()
                   .FirstOrDefaultAsync(a => a.CodMes == codigo);
        }
    }

   
}

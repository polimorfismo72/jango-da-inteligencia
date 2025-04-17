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
    public class AplicaMultaRepository : Repository<AplicaMulta>, IAplicaMultaRepository
    {
        public AplicaMultaRepository(JangoDbContext context) : base(context) { }

        /* EF Relations, Lado MUITO na Entidade */
        public async Task<AplicaMulta> ObterMulta(Guid id)
        {
            return await Db.AplicaMultas.AsNoTracking()
           .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}

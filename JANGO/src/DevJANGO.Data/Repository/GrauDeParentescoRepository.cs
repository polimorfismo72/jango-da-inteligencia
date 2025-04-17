using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class GrauDeParentescoRepository : Repository<GrauDeParentesco>, IGrauDeParentescoRepository
    {
        public GrauDeParentescoRepository(JangoDbContext context) : base(context){ }
        public async Task<GrauDeParentesco> ObterGrauDeParentesco(Guid id)
        {
            return await Db.GrauDeParentescos.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        
    }
}

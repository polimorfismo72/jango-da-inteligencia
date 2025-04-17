using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace DevJANGO.Data.Repository
{
    public class EncarregadoRepository : Repository<Encarregado>, IEncarregadoRepository
    {
        private readonly JangoDbContext _context;
        public EncarregadoRepository(JangoDbContext context) : base(context){ _context = context; }
        public async Task<IEnumerable<Encarregado>> ObterTodosEncarregados()
        {
            return await Db.Encarregados.AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<Encarregado> ObterEncarregado(Guid id)
        {
            return await Db.Encarregados.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
      
        public async Task<Encarregado> ObterEncarregadoAlunosMatriculados(Guid id)
        {
            return await Db.Encarregados.AsNoTracking()
               .Include(c => c.AlunoMatriculados)
               .FirstOrDefaultAsync(c => c.Id == id);
        }
        
    }
}

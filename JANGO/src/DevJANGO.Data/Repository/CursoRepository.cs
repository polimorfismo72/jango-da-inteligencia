using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.Data.Repository
{
    public class CursoRepository : Repository<Curso>, ICursoRepository
    {
        public CursoRepository(JangoDbContext context) : base(context){ }
        public async Task<Curso> ObterCursoId()
        {
            return await Db.Cursos.AsNoTracking()
                .FirstOrDefaultAsync();
        }
        public async Task<Curso> ObterCursoPrimaroICiclo()
        {
            return await Db.Cursos.Where(n => n.Nome == "Sem Curso").AsNoTracking()
          .FirstOrDefaultAsync();
        }
        public async Task<Curso> ObterCursoIICicloFb()
        {
            return await Db.Cursos.Where(n => n.Nome == "Ciências Fisicas e Biologicas").AsNoTracking()
          .FirstOrDefaultAsync();
        }
        public async Task<Curso> ObterCursoIICicloEj()
        {
            return await Db.Cursos.Where(n => n.Nome == "Ciências Economicas e Jurídicas").AsNoTracking()
          .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Curso>> ObterClasseSemCursos()
        {
            return await Db.Cursos.Where(n => n.Nome == "Sem Curso").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        public async Task<IEnumerable<Curso>> ObterCursoIICicloFbiologica()
        {
            return await Db.Cursos.Where(n => n.Nome == "Ciências Fisicas e Biologicas").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
        
        public async Task<IEnumerable<Curso>> ObterCursoIICicloEjuridica()
        {
            return await Db.Cursos.Where(n => n.Nome == "Ciências Economicas e Jurídicas").AsNoTracking()
                .OrderBy(a => a.Nome).ToListAsync();
        }
    }
}

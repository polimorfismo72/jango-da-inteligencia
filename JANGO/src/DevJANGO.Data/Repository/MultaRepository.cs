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
    public class MultaRepository : Repository<Multa>, IMultaRepository
    {
        public MultaRepository(JangoDbContext context) : base(context) { }
        public async Task<Multa> ObterMultaAlunoMatriculado(Guid id)
        {
            return await Db.Multas.AsNoTracking().Include(a => a.AlunoMatriculado)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Multa>> ObterMultasAlunoMatriculadoes()
        {
            return await Db.Multas.AsNoTracking().Include(a => a.AlunoMatriculado)
                .OrderBy(a => a.AlunoMatriculado.Nome).ToListAsync();
        }

        public async Task<IEnumerable<Multa>> ObterMultasPorAlunoMatriculado(Guid alunoMatriculadoId)
        { return await Buscar(p => p.AlunoMatriculadoId == alunoMatriculadoId); }

        public async Task<IEnumerable<Multa>> ObterMultasPorMesAlunoMatriculado(Guid mesId)
        { return await Buscar(m => m.MesId == mesId); }

        public async Task<IEnumerable<Multa>> ObterMultasPorTurmaClasseMesAlunoMatriculado(Guid turmaId)
        { return await Buscar(t => t.TurmaId == turmaId); }

    }
}

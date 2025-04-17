using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace DevJANGO.Data.Repository
{
    public class FuncionarioCaixaRepository : Repository<FuncionarioCaixa>, IFuncionarioCaixaRepository
    {
        private readonly JangoDbContext _context;
        public FuncionarioCaixaRepository(JangoDbContext context) : base(context){ _context = context; }

        public async Task<FuncionarioCaixa> ObterFuncionarioCaixa(Guid id)
        {
            return await Db.FuncionarioCaixas.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<FuncionarioCaixa> ObterFuncionario(string nome)
        {
            return await Db.FuncionarioCaixas.AsNoTracking()
                 .FirstOrDefaultAsync(c => c.Email == nome);
        }
    }
}

using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;


namespace DevJANGO.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly JangoDbContext _context;
        public ProdutoRepository(JangoDbContext context) : base(context) { _context = context; }

        //public ProdutoRepository(JangoDbContext context)
        //{
        //    _context = context;
        //}
        public async Task<Produto> ObterNomeProduto(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                 .FirstOrDefaultAsync(p => p.Id == id);
        }
  
        public async Task<Produto> ObterProdutoPrecoValorVenda(Guid id)
        {
            return await Db.Produtos.AsNoTracking()
                .Include(c => c.ValorVenda)
           .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> ObterProdutoPedidoItems(Guid id)
        {
            return await Db.Produtos.AsNoTracking().Include(p => p.PedidoItems)
             .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Produto> ObterProdutoPeloCodigo(int codigo)
        {
            return await Db.Produtos.AsNoTracking() 
           .FirstOrDefaultAsync(a => a.Codigo == codigo);
        }

        public async Task<Produto> ObterProdutoPorId(Guid id)
        {
            return await Db.Produtos
             .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<Produto> ObterPrecoProdutoPorId(Guid id)
        {
            return await Db.Produtos
                .Include(v=> v.ValorVenda)
             .FirstOrDefaultAsync(p => p.Id == id);
        }
   
    }
}

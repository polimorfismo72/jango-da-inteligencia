using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;

namespace DevJANGO.App.Queries
{
    public interface IProdutoQueries
    {
        Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null);
    }
    public class ProdutoQueries : IProdutoQueries
    {
        private readonly JangoDbContext _context;
        public ProdutoQueries(JangoDbContext context) { _context = context; }

        /*
        public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT * FROM Produtos 
              WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%') 
              ORDER BY [Nome] 
              OFFSET {pageSize * (pageIndex - 1)} ROWS 
              FETCH NEXT {pageSize} ROWS ONLY 
              SELECT COUNT(Id) FROM Produtos 
              WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
          .QueryMultipleAsync(sql, new { Nome = query });

            

            var produtos = multi.Read<ProdutoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<ProdutoViewModel>()
            {
                List = produtos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }
        */
        public async Task<PagedViewModel<ProdutoViewModel>> ObterTodos(int pageSize, int pageIndex, string query = null)
        {

            var sql = @$"SELECT * FROM Produtos 
              WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%') 
              ORDER BY [Nome] 
              OFFSET {pageSize * (pageIndex - 1)} ROWS 
              FETCH NEXT {pageSize} ROWS ONLY 
              SELECT COUNT(Id) FROM Produtos 
              WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var produtos = multi.Read<ProdutoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<ProdutoViewModel>()
            {
                List = produtos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }
    }
}

using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;

namespace DevJANGO.App.Queries
{
    public interface IProfessorQueries
    {
        Task<PagedViewModel<ProfessorViewModel>> ObterTodosProfessores(int pageSize, int pageIndex, string query = null);
    }
    public class ProfessorQueries : IProfessorQueries
    {
        private readonly JangoDbContext _context;
        public ProfessorQueries(JangoDbContext context) { _context = context; }

        public async Task<PagedViewModel<ProfessorViewModel>> ObterTodosProfessores(int pageSize, int pageIndex, string query = null)
        {

            var sql = @$"SELECT * FROM Professores 
              WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%') 
              ORDER BY [Nome] 
              OFFSET {pageSize * (pageIndex - 1)} ROWS 
              FETCH NEXT {pageSize} ROWS ONLY 
              SELECT COUNT(Id) FROM Professores 
              WHERE (@Nome IS NULL OR Nome LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var professores = multi.Read<ProfessorViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<ProfessorViewModel>()
            {
                List = professores,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query
            };
        }
    }
}

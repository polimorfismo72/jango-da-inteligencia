using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;

namespace DevJANGO.App.Queries
{
    public interface IProfessorDisciplinaClasseQueries
    {
        Task<PagedViewModel<ProfessorDisciplinaClasseViewModel>> ObterTodosProfessoresDisciplinaClasses(int pageSize, int pageIndex, string query = null);
    }
    public class ProfessorDisciplinaClasseQueries : IProfessorDisciplinaClasseQueries
    {
        private readonly JangoDbContext _context;
        public ProfessorDisciplinaClasseQueries(JangoDbContext context) { _context = context; }
        
       public async Task<PagedViewModel<ProfessorDisciplinaClasseViewModel>> ObterTodosProfessoresDisciplinaClasses(int pageSize, int pageIndex, string query = null)
        {

            var sql = @$"SELECT DISTINCT Pdc.ProfessorId AS 'ProfessorId',Pdc.NomeClasse AS NomeClasse ,P.Nome AS Nome, 
                       COUNT(Pdc.ProfessorId) AS DisciplinasAssociadas  FROM ProfessorDisciplinaClasses AS Pdc
                       INNER JOIN Professores AS P ON Pdc.ProfessorId = P.Id
                       WHERE (@Nome IS NULL OR P.Nome LIKE '%' + @Nome + '%')
                       GROUP BY ProfessorId,NomeClasse,Nome 
                       ORDER BY Nome 
                       OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                       FETCH NEXT {pageSize} ROWS ONLY 
                       SELECT COUNT(Pdc.ProfessorId)  FROM ProfessorDisciplinaClasses AS Pdc
                       INNER JOIN Professores AS P ON Pdc.ProfessorId = P.Id
                       WHERE (@Nome IS NULL OR P.Nome LIKE '%' + @Nome + '%')";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var professores = multi.Read<ProfessorDisciplinaClasseViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<ProfessorDisciplinaClasseViewModel>()
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

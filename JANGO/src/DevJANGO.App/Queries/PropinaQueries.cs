using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;


namespace DevJANGO.App.Queries
{
    public interface IPropinaQueries
    {
       Task<PagedViewModel<PropinaViewModel>> ObterAlunosComDividaPropinaPorTurma(string turmaNome);
    }
    public class PropinaQueries : IPropinaQueries
    {
        private readonly JangoDbContext _context;
        public PropinaQueries(JangoDbContext context)
        {
            _context = context;
        }
        
        public async Task<PagedViewModel<PropinaViewModel>> ObterAlunosComDividaPropinaPorTurma(string turmaNome = null)
        {
            const string sql = @"SELECT A.Nome As Nome,T.NomeTurma  As NomeDaTurma
                               ,C.Nome As NomeDaClasse,COUNT(P.ClasseId) AS Meses
                               ,((COUNT(P.ClasseId))*P.PrecoPropina) As PropinasEmAtraso
                               FROM  Propinas AS P
                               INNER JOIN Classes AS C ON P.ClasseId = C.Id
                               INNER JOIN Turmas AS T ON P.TurmaId = T.Id
                               INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
                               INNER JOIN Meses AS M ON P.MesId = M.Id
                               WHERE Situacao = 1 AND T.NomeTurma = @turmaNome
                               ORDER BY P.ClasseId,A.Nome,T.NomeTurma,C.Nome,P.PrecoPropina";

            /*
               const string sql = @"SELECT  A.Id, A.Nome
                               ,C.Nome As Classe,T.NomeTurma  As Turma
                               ,P.DescricaoPropina,P.PrecoPropina
                               ,P.Situacao FROM  Propinas AS P
                               INNER JOIN Classes AS C ON P.ClasseId = C.Id
                               INNER JOIN Turmas AS T ON P.TurmaId = T.Id
                               INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
                               INNER JOIN Meses AS M ON P.MesId = M.Id
                               WHERE Situacao = 1 AND T.NomeTurma = @turmaNome
                               ORDER BY A.Nome";
            */

            var pedido = await _context.Database.GetDbConnection()
            .QueryMultipleAsync(sql, new { turmaNome });
            var alunos = pedido.Read<PropinaViewModel>();
            return new PagedViewModel<PropinaViewModel>()
            {
                List = alunos,
                Query = turmaNome,
            };
        }
    }
}

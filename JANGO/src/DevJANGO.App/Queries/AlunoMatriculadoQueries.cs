using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;


namespace DevJANGO.App.Queries
{
    public interface IAlunoMatriculadoQueries
    {
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTurmaComAlunos(string turma);
        //Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterAlunosComDividaPropinaPorTurma(string turmaNome);
        //Task<PagedViewModel<PropinaViewModel>> ObterAlunosComDividaPropinaPorTurma(string turmaNome);

        #region ObterTodosEfetuados
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIniciacao(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosPrimario(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaUm(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaDois(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaTres(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosICiclo(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosMatriculaTurma(int pageSize, int pageIndex, string query = null);

        #endregion

        #region ObterTodosoPendente
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIniciacaoPendente(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosPrimarioPendente(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaUmPendente(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaDoisPendente(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaTresPendente(int pageSize, int pageIndex, string query = null);

        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosICicloPendente(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloFisicasBiologicaPendente(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloEconomicaJuridicaPendente(int pageSize, int pageIndex, string query = null);

        #endregion

        #region ObterTodosEncarregado
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoIniciacao(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEnsinoPrimario(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEtapaUm(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEtapaDois(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEtapaTres(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoICiclo(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null);
        #endregion
    }
    public class AlunoMatriculadoQueries : IAlunoMatriculadoQueries
    {
        private readonly JangoDbContext _context;
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        public AlunoMatriculadoQueries(JangoDbContext context,
           IAlunoMatriculadoRepository alunoMatriculadoRepository)
        {
            _alunoMatriculadoRepository= alunoMatriculadoRepository;
            _context = context;
        }

        #region ObterTodosEfetuados
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTurmaComAlunos(string turma)
        {
            const string sql = @"SELECT A.Id,
                               A.CodigoAluno,A.Nome,Ai.Datanascimento
                               ,A.Idade,A.Sexo,T.NomeTurma,C.Nome AS NomeClasse
                               FROM AlunoMatriculados AS A
                               INNER JOIN AlunoInscritos AS Ai ON A.AlunoInscritoId = Ai.Id
                               INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                               INNER JOIN Classes AS C ON A.ClasseId = C.Id
                               WHERE T.NomeTurma = @turma
                               ORDER BY A.Nome";

            var pedido = await _context.Database.GetDbConnection()
            .QueryMultipleAsync(sql, new { turma });

            var alunos = pedido.Read<AlunoMatriculadoViewModel>();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                Query = turma,
            };
        }
        //public async Task<PagedViewModel<PropinaViewModel>> ObterAlunosComDividaPropinaPorTurma(string turmaNome)
        //{
        //    const string sql = @"SELECT A.Nome ,T.NomeTurma  As Turma
        //                       ,C.Nome As Classe,COUNT(P.ClasseId) AS 'Meses'
        //                       ,((COUNT(P.ClasseId))*P.PrecoPropina) As PropinasEmAtraso
        //                       FROM  Propinas AS P
        //                       INNER JOIN Classes AS C ON P.ClasseId = C.Id
        //                       INNER JOIN Turmas AS T ON P.TurmaId = T.Id
        //                       INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
        //                       INNER JOIN Meses AS M ON P.MesId = M.Id
        //                       WHERE Situacao = 1 AND T.NomeTurma = @turmaNome
        //                       ORDER BY P.ClasseId,A.Nome,T.NomeTurma,C.Nome,P.PrecoPropina";

        //    /*
        //       const string sql = @"SELECT  A.Id, A.Nome
        //                       ,C.Nome As Classe,T.NomeTurma  As Turma
        //                       ,P.DescricaoPropina,P.PrecoPropina
        //                       ,P.Situacao FROM  Propinas AS P
        //                       INNER JOIN Classes AS C ON P.ClasseId = C.Id
        //                       INNER JOIN Turmas AS T ON P.TurmaId = T.Id
        //                       INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
        //                       INNER JOIN Meses AS M ON P.MesId = M.Id
        //                       WHERE Situacao = 1 AND T.NomeTurma = @turmaNome
        //                       ORDER BY A.Nome";
        //    */

        //    var pedido = await _context.Database.GetDbConnection()
        //    .QueryMultipleAsync(sql, new { turmaNome });
        //    var alunos = pedido.Read<PropinaViewModel>();
        //    return new PagedViewModel<PropinaViewModel>()
        //    {
        //        List = alunos,
        //        Query = turmaNome,
        //    };
        //}

        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIniciacao(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93' AND A.Estado = 1
                      ORDER BY [Nome] 
                        OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                       FETCH NEXT {pageSize} ROWS ONLY 
                       SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId  = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93' AND A.Estado = 1";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosPrimario(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E' AND A.Estado = 1";



            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaUm(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F' AND A.Estado = 1";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL

            //var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
            //           ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
            //           ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
            //           FROM  AlunoMatriculados AS A 
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //           INNER JOIN Turmas AS T ON A.TurmaId = T.Id
            //           WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
            //          AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'
            //          ORDER BY [Nome] 
            //          OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //          FETCH NEXT {pageSize} ROWS ONLY 
            //          SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //          WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
            //          AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'";



            var multi = await _context.Database.GetDbConnection()
               .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaDois(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B' AND A.Estado = 1";


            var multi = await _context.Database.GetDbConnection()
               .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaTres(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA' AND A.Estado = 1";

            
            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosICiclo(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7' AND A.Estado = 1";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8' AND A.Estado = 1";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL

            //var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
            //           ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
            //           ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
            //           FROM  AlunoMatriculados AS A 
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //           INNER JOIN Turmas AS T ON A.TurmaId = T.Id
            //           WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
            //          AND A.ClasseId = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8'
            //          ORDER BY [Nome] 
            //          OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //          FETCH NEXT {pageSize} ROWS ONLY 
            //          SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //          WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
            //          AND A.ClasseId  = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B' AND A.Estado = 1";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosMatriculaTurma(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B' AND A.Estado = 1
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B' AND A.Estado = 1";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }

        #endregion

        #region ObterTodosoPendente
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIniciacaoPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93' AND A.Estado = 0
                      ORDER BY [Nome] 
                        OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                       FETCH NEXT {pageSize} ROWS ONLY 
                       SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId  = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93' AND A.Estado = 0";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            /*
             var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId = C.Id  AND C.Id = 'D25EAC17-B270-44B4-8520-E15065FE3FC2'
                       ORDER BY [Nome] 
                       OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                       FETCH NEXT {pageSize} ROWS ONLY 
                       SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId  = C.Id  AND C.Id = 'D25EAC17-B270-44B4-8520-E15065FE3FC2'";
             */


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosPrimarioPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E' AND A.Estado = 0";



            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaUmPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F' AND A.Estado = 0";

            var multi = await _context.Database.GetDbConnection()
               .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaDoisPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B' AND A.Estado = 0";

            var multi = await _context.Database.GetDbConnection()
               .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEtapaTresPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA' AND A.Estado = 0";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosICicloPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7' AND A.Estado = 0";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL

            //var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
            //           ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
            //           ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
            //           FROM  AlunoMatriculados AS A 
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //           INNER JOIN Turmas AS T ON A.TurmaId = T.Id
            //           WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
            //          AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'
            //          ORDER BY [Nome] 
            //          OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //          FETCH NEXT {pageSize} ROWS ONLY 
            //          SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //          WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
            //          AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloFisicasBiologicaPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8' AND A.Estado = 0";
 
            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosIICicloEconomicaJuridicaPendente(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO

            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B' AND A.Estado = 0
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B' AND A.Estado = 0";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        #endregion

        #region ObterTodosEncarregado
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoIniciacao(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93'
                      ORDER BY [Nome] 
                        OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                       FETCH NEXT {pageSize} ROWS ONLY 
                       SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                       INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                       AND A.ClasseId  = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEnsinoPrimario(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEtapaUm(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%'OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'";

            var multi = await _context.Database.GetDbConnection()
               .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEtapaDois(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'";

            var multi = await _context.Database.GetDbConnection()
               .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoEtapaTres(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoICiclo(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = 'C238695B-86DA-4AB9-AB16-1D15D214A7D8'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoMatriculadoViewModel>> ObterTodosEncarregadoIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null)
        {
            var sql = @$"SELECT A.Id AS Id,A.CodigoAluno AS CodigoAluno,A.NumDocumento AS NumDocumento
                       ,C.Nome AS NomeClasse,T.NomeTurma AS NomeTurma,A.Nome AS Nome,A.Imagem AS Imagem 
                       ,A.Estado AS Estado,A.AnoLetivo AS AnoLetivo
                       FROM  AlunoMatriculados AS A 
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                       INNER JOIN Turmas AS T ON A.TurmaId = T.Id
                       WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B'
                      ORDER BY [Nome] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoMatriculados AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%' OR A.CodigoAluno LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.CursoId = '89CF9C89-D215-488E-884B-F1B1E8043E3B'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoMatriculadoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoMatriculadoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        #endregion

    }
}

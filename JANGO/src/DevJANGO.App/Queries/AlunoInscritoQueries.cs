//using DevJANGO.Data.Context;
//using Microsoft.EntityFrameworkCore;
//using Dapper;
//using DevJANGO.App.ViewModels;
//using System.Data.Common;
//using DevJANGO.Business.Intefaces;
//using DevJANGO.Business.Models;
//using DevJANGO.Data.Repository;
using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;

namespace DevJANGO.App.Queries
{
    //public interface IInscritoRepository : IRepository<AlunoInscrito>
    //{
    //    //Task<Pedido> ObterPorId(Guid id);
    //    //Task<IEnumerable<Pedido>> ObterListaPorClienteId(Guid clienteId);
    //    //void Adicionar(Pedido pedido);
    //    //void Atualizar(Pedido pedido);

    //    DbConnection ObterConexao();


    //    /* Pedido Item */
    //    //Task<PedidoItem> ObterItemPorId(Guid id);
    //    //Task<PedidoItem> ObterItemPorPedido(Guid pedidoId, Guid produtoId);
    //}
    public interface IAlunoInscritoQueries
    {
        //Task<IEnumerable<AlunoInscrito>> ObterLista();
        Task<IEnumerable<AlunoInscritoViewModel>> ObterLista();
        //Task<List<AlunoInscrito>> ObterT();
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosIniciacao(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosPrimario(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEtapaUm(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEtapaDois(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEtapaTres(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosICiclo(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null);
        
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoIniciacao(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEnsinoPrimario(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEtapaUm(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEtapaDois(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEtapaTres(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoICiclo(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null);
        Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null);
        
    }
    public class AlunoInscritoQueries : IAlunoInscritoQueries
    {
        //private readonly DbConnection _obterConexao;

        //private readonly IInscritoRepository _inscritoRepository;
        private readonly JangoDbContext _context;
        public AlunoInscritoQueries(JangoDbContext context
            //IInscritoRepository inscritoRepository,
            //DbConnection obterConexao
            ) { _context = context;
            //_inscritoRepository = inscritoRepository;
            //_obterConexao = obterConexao;
        }
        //public async Task<List<AlunoInscrito>> ObterT()
        //{
        //    string query = "SELECT * FROM AlunoInscritos";

        //    List<AlunoInscrito>
        //     //pedido = (await _inscritoRepository.ObterConexao()
        //     pedido = (await _obterConexao.QueryAsync<AlunoInscrito>(sql: query)).ToList();
        //    return pedido;
        //}

        public async Task<IEnumerable<AlunoInscritoViewModel>> ObterLista()
        {
            //Imagem,Nome,Nascimento,Lectivo,Classe,Estado
            string sql = @"SELECT a.Id,a.Imagem,a.Nome,a.Datanascimento,a.AnoLetivo,
                           c.Nome as NomeClasse,a.Estado FROM AlunoInscritos AS a
                         INNER JOIN NiveisDeEnsinos AS n ON a.NiveisDeEnsinoId = n.Id
                         INNER JOIN Classes AS c ON a.ClasseId = c.Id";
            var pedidos = await _context.Database.GetDbConnection()
                .QueryAsync<AlunoInscritoViewModel>(sql);

            return pedidos;
            /*
             *                     ////c.Nome as NomeClasse,
              a.AlunoInterno, c.Nome,a.ClasseId
               a.AreaDeConhecimentoId,  a.Codigo, a.DataCadastro,
               a.EncarregadoId, a.Endereco, a.EscolaDeOrgigem,
              a.FuncionarioCaixaId, a.GrauDeParentescoId, a.Idade, 
              a.NiveisDeEnsinoId,  a.NomeDaMae, a.NomeDoPai, a.NumDocumento, a.Sexo,
              a.TipoDocumento, a.ValorDaInscricao, n.Id, n.NomeNiveisDeEnsino, c.Id, 
              c.ClassDeExame, c.CursoId, c.NiveisDeEnsinoId, c.PrecoPropina
             */
        }
        public async Task<IEnumerable<AlunoInscritoViewModel>> ObterListay()
        {
            //Imagem,Nome,Nascimento,Lectivo,Classe,Estado
            string sql = @"SELECT a.Id,a.Imagem,a.Nome,a.Datanascimento,a.AnoLetivo,
                           c.Nome as NomeClasse,a.Estado FROM AlunoInscritos AS a
                         INNER JOIN NiveisDeEnsinos AS n ON a.NiveisDeEnsinoId = n.Id
                         INNER JOIN Classes AS c ON a.ClasseId = c.Id";
            var pedidos = await _context.Database.GetDbConnection()
                .QueryAsync<AlunoInscritoViewModel>(sql);

            return pedidos;
            /*
             *                     ////c.Nome as NomeClasse,
              a.AlunoInterno, c.Nome,a.ClasseId
               a.AreaDeConhecimentoId,  a.Codigo, a.DataCadastro,
               a.EncarregadoId, a.Endereco, a.EscolaDeOrgigem,
              a.FuncionarioCaixaId, a.GrauDeParentescoId, a.Idade, 
              a.NiveisDeEnsinoId,  a.NomeDaMae, a.NomeDoPai, a.NumDocumento, a.Sexo,
              a.TipoDocumento, a.ValorDaInscricao, n.Id, n.NomeNiveisDeEnsino, c.Id, 
              c.ClassDeExame, c.CursoId, c.NiveisDeEnsinoId, c.PrecoPropina
             */
        }

        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosIniciacao(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT AlunoInscritos.Id AS Id,AlunoInscritos.Nome AS 'NOME',
                      AlunoInscritos.Datanascimento AS Datanascimento,AlunoInscritos.Imagem AS Imagem,AlunoInscritos.Idade AS Idade,AlunoInscritos.Sexo AS Sexo, 
                      Classes.Nome AS NomeClasse,AlunoInscritos.EncarregadoId ,AlunoInscritos.Estado AS Estado
                      FROM AlunoInscritos 
                      INNER JOIN Classes ON AlunoInscritos.ClasseId = Classes.Id
                      WHERE (@Nome IS NULL OR AlunoInscritos.Nome LIKE '%' + @Nome + '%' OR NumDocumento LIKE '%' + @Nome + '%') AND AlunoInscritos.ClasseId = Classes.Id  AND Classes.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(AlunoInscritos.Id) FROM AlunoInscritos
                      INNER JOIN Classes ON AlunoInscritos.ClasseId = Classes.Id
                      WHERE (@Nome IS NULL OR AlunoInscritos.Nome LIKE '%' + @Nome + '%' OR NumDocumento LIKE '%' + @Nome + '%') AND AlunoInscritos.ClasseId  = Classes.Id  AND Classes.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93'";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT AlunoInscritos.Id AS Id,AlunoInscritos.Nome AS 'NOME',
            //          AlunoInscritos.Datanascimento AS Datanascimento,AlunoInscritos.Imagem AS Imagem,AlunoInscritos.Idade AS Idade,AlunoInscritos.Sexo AS Sexo, 
            //          Classes.Nome AS NomeClasse,AlunoInscritos.EncarregadoId ,AlunoInscritos.Estado AS Estado
            //          FROM AlunoInscritos 
            //          INNER JOIN Classes ON AlunoInscritos.ClasseId = Classes.Id
            //          WHERE (@Nome IS NULL OR AlunoInscritos.Nome LIKE '%' + @Nome + '%' OR NumDocumento LIKE '%' + @Nome + '%') AND AlunoInscritos.ClasseId = Classes.Id  AND Classes.Id = 'D25EAC17-B270-44B4-8520-E15065FE3FC2'
            //          ORDER BY [NOME] 
            //          OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //          FETCH NEXT {pageSize} ROWS ONLY 
            //          SELECT COUNT(AlunoInscritos.Id) FROM AlunoInscritos
            //          INNER JOIN Classes ON AlunoInscritos.ClasseId = Classes.Id
            //          WHERE (@Nome IS NULL OR AlunoInscritos.Nome LIKE '%' + @Nome + '%' OR NumDocumento LIKE '%' + @Nome + '%') AND AlunoInscritos.ClasseId  = Classes.Id  AND Classes.Id = 'D25EAC17-B270-44B4-8520-E15065FE3FC2'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosPrimario(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEtapaUm(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEtapaDois(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEtapaTres(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosICiclo(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoIniciacao(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') AND A.ClasseId = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') AND A.ClasseId  = C.Id  AND C.Id = '74B08060-6FD9-47BD-4843-08DCA977AE93'";


            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //          A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //          C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //          FROM AlunoInscritos AS A
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //          WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //          AND A.ClasseId = C.Id  AND C.Id = 'D25EAC17-B270-44B4-8520-E15065FE3FC2'
            //          ORDER BY [NOME] 
            //          OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //          FETCH NEXT {pageSize} ROWS ONLY 
            //          SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //          INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //          WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //          AND A.ClasseId  = C.Id  AND C.Id = 'D25EAC17-B270-44B4-8520-E15065FE3FC2'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEnsinoPrimario(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '7320579E-2461-4E10-A0EE-BF402DB1E64E'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEtapaUm(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '64E67D04-3A7A-42D4-AE1C-AEEFB0EED01F'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEtapaDois(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = 'BD6C00A0-533D-4636-8A57-046FBBDB365B'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoEtapaTres(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '293567AC-4832-4A6B-BEB5-EE2B147FBAFA'";

            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoICiclo(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.NiveisDeEnsinoId = '3BD50DA4-7435-4034-A5C3-2D0E8DD897C7'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoIICicloFisicasBiologica(int pageSize, int pageIndex, string query = null)
        {
            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.AreaDeConhecimentoId = 'ACA9CDBF-008C-46DE-BE9E-01C50C0F333A'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
        public async Task<PagedViewModel<AlunoInscritoViewModel>> ObterTodosEncarregadoIICicloEconomicaJuridica(int pageSize, int pageIndex, string query = null)
        {

            //// EM PRODUÇÃO- NO JANGO
            var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
                      A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
                      C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
                      FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId = C.Id AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'
                      ORDER BY [NOME] 
                      OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
                      FETCH NEXT {pageSize} ROWS ONLY 
                      SELECT COUNT(A.Id) FROM AlunoInscritos AS A
                      INNER JOIN Classes AS C ON A.ClasseId = C.Id
                      WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
                      AND A.ClasseId  = C.Id AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'";

            //// EM DESENVOLVIMENTO NA MAQUINA LOCAL
            //var sql = @$"SELECT A.Id AS Id,A.Nome AS 'NOME',A.AnoLetivo AS AnoLetivo,
            //            A.Datanascimento AS Datanascimento,A.Imagem AS Imagem,A.Idade AS Idade,A.Sexo AS Sexo, 
            //            C.Nome AS NomeClasse,A.EncarregadoId ,A.Estado AS Estado
            //            FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%') 
            //            AND A.ClasseId = C.Id  AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'
            //            ORDER BY [NOME] 
            //            OFFSET {pageSize * (pageIndex - 1)} ROWS                                         
            //            FETCH NEXT {pageSize} ROWS ONLY 
            //            SELECT COUNT(A.Id) FROM AlunoInscritos AS A
            //            INNER JOIN Classes AS C ON A.ClasseId = C.Id
            //            WHERE (@Nome IS NULL OR A.Nome LIKE '%' + @Nome + '%' OR A.NumDocumento LIKE '%' + @Nome + '%')
            //            AND A.ClasseId  = C.Id  AND A.AreaDeConhecimentoId = '181629BA-7B51-4F64-87AF-520FA4982D9D'";


            var multi = await _context.Database.GetDbConnection()
                .QueryMultipleAsync(sql, new { Nome = query });

            var alunos = multi.Read<AlunoInscritoViewModel>();
            var total = multi.Read<int>().FirstOrDefault();

            return new PagedViewModel<AlunoInscritoViewModel>()
            {
                List = alunos,
                TotalResults = total,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Query = query,
            };
        }
    }
}

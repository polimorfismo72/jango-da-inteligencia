using DevJANGO.Data.Context;
using Microsoft.EntityFrameworkCore;
using Dapper;
using DevJANGO.App.ViewModels;
//using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System.Data.Common;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Collections.Generic;
using System.Drawing.Printing;
using DevJANGO.Business.Models;
using DevJANGO.Data.Repository;


namespace DevJANGO.App.Queries
{
    public interface IPagamentoQueries
    {
        #region Estatistica Financeira
        Task<PagamentoPropinaViewModel> ObterTotalPagoNaMatricula(string anoLetivo);
        #endregion
    }
    public class PagamentoQueries : IPagamentoQueries
    {
        private readonly JangoDbContext _context;
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        public PagamentoQueries(JangoDbContext context,
           IAlunoMatriculadoRepository alunoMatriculadoRepository)
        {
            _alunoMatriculadoRepository= alunoMatriculadoRepository;
            _context = context;
        }

        #region Estatistica Financeira
        public async Task<PagamentoPropinaViewModel> ObterTotalPagoNaMatricula(string anoLetivo)
        {
            const string sql = @"SELECT SUM(TotalPago) AS TotalPago
                                FROM [JANGOBD].[dbo].[PagamentoPropinas] AS P
                                INNER JOIN AlunoMatriculados AS A ON P.AlunoMatriculadoId = A.Id
                                where P.Ativo = 1 AND A.AnoLetivo = @anoLetivo  
                                AND P.PagamentoMaticula = 1";

            var pedido = await _context.Database.GetDbConnection()
             .QueryAsync(sql, new { anoLetivo });
            return (PagamentoPropinaViewModel)pedido;
        }
        #endregion
        
    }
}

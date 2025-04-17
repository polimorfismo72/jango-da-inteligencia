using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IPagamentoPropinaRepository : IRepository<PagamentoPropina>
    {
        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado MUITO na Entidade PagamentoPropina */
      Task<PagamentoPropina> ObterPagamentoPropina(Guid id);
      Task<PagamentoPropina> ObterPagamentoPropinaPropinasAlunoMatriculado(Guid id);
      Task<PagamentoPropina> ObterPagamentoPropinaPeloAluno(Guid id);
      Task<PagamentoPropina> ObterPagamentoPropinaPeloAlunoAnoLetivo(Guid id, string anoLetivo);
      Task<PagamentoPropina> ObterPagamentoPropinaAlunoMatriculado(Guid id);
      Task<PagamentoPropina> ObterPagamentoPropinaPeloAlunoMatriculado(Guid alunoMatriculadoId);
      Task<IEnumerable<PagamentoPropina>> ObterPagamentoPropinasAlunoMatriculados();
        Task<IEnumerable<PagamentoPropina>> ObterEstatisticaDePagamentos();
      Task<IEnumerable<PagamentoPropina>> ObterPagamentoPropinasPorAlunoMatriculado(Guid alunoMatriculadoId);

    }
}

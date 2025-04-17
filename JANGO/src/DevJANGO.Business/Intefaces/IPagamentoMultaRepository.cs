using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IPagamentoMultaRepository : IRepository<PagamentoMulta>
    {
        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado MUITO na Entidade PagamentoMulta */
        Task<PagamentoMulta> ObterPagamentoMultaAlunoMatriculado(Guid id);
        Task<IEnumerable<PagamentoMulta>> ObterPagamentoMultasAlunoMatriculados();
        Task<IEnumerable<PagamentoMulta>> ObterPagamentoMultasPorAlunoMatriculado(Guid alunoMatriculadoId);

    }
}

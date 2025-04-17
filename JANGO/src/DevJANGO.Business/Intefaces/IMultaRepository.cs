using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IMultaRepository : IRepository<Multa>
    {
        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado MUITO na Entidade Multa */
        Task<Multa> ObterMultaAlunoMatriculado(Guid id);
        Task<IEnumerable<Multa>> ObterMultasAlunoMatriculadoes();
        Task<IEnumerable<Multa>> ObterMultasPorAlunoMatriculado(Guid alunoMatriculadoId);
        Task<IEnumerable<Multa>> ObterMultasPorMesAlunoMatriculado(Guid mesId);
        Task<IEnumerable<Multa>> ObterMultasPorTurmaClasseMesAlunoMatriculado(Guid turmaId);

    }
}

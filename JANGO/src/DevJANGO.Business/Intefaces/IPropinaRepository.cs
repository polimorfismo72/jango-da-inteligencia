using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IPropinaRepository : IRepository<Propina>
    {
        /* EF Relations, Lado MUITO na Entidade */
        Task<Propina> ObterPropinasPeloPagamento(Guid pagamentoPropinaId);
        Task<Propina> ObterPropinaPaga(Guid id);
        Task<Propina> ObterPropinaAlunoMatriculado(Guid id);
        Task<Propina> ObterPropina(Guid idAluno);
        Task<Propina> ObterPropinaPeloAluno(Guid idAluno);
        Task<Propina> ObterPropinaPeloAlunoAnoLetivo(Guid idAluno, string anoLetivo);
        Task<IEnumerable<Propina>> ObterPropinasPagamentoPropina(Guid pagamentoPropinaId);
        Task<IEnumerable<Propina>> ObterPropinaMesAlunoMatriculadoClasse();
        Task<IEnumerable<Propina>> PropinaMesAlunoMatriculadoClasseTurma();
        Task<IEnumerable<Propina>> ObterPropinasPorMesAlunoMatriculadoClasseTurma(Guid mesId);
        Task<IEnumerable<Propina>> ObterPropinaAluno(Guid idAluno);
        Task<IEnumerable<Propina>> ObterPropinasPorPagamento(Guid pagamentoPropinaId);
        Task<IEnumerable<Propina>> ObterPropinasPorAlunoMatriculadoMesClasseTurma(Guid alunoMatriculadoId);
        Task<IEnumerable<Propina>> ObterPropinasPorClasseMesAlunoMatriculadoTurma(Guid classeId);
        Task<IEnumerable<Propina>> ObterPropinasPorTurmaMesAlunoMatriculadoClasse(Guid turmaId);
    }
}

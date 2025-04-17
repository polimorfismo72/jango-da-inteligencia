using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IAvaliacaoRepository : IRepository<Avaliacao>
    {
        /* AlunoInscrito N  : 1 Encarregado  */
        /* EF Relations, Lado MUITO na Entidade */
        Task<Avaliacao> ObterAvaliacaoAlunoMatriculado(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosAlunoMatriculado();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorAlunoMatriculado(Guid alunoMatriculadoId);

        Task<Avaliacao> ObterAvaliacaoTipoAvaliacao(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosTipoAvaliacao();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorTipoAvaliacao(Guid tipoAvaliacaoId);

        Task<Avaliacao> ObterAvaliacaoTrimestre(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosTrimestre();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorTrimestre(Guid trimestreId);

        Task<Avaliacao> ObterAvaliacaoClasse(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosClasse();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorClasse(Guid classeId);

        Task<Avaliacao> ObterAvaliacaoTurma(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosTurma();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorTurma(Guid turmaId);

        Task<Avaliacao> ObterAvaliacaoDisciplina(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosDisciplina();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorDisciplina(Guid disciplinaId);

        Task<Avaliacao> ObterAvaliacaoProfessor(Guid id);
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosProfessor();
        Task<IEnumerable<Avaliacao>> ObterAvaliacaosPorProfessor(Guid professorId);


    }
}

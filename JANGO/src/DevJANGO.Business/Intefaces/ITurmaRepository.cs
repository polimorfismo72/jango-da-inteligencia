using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface ITurmaRepository : IRepository<Turma>
    {
        /* EF Relations, Lado MUITO na Entidade */
        Task<Turma> ObterTurma(Guid id);
        Task<Turma> ObterTurmaClasse(Guid id);
        Task<IEnumerable<Turma>> ObterTurmasClassesAreaDeConhecimentos();
        Task<IEnumerable<Turma>> ObterTurmasIniciacao();
        Task<IEnumerable<Turma>> ObterTurmasEnsinoPrimario();
        Task<IEnumerable<Turma>> ObterTurmasEtapaUm();
        Task<IEnumerable<Turma>> ObterTurmasEtapaDois();
        Task<IEnumerable<Turma>> ObterTurmasEtapaTres();
  
        Task<IEnumerable<Turma>> ObterTurmasICiclo();
        Task<IEnumerable<Turma>> ObterTurmasIICicloFb();
        Task<IEnumerable<Turma>> ObterTurmasIICicloEj();
        
        Task<IEnumerable<Turma>> ObterTurmaClasses();
        Task<IEnumerable<Turma>> ObterTurmaPorClasses(Guid classeId);
        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado UM na Entidade */
        Task<Turma> ObterTurmaClasseAreaDeConhecimento(Guid id);
        Task<Turma> ObterTurmaAlunoMatriculadosClasse(Guid id);
        Task<Turma> ObterTurmaPropinasClasse(Guid id);

    }
}

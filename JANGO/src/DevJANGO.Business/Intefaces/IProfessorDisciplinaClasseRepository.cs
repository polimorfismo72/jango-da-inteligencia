using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IProfessorDisciplinaClasseRepository : IRepository<ProfessorDisciplinaClasse>
    {
        /* AlunoInscrito N  : 1 Encarregado  */
        /* EF Relations, Lado MUITO na Entidade */
        Task<ProfessorDisciplinaClasse> ObterProfessorPorDisciplinaClasseProfessor(Guid id);
        Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClassePorProfessor(Guid id);
        Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClasseProfessor(Guid id);
        Task<ProfessorDisciplinaClasse> ObterNomePorDisciplina(Guid id);
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesProfessores();
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorProfessor(Guid professorId);
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorProfessores(Guid professorId);
        Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClasseDisciplina(Guid id);
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesDisciplina();
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorDisciplina(Guid disciplinaId);

        Task<ProfessorDisciplinaClasse> ObterProfessorDisciplinaClasseClasse(Guid id);
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesClasse();
        Task<IEnumerable<ProfessorDisciplinaClasse>> ObterProfessorDisciplinaClassesPorClasse(Guid classeId);
    }
}

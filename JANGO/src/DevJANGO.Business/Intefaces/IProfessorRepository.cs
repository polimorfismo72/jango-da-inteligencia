using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IProfessorRepository : IRepository<Professor>
    {
        Task<IEnumerable<Professor>> ObterTodosProfessores();
        Task<IEnumerable<Professor>> ObterProfessorPorId(Guid id);
        Task<Professor> ObterProfessor(Guid id);
        Task<Professor> ObterProfessorPeloTelefone(string telefoneBI);
        Task<Professor> ObterProfessorDisciplina(Guid id);
        Task<Professor> ObterDisciplinasEClassesDoProfessor(Guid id);
        Task<IEnumerable<Professor>> ObterProfessoresDisciplinas();
    }
}

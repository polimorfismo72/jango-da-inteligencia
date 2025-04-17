using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IDisciplinaRepository : IRepository<Disciplina>
    {
        Task<Disciplina> ObterDisciplina(Guid id);
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsino();
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsinoEnsinoPrimario();
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsinoICiclo();
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEnsinoIICiclo();
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEtapaUm();
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEtapaDois();
        Task<IEnumerable<Disciplina>> ObterDisciplinasNiveisDeEtapaTres();
        
        //Task<Disciplina> ObterDisciplinaProfessores(Guid id);
    }
}

using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface ICursoRepository : IRepository<Curso>
    {
        /* N  : 1  
            * Obter uma determinada Classe com o seu NiveisDeEnsino e Curso */
        
        Task<Curso> ObterCursoId();
        Task<Curso> ObterCursoPrimaroICiclo();
        Task<Curso> ObterCursoIICicloFb();
        Task<Curso> ObterCursoIICicloEj();
        Task<IEnumerable<Curso>> ObterClasseSemCursos();
        Task<IEnumerable<Curso>> ObterCursoIICicloFbiologica();
        Task<IEnumerable<Curso>> ObterCursoIICicloEjuridica();

    }
}

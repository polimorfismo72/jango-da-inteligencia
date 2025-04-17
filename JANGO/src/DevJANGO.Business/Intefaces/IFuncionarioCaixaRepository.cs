using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IFuncionarioCaixaRepository : IRepository<FuncionarioCaixa>
    {
        Task<FuncionarioCaixa> ObterFuncionarioCaixa(Guid id);
        Task<FuncionarioCaixa> ObterFuncionario(string nome);
        /* N  : 1  
            * Obter uma determinada Classe com o seu NiveisDeEnsino e Curso */
        //Task<Classe> ObterClasseNiveisDeEnsinoCurso(Guid id);
    }
}

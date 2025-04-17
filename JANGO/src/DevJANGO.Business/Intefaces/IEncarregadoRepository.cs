using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IEncarregadoRepository : IRepository<Encarregado>
    {
        Task<Encarregado> ObterEncarregado(Guid id);
        Task<Encarregado> ObterEncarregadoAlunosMatriculados(Guid id);
        Task<IEnumerable<Encarregado>> ObterTodosEncarregados();
        /* N  : 1  
            * Obter uma determinada Classe com o seu NiveisDeEnsino e Curso */
        //Task<Classe> ObterClasseNiveisDeEnsinoCurso(Guid id);
    }
}

using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IClasseRepository : IRepository<Classe>
    {
        /* N  : 1  
            * Obter uma determinada Classe com o seu NiveisDeEnsino e Curso */
        Task<Classe> ObterClasseId();
        Task<Classe> ObterClasseIdEtapaUm();
        Task<Classe> ObterClasseIdEtapaDois();
        Task<Classe> ObterClasseIdEtapaTres();
        Task<Classe> ObterClassePeloPreco(Guid id);
        Task<Classe> ObterClassePelaTurma(Guid id);
        Task<Classe> ObterClassePeloId(Guid id);
        Task<Classe> ObterClasseIdEnsinoPrimario();
        Task<Classe> ObterClasseIdICiclo();
        Task<Classe> ObterClasseIdIICiclo();
        Task<Classe> ObterNomeClasse(Guid id);
        Task<Classe> ObterClassePorId(Guid id);
        Task<Classe> ObterClasse(Guid id);
        Task<Classe> ObterClasseNiveisDeEnsinoCurso(Guid id);
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIniciacao();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEtapaUm();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEtapaDois();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEtapaTres();
        Task<IEnumerable<Classe>> ObterClassesEnsinoPrimario();
        Task<IEnumerable<Classe>> ObterClassesIniciacao();
        Task<IEnumerable<Classe>> ObterClassesICiclo();
        Task<IEnumerable<Classe>> ObterClassesCursoFb();
        Task<IEnumerable<Classe>> ObterClassesCursoEj();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoPrimario();
        //Task<IEnumerable<Classe>> ObterClassesNiveisEtapaUm();
        //Task<IEnumerable<Classe>> ObterClassesNiveisEtapaDois();
        //Task<IEnumerable<Classe>> ObterClassesNiveisEtapaTres();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoICiclo();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIICiclo();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIICicloFb();
        Task<IEnumerable<Classe>> ObterClassesNiveisDeEnsinoIICicloEj();
    }
}

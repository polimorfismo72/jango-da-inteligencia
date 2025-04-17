using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface INiveisDeEnsinoRepository : IRepository<NiveisDeEnsino>
    {
        
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoId();
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEnsinoPrimario();
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEtapaUm();
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEtapaDois();
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdEtapaTres(); 
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdICiclo();
        Task<NiveisDeEnsino> ObterNiveisDeEnsinoIdIICiclo();
        Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoIniciacao();
        Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoEnsinoPrimario();
        Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoICiclo();
        Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinoIICiclo();
        Task<IEnumerable<NiveisDeEnsino>> ObterNiveisDeEnsinos();
        Task<NiveisDeEnsino> ObterNiveis(string nome);
        Task<NiveisDeEnsino> ObterNiveisDeEnsino(Guid id);
        Task<NiveisDeEnsino> ObterClasseNiveisDeEnsinoCurso(Guid id);
        
    }
}

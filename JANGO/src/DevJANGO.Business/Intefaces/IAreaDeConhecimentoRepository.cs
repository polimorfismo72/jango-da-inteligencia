using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IAreaDeConhecimentoRepository : IRepository<AreaDeConhecimento>
    {
        
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoId();
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEnsinoPrimario();
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEtapaUm();
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEtapaDois();
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdEtapaTres();

        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdICiclo();
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdIICicloFB();
        Task<AreaDeConhecimento> ObterAreaDeConhecimentoIdIICicloEJ();
        Task<AreaDeConhecimento> ObterAreaDeConhecimento(Guid id);
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoIniciacao();
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEtapaUm();
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEtapaDois();
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEtapaTres();
        //Task<AreaDeConhecimento> ObterClasseNiveisDeEnsinoCurso(Guid id);
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoEnsinoPrimario();
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoICiclo();
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoIICicloFB();
        Task<IEnumerable<AreaDeConhecimento>> ObterAreaDeConhecimentoIICicloEJ();
    }
}

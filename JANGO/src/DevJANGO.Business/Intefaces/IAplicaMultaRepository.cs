using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IAplicaMultaRepository : IRepository<AplicaMulta>
    {
        /* EF Relations, Lado MUITO na Entidade */
        Task<AplicaMulta> ObterMulta(Guid id);
        
        //Task<IEnumerable<AplicaMultas>> ObterTurmaClasses();
        //Task<IEnumerable<AplicaMultas>> ObterTurmaPorClasses(Guid classeId);

    }
}

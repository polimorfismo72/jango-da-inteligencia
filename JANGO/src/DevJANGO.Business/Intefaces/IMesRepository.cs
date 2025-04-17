using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IMesRepository : IRepository<Mes>
    {
        /* AlunoInscrito N  : 1 Encarregado  */
        /* EF Relations, Lado MUITO na Entidade */
        Task<IEnumerable<Mes>> ObterMes();
        Task<Mes> ObterMesPeloCodigo(int codigo);
        Task<Mes> ObterNomeMes(Guid id);

    }
}

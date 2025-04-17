using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IGrauDeParentescoRepository : IRepository<GrauDeParentesco>
    {
        Task<GrauDeParentesco> ObterGrauDeParentesco(Guid id);
    }
}

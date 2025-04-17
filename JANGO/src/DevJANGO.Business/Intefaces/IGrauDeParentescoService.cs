using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IGrauDeParentescoService : IDisposable
    {
        Task Adicionar(GrauDeParentesco grauDeParentesco);
        Task Atualizar(GrauDeParentesco grauDeParentesco);
        Task Remover(Guid id);
    }
}
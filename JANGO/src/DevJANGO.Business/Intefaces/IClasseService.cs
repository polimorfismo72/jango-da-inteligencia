using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IClasseService : IDisposable
    {
        Task Adicionar(Classe classe);
        Task Atualizar(Classe classe);
        Task Remover(Guid id);
    }
}
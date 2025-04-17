using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IEncarregadoService : IDisposable
    {
        Task Adicionar(Encarregado encarregado);
        Task Atualizar(Encarregado encarregado);
        Task Remover(Guid id);
    }
}
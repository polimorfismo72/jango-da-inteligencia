using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IAvaliacaoService : IDisposable
    {
        Task Adicionar(Avaliacao avaliacao);
        Task Atualizar(Avaliacao avaliacao);
        Task Remover(Guid id);
    }
}
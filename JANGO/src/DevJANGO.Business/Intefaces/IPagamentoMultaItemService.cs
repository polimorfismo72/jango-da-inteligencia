using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IPagamentoMultaItemService : IDisposable
    {
        Task Adicionar(PagamentoMultaItem pagamentoMultaItem);
        Task Atualizar(PagamentoMultaItem pagamentoMultaItem);
        Task Remover(Guid id);
    }
}
using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IPagamentoMultaService : IDisposable
    {
        Task Adicionar(PagamentoMulta pagamentoMulta);
        Task Atualizar(PagamentoMulta pagamentoMulta);
        Task Remover(Guid id);
    }
}
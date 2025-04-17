using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IPagamentoPropinaService : IDisposable
    {
        Task Adicionar(PagamentoPropina pagamentoPropina);
        Task Atualizar(PagamentoPropina pagamentoPropina);
        Task Remover(Guid id);
    }
}
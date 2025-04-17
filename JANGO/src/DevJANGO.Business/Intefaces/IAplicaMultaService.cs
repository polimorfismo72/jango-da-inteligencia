using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IAplicaMultaService : IDisposable
    {
        Task Adicionar(AplicaMulta aplicaMulta);
        //Task Atualizar(Avaliacao avaliacao);
        //Task Remover(Guid id);
    }
}
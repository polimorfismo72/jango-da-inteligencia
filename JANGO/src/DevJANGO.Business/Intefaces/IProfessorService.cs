using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IProfessorService : IDisposable
    {
        Task Adicionar(Professor professor);
        Task Atualizar(Professor professor);
        Task Remover(Guid id);
    }
}
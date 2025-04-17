using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface ICursoService : IDisposable
    {
        Task Adicionar(Curso curso);
        Task Atualizar(Curso curso);
        Task Remover(Guid id);
    }
}
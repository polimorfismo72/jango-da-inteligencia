using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IAlunoInscritoService : IDisposable
    {
        Task Adicionar(AlunoInscrito alunoInscrito);
        Task Atualizar(AlunoInscrito alunoInscrito);
        Task Remover(Guid id);
    }
    public interface IAlunoInscritoIniciacaoService : IDisposable
    {
        Task Adicionar(AlunoInscrito alunoInscrito);
        Task Atualizar(AlunoInscrito alunoInscrito);
        void Dispose();
        Task Remover(Guid id);
    }
}
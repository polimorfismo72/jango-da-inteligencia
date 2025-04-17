using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IAlunoMatriculadoService : IDisposable
    {
        Task Adicionar(AlunoMatriculado alunoMatriculado);
        Task Atualizar(AlunoMatriculado alunoMatriculado);
        Task Remover(Guid id);
    }
}
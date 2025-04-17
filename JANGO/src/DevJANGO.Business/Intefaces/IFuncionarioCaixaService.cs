using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IFuncionarioCaixaService : IDisposable
    {
        Task Adicionar(FuncionarioCaixa funcionarioCaixa);
        Task Atualizar(FuncionarioCaixa funcionarioCaixa);
        Task Remover(Guid id);
    }
}
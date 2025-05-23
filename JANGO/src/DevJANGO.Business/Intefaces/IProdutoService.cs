﻿using System;
using System.Threading.Tasks;
using DevJANGO.Business.Models;

namespace DevJANGO.Business.Intefaces
{
    public interface IProdutoService : IDisposable
    {
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto);
        Task Remover(Guid id);
    }
}
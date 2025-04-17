using DevJANGO.Business.Models;
using DevJANGO.Business.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevJANGO.Business.Intefaces
{
    public interface IPagamentoMultaItemRepository : IRepository<PagamentoMultaItem>
    {
        /* AlunoMatriculado 1 : N Avaliacao */
        /* EF Relations, Lado MUITO na Entidade PagamentoPropina */

        Task<PagamentoMultaItem> ObterObterPagamentoMultaItemMulta(Guid id);
        Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemMultas();
        Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPorMulta(Guid multaId);

        Task<PagamentoMultaItem> ObterObterPagamentoMultaItemPagamentoMulta(Guid id);
        Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPagamentoMultas();
        Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPorPagamentoMulta(Guid pagamentoMultaId);

        Task<PagamentoMultaItem> ObterObterPagamentoMultaItemFuncionarioCaixa(Guid id);
        Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemFuncionarioCaixas();
        Task<IEnumerable<PagamentoMultaItem>> ObterObterPagamentoMultaItemPorFuncionarioCaixa(Guid funcionarioCaixaId);



    }
}

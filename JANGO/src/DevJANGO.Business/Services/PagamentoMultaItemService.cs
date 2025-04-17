using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class PagamentoMultaItemService : BaseService, IPagamentoMultaItemService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IPagamentoMultaItemRepository _pagamentoMultaItemRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PagamentoMultaItemService(IPagamentoMultaItemRepository pagamentoMultaItemRepository,
                                INotificador notificador) : base(notificador)
        {
            _pagamentoMultaItemRepository = pagamentoMultaItemRepository;
        }
        #endregion

        public async Task Adicionar(PagamentoMultaItem pagamentoMultaItem)
        {
            if (!ExecutarValidacao(new PagamentoMultaItemValidation(), pagamentoMultaItem)) return;

            await _pagamentoMultaItemRepository.Adicionar(pagamentoMultaItem);
        }

        public async Task Atualizar(PagamentoMultaItem pagamentoMultaItem)
        {
            if (!ExecutarValidacao(new PagamentoMultaItemValidation(), pagamentoMultaItem)) return;

            await _pagamentoMultaItemRepository.Atualizar(pagamentoMultaItem);
        }

        public async Task Remover(Guid id)
        {
            await _pagamentoMultaItemRepository.Remover(id);
        }

        public void Dispose()
        {
            _pagamentoMultaItemRepository?.Dispose();
        }
    }

}
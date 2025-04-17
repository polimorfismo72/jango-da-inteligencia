using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class PagamentoMultaService : BaseService, IPagamentoMultaService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IPagamentoMultaRepository _pagamentoMultaRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PagamentoMultaService(IPagamentoMultaRepository pagamentoMultaRepository,
                                INotificador notificador) : base(notificador)
        {
            _pagamentoMultaRepository = pagamentoMultaRepository;
        }
        #endregion

        public async Task Adicionar(PagamentoMulta pagamentoMulta)
        {
            if (!ExecutarValidacao(new PagamentoMultaValidation(), pagamentoMulta)) return;

            //if (_pagamentoMultaRepository.Buscar(a => a.NumeroDeTransacaoDePagamento == pagamentoMulta.NumeroDeTransacaoDePagamento).Result.Any())
            //{
            //    Notificar("O número de pagamento de multa já existe.");
            //    return;
            //}
            
            await _pagamentoMultaRepository.Adicionar(pagamentoMulta);
        }

        public async Task Atualizar(PagamentoMulta pagamentoMulta)
        {
            if (!ExecutarValidacao(new PagamentoMultaValidation(), pagamentoMulta)) return;
            //if (_pagamentoMultaRepository.Buscar(p=> p.NumeroDeTransacaoDePagamento == pagamentoMulta.NumeroDeTransacaoDePagamento && p.Id != pagamentoMulta.Id).Result.Any())
            //{
            //    Notificar("Já existe um pagamento de multa com este número infomado.");
            //    return;
            //}

            await _pagamentoMultaRepository.Atualizar(pagamentoMulta);
        }

        public async Task Remover(Guid id)
        {
            await _pagamentoMultaRepository.Remover(id);
        }

        public void Dispose()
        {
            _pagamentoMultaRepository?.Dispose();
        }
    }

}
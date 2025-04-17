using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class PagamentoPropinaService : BaseService, IPagamentoPropinaService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IPagamentoPropinaRepository _pagamentoPropinaRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PagamentoPropinaService(IPagamentoPropinaRepository pagamentoPropinaRepository,
                                INotificador notificador) : base(notificador)
        {
            _pagamentoPropinaRepository = pagamentoPropinaRepository;
        }
        #endregion

        public async Task Adicionar(PagamentoPropina pagamentoPropina)
        {
            if (!ExecutarValidacao(new PagamentoPropinaValidation(), pagamentoPropina)) return;

            //if (_pagamentoPropinaRepository.Buscar(e => e.Ativo == false).Result.Any())
            //{
            //    Notificar("Este aluno já possui um pagamento pendente. Deve regularizar antes pagamento!");
            //    return;
            //}
            if (_pagamentoPropinaRepository.Buscar(a => a.AlunoMatriculadoId == pagamentoPropina.AlunoMatriculadoId &&  a.Ativo == false).Result.Any())
            {
                //Notificar("O aluno infomado já possui um pagamento pendente.");
                return;
            }
            await _pagamentoPropinaRepository.Adicionar(pagamentoPropina);
        }

        public async Task Atualizar(PagamentoPropina pagamentoPropina)
        {
            if (!ExecutarValidacao(new PagamentoPropinaValidation(), pagamentoPropina)) return;

            //if (_pagamentoPropinaRepository.Buscar(e => e.NumeroDeTransacaoDePagamento == pagamentoPropina.NumeroDeTransacaoDePagamento && e.Id != pagamentoPropina.Id).Result.Any())
            //{
            //    Notificar("Já existe um aluno com este número de transação de pagamento infomado.");
            //    return;
            //}
          
            await _pagamentoPropinaRepository.Atualizar(pagamentoPropina);
        }

        public async Task Remover(Guid id)
        {
            await _pagamentoPropinaRepository.Remover(id);
        }

        public void Dispose()
        {
            _pagamentoPropinaRepository?.Dispose();
        }
    }

}
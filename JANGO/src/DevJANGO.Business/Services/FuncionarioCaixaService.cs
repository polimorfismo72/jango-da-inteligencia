using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class FuncionarioCaixaService : BaseService, IFuncionarioCaixaService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public FuncionarioCaixaService(IFuncionarioCaixaRepository funcionarioCaixaRepository,
                                INotificador notificador) : base(notificador)
        {
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
        }
        #endregion

        public async Task Adicionar(FuncionarioCaixa funcionarioCaixa)
        {
            if (!ExecutarValidacao(new FuncionarioCaixaValidation(), funcionarioCaixa)) return;

            if (_funcionarioCaixaRepository.Buscar(e => e.Email == funcionarioCaixa.Email).Result.Any())
            {
                Notificar("Já existe um Funcionario Caixa com este email infomado.");
                return;
            }

            await _funcionarioCaixaRepository.Adicionar(funcionarioCaixa);
        }

        public async Task Atualizar(FuncionarioCaixa funcionarioCaixa)
        {
            if (!ExecutarValidacao(new FuncionarioCaixaValidation(), funcionarioCaixa)) return;

            if (_funcionarioCaixaRepository.Buscar(e => e.Email == funcionarioCaixa.Email && e.Id != funcionarioCaixa.Id).Result.Any())
            {
                Notificar("Já existe um Funcionario Caixa com este emaile infomado.");
                return;
            }

            await _funcionarioCaixaRepository.Atualizar(funcionarioCaixa);
        }

        public async Task Remover(Guid id)
        {
            await _funcionarioCaixaRepository.Remover(id);
        }

        public void Dispose()
        {
            _funcionarioCaixaRepository?.Dispose();
        }
    }

}
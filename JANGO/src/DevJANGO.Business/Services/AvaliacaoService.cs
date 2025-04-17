using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class AvaliacaoService : BaseService, IAvaliacaoService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IAvaliacaoRepository _avaliacaoRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AvaliacaoService(IAvaliacaoRepository avaliacaoRepository,
                                INotificador notificador) : base(notificador)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }
        #endregion

        public async Task Adicionar(Avaliacao avaliacao)
        {
            if (!ExecutarValidacao(new AvaliacaoValidation(), avaliacao)) return;

            await _avaliacaoRepository.Adicionar(avaliacao);
        }

        public async Task Atualizar(Avaliacao avaliacao)
        {
            if (!ExecutarValidacao(new AvaliacaoValidation(), avaliacao)) return;
         
            await _avaliacaoRepository.Atualizar(avaliacao);
        }

        public async Task Remover(Guid id)
        {
            await _avaliacaoRepository.Remover(id);
        }

        public void Dispose()
        {
            _avaliacaoRepository?.Dispose();
        }
    }

}
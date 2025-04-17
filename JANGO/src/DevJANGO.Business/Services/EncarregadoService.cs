using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class EncarregadoService : BaseService, IEncarregadoService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IEncarregadoRepository _encarregadoRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public EncarregadoService(IEncarregadoRepository encarregadoRepository,
                                INotificador notificador) : base(notificador)
        {
            _encarregadoRepository = encarregadoRepository;
        }
        #endregion

        public async Task Adicionar(Encarregado encarregado)
        {
            if (!ExecutarValidacao(new EncarregadoValidation(), encarregado)) return;

            if (_encarregadoRepository.Buscar(e => e.Telefone == encarregado.Telefone).Result.Any())
            {
                Notificar("Já existe um encarregado com este telefone infomado.");
                return;
            }

            await _encarregadoRepository.Adicionar(encarregado);
        }

        public async Task Atualizar(Encarregado encarregado)
        {
            if (!ExecutarValidacao(new EncarregadoValidation(), encarregado)) return;

            if (_encarregadoRepository.Buscar(e => e.Telefone == encarregado.Telefone && e.Id != encarregado.Id).Result.Any())
            {
                Notificar("Já existe um encarregado com este telefone infomado.");
                return;
            }

            await _encarregadoRepository.Atualizar(encarregado);
        }

        public async Task Remover(Guid id)
        {
            await _encarregadoRepository.Remover(id);
        }

        public void Dispose()
        {
            _encarregadoRepository?.Dispose();
        }
    }

}
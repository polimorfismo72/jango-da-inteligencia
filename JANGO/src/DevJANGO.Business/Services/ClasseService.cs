using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class ClasseService : BaseService, IClasseService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IClasseRepository _classeRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ClasseService(IClasseRepository classeRepository,
                                INotificador notificador) : base(notificador)
        {
            _classeRepository = classeRepository;
        }
        #endregion

        public async Task Adicionar(Classe classe)
        {
            if (!ExecutarValidacao(new ClasseValidation(), classe)) return;

            await _classeRepository.Adicionar(classe);
        }

        public async Task Atualizar(Classe classe)
        {
            if (!ExecutarValidacao(new ClasseValidation(), classe)) return;
            await _classeRepository.Atualizar(classe);
        }

        public async Task Remover(Guid id)
        {
            await _classeRepository.Remover(id);
        }

        public void Dispose()
        {
            _classeRepository?.Dispose();
        }
    }

}
using System;
using System.Linq;
using System.Threading.Tasks;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Business.Models.Validations;

namespace DevJANGO.Business.Services
{

    public class GrauDeParentescoService : BaseService, IGrauDeParentescoService
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IGrauDeParentescoRepository _grauDeParentescoRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public GrauDeParentescoService(IGrauDeParentescoRepository grauDeParentescoRepository,
                                INotificador notificador) : base(notificador)
        {
            _grauDeParentescoRepository = grauDeParentescoRepository;
        }
        #endregion

        public async Task Adicionar(GrauDeParentesco grauDeParentesco)
        {
            if (!ExecutarValidacao(new GrauDeParentescoValidation(), grauDeParentesco)) return;

            await _grauDeParentescoRepository.Adicionar(grauDeParentesco);
        }

        public async Task Atualizar(GrauDeParentesco grauDeParentesco)
        {
            if (!ExecutarValidacao(new GrauDeParentescoValidation(), grauDeParentesco)) return;

            await _grauDeParentescoRepository.Atualizar(grauDeParentesco);
        }

        public async Task Remover(Guid id)
        {
            await _grauDeParentescoRepository.Remover(id);
        }

        public void Dispose()
        {
            _grauDeParentescoRepository?.Dispose();
        }
    }

}
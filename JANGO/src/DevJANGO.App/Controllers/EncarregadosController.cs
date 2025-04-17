using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using Microsoft.AspNetCore.Authorization;
using DevJANGO.Business.Models;
using DevJANGO.App.Extensions;

namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class EncarregadosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IEncarregadoRepository _encarregadoRepository;
        private readonly IEncarregadoService _encarregadoService;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public EncarregadosController(IEncarregadoRepository encarregadoRepository,
                                     IMapper mapper,
                                     IEncarregadoService encarregadoService,
                                     INotificador notificador) : base(notificador)
        {
            _encarregadoRepository = encarregadoRepository;
            _mapper = mapper;
            _encarregadoService = encarregadoService;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [AllowAnonymous]
        [Route("lista-liberada-de-encarregados")]
        public async Task<IActionResult> IndexEncarregado()
        {
            //ObterTodosEncarregados
            //return View(_mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos()));
            return View(_mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos()));
        }

        [AllowAnonymous]
        [Route("aviso-importante")]
        public IActionResult IndexAviso()
        {
            return View();
        }

        [ClaimsAuthorize("Encarregados", "VI")]
        [Route("lista-de-encarregados")]
        public async Task<IActionResult> Index()
        {
            //return View(_mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos()));  
            return View(_mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos()));
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [ClaimsAuthorize("Encarregados", "VI")]
        [Route("dados-do-encarregado/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var encarregadoViewModel = await ObterEncarregado(id);

            if (encarregadoViewModel == null)
            {
                return NotFound();
            }

            return View(encarregadoViewModel);
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [ClaimsAuthorize("Encarregados", "AD")]
        [Route("novo-encarregado")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Encarregados", "AD")]
        [HttpPost]
        [Route("novo-encarregado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EncarregadoViewModel encarregadoViewModel)
        {
            if (!ModelState.IsValid) return View(encarregadoViewModel);

            var encarregado = _mapper.Map<Encarregado>(encarregadoViewModel);
            await _encarregadoService.Adicionar(encarregado);
            //await _encarregadoRepository.Adicionar(encarregado);

            if (!OperacaoValida()) return View(encarregadoViewModel);
          
            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PELO ENCARREGADO
        [AllowAnonymous]
        [Route("liberado-para-novo-encarregado")]
        public IActionResult CreateEncarregado()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("liberado-para-novo-encarregado")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregado(EncarregadoViewModel encarregadoViewModel)
        {
            if (!ModelState.IsValid) return View(encarregadoViewModel);

            var encarregado = _mapper.Map<Encarregado>(encarregadoViewModel);
            await _encarregadoService.Adicionar(encarregado);
            if (!OperacaoValida()) return View(encarregadoViewModel);
            return RedirectToAction("IndexEncarregado");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        [ClaimsAuthorize("Encarregados", "AD")]
        [Route("editar-encarregado/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var encarregadoViewModel = await ObterEncarregadoAlunosMatriculados(id);

            if (encarregadoViewModel == null)
            {
                return NotFound();
            }

            return View(encarregadoViewModel);
        }

        [ClaimsAuthorize("Encarregados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("editar-encarregado/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, EncarregadoViewModel encarregadoViewModel)
        {
            if (id != encarregadoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(encarregadoViewModel);

            var encarregado = _mapper.Map<Encarregado>(encarregadoViewModel);
            await _encarregadoService.Atualizar(encarregado);
            //await _encarregadoRepository.Atualizar(encarregado);

             if (!OperacaoValida()) return View(await ObterEncarregadoAlunosMatriculados(id));

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        [ClaimsAuthorize("Encarregados", "DG")]
        [Route("excluir-encarregado/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var encarregadoViewModel = await ObterEncarregado(id);

            if (encarregadoViewModel == null)
            {
                return NotFound();
            }

            return View(encarregadoViewModel);
        }

        [ClaimsAuthorize("Encarregados", "DG")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-encarregado/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var encarregado = await ObterEncarregado(id);

            if (encarregado == null) return NotFound();

            await _encarregadoService.Remover(id);
            //await _encarregadoRepository.Remover(id);

            if (!OperacaoValida()) return View(encarregado);

            return RedirectToAction("Index");
        }
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<EncarregadoViewModel> ObterEncarregado(Guid id)
        {

            return _mapper.Map<EncarregadoViewModel>(await _encarregadoRepository.ObterEncarregado(id));
        }
        private async Task<EncarregadoViewModel> ObterEncarregadoAlunosMatriculados(Guid id)
        {
            return _mapper.Map<EncarregadoViewModel>(await _encarregadoRepository.ObterEncarregadoAlunosMatriculados(id));
        }
        #endregion
    }
}

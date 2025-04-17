using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevJANGO.App.Data;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;

namespace DevJANGO.App.Controllers
{
    public class GrauDeParentescosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IGrauDeParentescoRepository _grauDeParentescoRepository;
        private readonly IGrauDeParentescoService _grauDeParentescoService;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public GrauDeParentescosController(IGrauDeParentescoRepository grauDeParentescoRepository,
                                     IMapper mapper,
                                     IGrauDeParentescoService grauDeParentescoService,
                                     INotificador notificador) : base(notificador)
        {
            _grauDeParentescoRepository = grauDeParentescoRepository;
            _mapper = mapper;
            _grauDeParentescoService = grauDeParentescoService;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        //[AllowAnonymous]
        [Route("lista-de-grau-de-parentescos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos()));

        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        //[AllowAnonymous]
        [Route("dados-do-grau-de-parentesco/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var grauDeParentescoViewModel = await ObterGrauDeParentesco(id);

            if (grauDeParentescoViewModel == null)
            {
                return NotFound();
            }

            return View(grauDeParentescoViewModel);
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [Route("novo-grau-de-parentesco")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-grau-de-parentesco")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrauDeParentescoViewModel grauDeParentescoViewModel)
        {
            if (!ModelState.IsValid) return View(grauDeParentescoViewModel);

            var grauDeParentesco = _mapper.Map<GrauDeParentesco>(grauDeParentescoViewModel);
            await _grauDeParentescoService.Adicionar(grauDeParentesco);
            //await _grauDeParentescoRepository.Adicionar(grauDeParentesco);

            if (!OperacaoValida()) return View(grauDeParentescoViewModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        // [ClaimsAuthorize("grauDeParentesco", "Editar")]
        [Route("editar-grau-de-parentesco/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var grauDeParentescoViewModel = await ObterGrauDeParentesco(id);

            if (grauDeParentescoViewModel == null)
            {
                return NotFound();
            }

            return View(grauDeParentescoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("GrauDeParentesco", "Editar")]
        [Route("editar-grau-de-parentesco/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, GrauDeParentescoViewModel grauDeParentescoViewModel)
        {
            if (id != grauDeParentescoViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(grauDeParentescoViewModel);

            var grauDeParentesco = _mapper.Map<GrauDeParentesco>(grauDeParentescoViewModel);
            await _grauDeParentescoService.Atualizar(grauDeParentesco);
            //await _grauDeParentescoRepository.Atualizar(grauDeParentesco);

            if (!OperacaoValida()) return View(await ObterGrauDeParentesco(id));

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        //[ClaimsAuthorize("grauDeParentesco", "Excluir")]
        [Route("excluir-grau-de-parentesco/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var grauDeParentescoViewModel = await ObterGrauDeParentesco(id);

            if (grauDeParentescoViewModel == null)
            {
                return NotFound();
            }

            return View(grauDeParentescoViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize("GrauDeParentesco", "Excluir")]
        [Route("excluir-grau-de-parentesco/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grauDeParentesco = await ObterGrauDeParentesco(id);

            if (grauDeParentesco == null) return NotFound();

            await _grauDeParentescoService.Remover(id);
            //await _grauDeParentescoRepository.Remover(id);

            if (!OperacaoValida()) return View(grauDeParentesco);

            return RedirectToAction("Index");
        }
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<GrauDeParentescoViewModel> ObterGrauDeParentesco(Guid id)
        {
            return _mapper.Map<GrauDeParentescoViewModel>(await _grauDeParentescoRepository.ObterGrauDeParentesco(id));
        }
      
        #endregion
    }
}

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
using DevJANGO.Data.Repository;
using DevJANGO.App.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class FuncionarioCaixasController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;
        private readonly IFuncionarioCaixaService _funcionarioCaixaService;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public FuncionarioCaixasController(IFuncionarioCaixaRepository funcionarioCaixaRepository,
                                     IMapper mapper,
                                     IFuncionarioCaixaService funcionarioCaixaService,
                                     INotificador notificador) : base(notificador)
        {
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
            _mapper = mapper;
            _funcionarioCaixaService = funcionarioCaixaService;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [Route("lista-de-funcionario")]
        [ClaimsAuthorize("FuncionarioCaixa", "VI")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FuncionarioCaixaViewModel>>(await _funcionarioCaixaRepository.ObterTodos()));
        }

        [Route("area-do-funcionario")]
        [ClaimsAuthorize("FuncionarioCaixa", "VI")]
        public IActionResult Professor()
        {
            return View();
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [Route("dados-do-funcionario/{id:guid}")]
        [ClaimsAuthorize("FuncionarioCaixa", "VI")]
        public async Task<IActionResult> Details(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioCaixa(id);

            if (funcionarioViewModel == null)
            {
                return NotFound();
            }

            return View(funcionarioViewModel);
        }
        #endregion


        #region MÉTODO PARA CADASTRAR NOVO
        [Route("novo-funcionario")]
        [ClaimsAuthorize("FuncionarioCaixa", "AD")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-funcionario")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("FuncionarioCaixa", "AD")]
        public async Task<IActionResult> Create(FuncionarioCaixaViewModel funcionarioViewModel)
        {
            if (!ModelState.IsValid) return View(funcionarioViewModel);

            var funcionario = _mapper.Map<FuncionarioCaixa>(funcionarioViewModel);
            //await _funcionarioCaixaService.Adicionar(funcionario);
            await _funcionarioCaixaRepository.Adicionar(funcionario);

            if (!OperacaoValida()) return View(funcionarioViewModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        [Route("editar-funcionario/{id:guid}")]
        [ClaimsAuthorize("FuncionarioCaixa", "AD")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioCaixa(id);

            if (funcionarioViewModel == null)
            {
                return NotFound();
            }

            return View(funcionarioViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-funcionario/{id:guid}")]
        [ClaimsAuthorize("FuncionarioCaixa", "AD")]
        public async Task<IActionResult> Edit(Guid id, FuncionarioCaixaViewModel funcionarioViewModel)
        {
            if (id != funcionarioViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(funcionarioViewModel);

            var funcionario = _mapper.Map<FuncionarioCaixa>(funcionarioViewModel);
            //await _funcionarioCaixaService.Atualizar(funcionario);
            await _funcionarioCaixaRepository.Atualizar(funcionario);

            //if (!OperacaoValida()) return View(await ObterFuncionarioCaixa(id));

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        [Route("excluir-funcionario/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioCaixa(id);

            if (funcionarioViewModel == null)
            {
                return NotFound();
            }

            return View(funcionarioViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-funcionario/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var funcionarioViewModel = await ObterFuncionarioCaixa(id);

            if (funcionarioViewModel == null) return NotFound();

            await _funcionarioCaixaRepository.Remover(id);

            //if (!OperacaoValida()) return View(funcionarioCaixa);

            return RedirectToAction("Index");
        }
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<FuncionarioCaixaViewModel> ObterFuncionarioCaixa(Guid id)
        {
            return _mapper.Map<FuncionarioCaixaViewModel>(await _funcionarioCaixaRepository.ObterFuncionarioCaixa(id));
        }
       
        #endregion

    }
}

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
using DevJANGO.Data.Context;
using DevJANGO.Data.Repository;
using DevJANGO.Business.Models;
using DevJANGO.Data.Migrations;
using DevJANGO.Business.Services;
using DevJANGO.App.Extensions;

namespace DevJANGO.App.Controllers
{
    public class AplicaMultasController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IAplicaMultaRepository _aplicaMultaRepository;
        private readonly IAplicaMultaService _aplicaMultaService;
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AplicaMultasController(
            IAplicaMultaRepository aplicaMultaRepository,
            IAplicaMultaService aplicaMultaService,
            IFuncionarioCaixaRepository funcionarioCaixaRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _aplicaMultaRepository = aplicaMultaRepository;
            _aplicaMultaService = aplicaMultaService;
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA LISTAR
        [ClaimsAuthorize("AplicaMultas", "VI")]
        [Route("lista-multas")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<AplicaMultaViewModel>>(await _aplicaMultaRepository.ObterTodos()));
        }

        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        ////[AllowAnonymous]
        //[Route("dados/{id:guid}")]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var aluno = await ObterMulta(id);

        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aluno);
        //}
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [ClaimsAuthorize("AplicaMultas", "AD")]
        [Route("aplicar-nova-multa")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("AplicaMultas", "AD")]
        [Route("aplicar-nova-multa")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AplicaMultaViewModel aplicaMultaViewModel)
        {
            if (!ModelState.IsValid) return View(aplicaMultaViewModel);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (emailFuncionario == null)
            {
                TempData["Erro"] = "Opa! Este Funcionário não está logado :(";
                return RedirectToAction("Index");
            }
            DateTime agora = DateTime.Today;
            string mes = agora.ToString("MMMM");
            #endregion
         
            aplicaMultaViewModel.Nome = $"Aplicação da Multa Referente ao mês de {mes}";
            aplicaMultaViewModel.Usuario = emailFuncionario.Nome;

            await _aplicaMultaService.Adicionar(_mapper.Map<AplicaMulta>(aplicaMultaViewModel));

            if (!OperacaoValida()) return View(aplicaMultaViewModel);
            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR

        //[Route("editar/{id:guid}")]
        //public async Task<IActionResult> Edit(Guid id)
        //{
        //    var turma = await ObterTurma(id);

        //    if (turma == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(turma);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[ClaimsAuthorize("Fornecedor", "Editar")]
        //[Route("editar/{id:guid}")]
        //public async Task<IActionResult> Edit(Guid id, TurmaViewModel turmaViewModel)
        //{
        //    if (id != turmaViewModel.Id) return NotFound();

        //    var turmaAtualizacao = await ObterTurma(id);

        //    if (!ModelState.IsValid) return View(turmaViewModel);
        //    turmaAtualizacao.ClasseId = turmaViewModel.ClasseId;
        //    turmaAtualizacao.AreaDeConhecimentoId = turmaViewModel.AreaDeConhecimentoId;
        //    turmaAtualizacao.NomeTurma = turmaViewModel.NomeTurma;
        //    turmaAtualizacao.NumDeVagas = turmaViewModel.NumDeVagas;
        //    turmaAtualizacao.Estado = turmaViewModel.Estado;

        //    await _turmaRepository.Atualizar(_mapper.Map<Turma>(turmaAtualizacao));
        //    //await _turmaService.Atualizar(cliente);

        //    // if (!OperacaoValida()) return View(await ObterFornecedorTurmas(id));

        //    return RedirectToAction("Index");
        //}

        #endregion

        #region MÉTODO PARA EXCLUIR

        ////[ClaimsAuthorize("Fornecedor", "Excluir")]
        //[Route("excluir/{id:guid}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var turma = await ObterTurma(id);

        //    if (turma == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(turma);

        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        ////[ClaimsAuthorize("Fornecedor", "Excluir")]
        //[Route("excluir/{id:guid}")]
        ////[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var turma = await ObterTurma(id);

        //    if (turma == null) return NotFound();

        //    //await _turmaService.Remover(id);
        //    await _turmaRepository.Remover(id);

        //    //if (!OperacaoValida()) return View(turma);
        //    TempData["Sucesso"] = "Turma excluido com sucesso!";
        //    return RedirectToAction("Index");
        //}

        #endregion

        #region METODOS PRIVADOS DA CONTROLLER

        //private async Task<TurmaViewModel> ObterTurma(Guid id)
        //{
        //    turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _aplicaMultasRepository.ObterTodos());

        //    return turma;
        //}
       
      

        #endregion
    }
}

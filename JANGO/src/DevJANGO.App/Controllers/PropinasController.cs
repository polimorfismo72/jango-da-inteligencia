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
using DevJANGO.App.Queries;

namespace DevJANGO.App.Controllers
{
    //[Route("as-propinas")]
    public class PropinasController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly JangoDbContext _context;
        private readonly IPropinaRepository _propinaRepository;
        private readonly IPropinaQueries _propinaQueries;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PropinasController(
              JangoDbContext context,
            IPropinaRepository propinaRepository,
            IPropinaQueries propinaQueries,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _context = context;
            _propinaRepository = propinaRepository;
            _propinaQueries = propinaQueries;
            _mapper = mapper;
        }
        #endregion

        //[ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-divida-de-propina")]
        public async Task<IActionResult> IndexAlunoComDividaPropinaPorTurma([FromQuery] string turma)
        {
            var alunos = await _propinaQueries.ObterAlunosComDividaPropinaPorTurma(turma);
            ViewBag.Pesquisa = turma;
            alunos.ReferenceAction = "lista-de-alunos-com-divida-de-propina";
            return View(alunos);
        }
       
        /*
        #region MÉTODO PARA LISTAR
        //[AllowAnonymous]
        [Route("lista")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterTodos()));
        }
        public async Task<IActionResult> IndexAluno(Guid id)
        {
            return View(_mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaAluno(id)));
        }
 
        #endregion

     

       
        */

        #region MÉTODO PARA MATRICULAR  INICIAÇÂO
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var propina = await ObterPropinaMatriculado(id);

            if (propina == null)
            {
                return NotFound();
            }

            return View(propina);
        }

        [Route("efetuar-pagamento/{id:guid}")]
        public async Task<IActionResult> EfetuarPagamento(Guid id)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaMeses = (from p in _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse())
                                 select new SelectListItem() { Text = p.Mes.NomeMes, Value = p.MesId.ToString() }).ToList();
            listaMeses.Insert(0, new SelectListItem() { Text = "---- Selecione ----", Value = string.Empty });

            ViewBag.Mes = listaMeses;

            #endregion

            var propina = await ObterPropina(id);
            //var propina = await ObterPropinaMatriculado(id);
            //_mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse());
            //ViewBag.Id = propina.Id;

            if (propina == null)
            {
                return NotFound();
            }

            //var propinaAluno =_mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaAluno(id));
            //return RedirectToAction("EfetuarPagamento", propinaAluno);
            return View(propina);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("efetuar-pagamento/{id:guid}")]
        public async Task<IActionResult> EfetuarPagamento(PropinaViewModel propina)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaMeses = (from p in _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse())
                              select new SelectListItem() { Text = p.Mes.NomeMes, Value = p.MesId.ToString() }).ToList();
            listaMeses.Insert(0, new SelectListItem() { Text = "---- Selecione ----", Value = string.Empty });

            ViewBag.Mes = listaMeses;

            #endregion

            var pagamento = await _propinaRepository.ObterPropinaAlunoMatriculado(propina.Id);
            var pagamentoId = pagamento.PagamentoPropinaId;

            return RedirectToAction("IndexMatriculaEfetuada");
        }

        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        //private async Task<PropinaViewModel> PopularPropinas(PropinaViewModel pagamento)
        //{
        //     pagamento = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse());
        //    return pagamento; 
        //}
        private async Task<PropinaViewModel> ObterPropinaMatriculado(Guid id)
        {
            var propina = _mapper.Map<PropinaViewModel>(await _propinaRepository.ObterPropinaAlunoMatriculado(id));
            return propina;
        }
        private async Task<PropinaViewModel> ObterPropina(Guid id)
        {
            var propina = _mapper.Map<PropinaViewModel>(await _propinaRepository.ObterPropina(id));
            return propina;
        }
        #endregion
    }
}

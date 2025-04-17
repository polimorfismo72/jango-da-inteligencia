using AutoMapper;
using DevJANGO.App.Extensions;
using DevJANGO.App.Queries;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class ProfessoresController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IProfessorService _professorService;
        private readonly IProfessorQueries _professorQueries;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ProfessoresController(IProfessorRepository professorRepository,
                                     IDisciplinaRepository disciplinaRepository,
                                     IMapper mapper,
                                     IProfessorService professorService,
                                     IProfessorQueries professorQueries,
                                     INotificador notificador) : base(notificador)
        {
            _professorRepository = professorRepository;
            _disciplinaRepository = disciplinaRepository;
            _professorQueries = professorQueries;
            _mapper = mapper;
            _professorService = professorService;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("Professores", "VI")]
        [Route("lista-de-professores")]
        public async Task<IActionResult> Index([FromQuery] int ps = 6, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var professores = await _professorQueries.ObterTodosProfessores(ps, page, q);
            ViewBag.Pesquisa = q;
            professores.ReferenceAction = "lista-de-professores";
            return View(professores);
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [ClaimsAuthorize("Professores", "VI")]
        [Route("detalhes-do-professor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var professorViewModel = await ObterProfessorDisciplina(id);
            ViewBag.Nome = professorViewModel.Nome;
            if (professorViewModel == null)
            {
                return NotFound();
            }

            return View(professorViewModel);
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [ClaimsAuthorize("Professores", "AD")]
        [Route("novo-professor")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Professores", "AD")]
        [Route("novo-professor")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfessorViewModel professorViewModel)
        {
            if (!ModelState.IsValid) return View(professorViewModel);

            var professor = _mapper.Map<Professor>(professorViewModel);
            await _professorService.Adicionar(professor);

            if (!OperacaoValida()) return View(professorViewModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        [ClaimsAuthorize("Professores", "AD")]
        [Route("editar-professor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var professorViewModel = await ObterProfessorDisciplina(id);
            //var professorViewModel = await ObterProfessor(id);
            if (professorViewModel == null)
            {
                return NotFound();
            }

            return View(professorViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Professores", "AD")]
        [Route("editar-professor/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProfessorViewModel professorViewModel)
        {
            if (id != professorViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(professorViewModel);

            var professor = _mapper.Map<Professor>(professorViewModel);
            await _professorService.Atualizar(professor);

            if (!OperacaoValida()) return View(await ObterProfessorDisciplina(id));

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        [ClaimsAuthorize("Professores", "DG")]
        [Route("excluir-professor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var professorViewModel = await ObterProfessor(id);

            if (professorViewModel == null)
            {
                return NotFound();
            }

            return View(professorViewModel);
        }

        [ClaimsAuthorize("Professores", "DG")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-professor/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var professor = await ObterProfessor(id);

            if (professor == null) return NotFound();

            await _professorService.Remover(id);
            //await _professorRepository.Remover(id);

            if (!OperacaoValida()) return View(professor);

            return RedirectToAction("Index");
        }
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
       
        private async Task<ProfessorViewModel> ObterProfessor(Guid id)
        {
            return _mapper.Map<ProfessorViewModel>(await _professorRepository.ObterProfessor(id));
        }
        private async Task<ProfessorViewModel> ObterProfessorDisciplina(Guid id)
        {
            return _mapper.Map<ProfessorViewModel>(await _professorRepository.ObterProfessorDisciplina(id));
        }
      
        #endregion
    }
}

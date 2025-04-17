using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.App.Extensions;

namespace DevJANGO.App.Controllers
{
    [Route("as-turmas")]
    public class TurmasController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IClasseRepository _classeRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly IAreaDeConhecimentoRepository _areaDeConhecimentoRepository;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public TurmasController(
            IClasseRepository classeRepository,
            ITurmaRepository turmaRepository,
            IAreaDeConhecimentoRepository areaDeConhecimentoRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _classeRepository = classeRepository;
            _turmaRepository = turmaRepository;
            _areaDeConhecimentoRepository = areaDeConhecimentoRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA LISTAR
        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-iniciacao")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIniciacao()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-etapa-um")]
        public async Task<IActionResult> IndexEtapaUm()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaUm()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-etapa-dois")]
        public async Task<IActionResult> IndexEtapaDois()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaDois()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-etapa-tres")]
        public async Task<IActionResult> IndexEtapaTres()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaTres()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-ensino-primario")]
        public async Task<IActionResult> IndexEnsinoPrimario()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEnsinoPrimario()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-primeiro-ciclo")]
        public async Task<IActionResult> IndexICiclo()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasICiclo()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-segundo-ciclo-fb")]
        public async Task<IActionResult> IndexFb()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIICicloFb()));
        }

        [ClaimsAuthorize("Turmas", "VI")]
        [Route("lista-segundo-ciclo-ej")]
        public async Task<IActionResult> IndexEj()
        {
            return View(_mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIICicloEj()));
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [ClaimsAuthorize("Turmas", "VI")]
        [Route("dados/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var aluno = await ObterTurma(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-iciacao")]
        public async Task<IActionResult> Create()
        {
            var turmaViewModel = await PopularTurmasClassesAreaDeConhecimentos(new TurmaViewModel());
            return View(turmaViewModel);

        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-iciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TurmaViewModel turmaViewModel)
        {
            turmaViewModel = await PopularTurmasClassesAreaDeConhecimentos(turmaViewModel);
            if (!ModelState.IsValid) return View(turmaViewModel);
            turmaViewModel.NumDeVagas = 36;
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turmaViewModel));
            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-etapa-um")]
        public async Task<IActionResult> CreateEtapaUm()
        {
            var turmaViewModel = await PopularTurmasClassesAreaDeConhecimentosEtapaUm(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaUm(TurmaViewModel turmaViewModel)
        {
            turmaViewModel = await PopularTurmasClassesAreaDeConhecimentosEtapaUm(turmaViewModel);
            if (!ModelState.IsValid) return View(turmaViewModel);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turmaViewModel));
            return RedirectToAction("IndexEtapaUm");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-etapa-dois")]
        public async Task<IActionResult> CreateEtapaDois()
        {
            var turmaViewModel = await PopularTurmasClassesAreaDeConhecimentosEtapaDois(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaDois(TurmaViewModel turmaViewModel)
        {
            turmaViewModel = await PopularTurmasClassesAreaDeConhecimentosEtapaDois(turmaViewModel);
            if (!ModelState.IsValid) return View(turmaViewModel);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turmaViewModel));
            return RedirectToAction("IndexEtapaDois");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-etapa-tres")]
        public async Task<IActionResult> CreateEtapaTres()
        {
            var turmaViewModel = await PopularTurmasClassesAreaDeConhecimentosEtapaTres(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-na-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaTres(TurmaViewModel turmaViewModel)
        {
            turmaViewModel = await PopularTurmasClassesAreaDeConhecimentosEtapaTres(turmaViewModel);
            if (!ModelState.IsValid) return View(turmaViewModel);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turmaViewModel));
            return RedirectToAction("IndexEtapaTres");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-ensino-primario")]
        public async Task<IActionResult> CreateEnsinoPrimario()
        {
            var turmaViewModel = await PopularTurmasEnsinoPrimario(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnsinoPrimario(TurmaViewModel turma)
        {
           turma= await PopularTurmasEnsinoPrimario(turma);
            if (!ModelState.IsValid) return View(turma);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turma));
            return RedirectToAction("IndexEnsinoPrimario");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-primeiro-ciclo")]
        public async Task<IActionResult> CreateICiclo()
        //public IActionResult CreateICiclo()
        {
            var turmaViewModel = await PopularTurmasICiclo(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateICiclo(TurmaViewModel turma)
        {
           turma= await PopularTurmasICiclo(turma);
            if (!ModelState.IsValid) return View(turma);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turma));
            return RedirectToAction("IndexICiclo");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-segundo-ciclo-fb")]
        public async Task<IActionResult> CreateIICiclofb()
        //public IActionResult CreateIICiclofb()
        {
            var turmaViewModel = await PopularTurmasClassesCursoFb(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloFb(TurmaViewModel turma)
        {
            turma = await PopularTurmasClassesCursoFb(turma);
            if (!ModelState.IsValid) return View(turma);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turma));
            return RedirectToAction("IndexFb");
        }

        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-segundo-ciclo-ej")]
        public async Task<IActionResult> CreateIICicloEj()
        //public IActionResult CreateIICicloEj()
        {
            var turmaViewModel = await PopularTurmasClassesCursoEj(new TurmaViewModel());
            return View(turmaViewModel);
        }

        [HttpPost]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("nova-no-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloEj(TurmaViewModel turma)
        {
            turma = await PopularTurmasClassesCursoEj(turma);
            if (!ModelState.IsValid) return View(turma);
            await _turmaRepository.Adicionar(_mapper.Map<Turma>(turma));
            return RedirectToAction("IndexEj");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var turma = await ObterTurma(id);

            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Turmas", "AD")]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, TurmaViewModel turmaViewModel)
        {
            if (id != turmaViewModel.Id) return NotFound();

            var turmaAtualizacao = await ObterTurma(id);

            if (!ModelState.IsValid) return View(turmaViewModel);
            turmaAtualizacao.ClasseId = turmaViewModel.ClasseId;
            turmaAtualizacao.AreaDeConhecimentoId = turmaViewModel.AreaDeConhecimentoId;
            turmaAtualizacao.NomeTurma = turmaViewModel.NomeTurma;
            turmaAtualizacao.NumDeVagas = turmaViewModel.NumDeVagas;
            turmaAtualizacao.Estado = turmaViewModel.Estado;

            await _turmaRepository.Atualizar(_mapper.Map<Turma>(turmaAtualizacao));
            //await _turmaService.Atualizar(cliente);

            // if (!OperacaoValida()) return View(await ObterFornecedorTurmas(id));

            return RedirectToAction("Index");
        }

        #endregion

        #region MÉTODO PARA EXCLUIR

        [ClaimsAuthorize("Turmas", "EX")]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var turma = await ObterTurma(id);

            if (turma == null)
            {
                return NotFound();
            }

            return View(turma);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Turmas", "EX")]
        [Route("excluir/{id:guid}")]
        //[HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var turma = await ObterTurma(id);

            if (turma == null) return NotFound();

            //await _turmaService.Remover(id);
            await _turmaRepository.Remover(id);

            //if (!OperacaoValida()) return View(turma);
            TempData["Sucesso"] = "Turma excluido com sucesso!";
            return RedirectToAction("Index");
        }

        #endregion

        #region METODOS PRIVADOS DA CONTROLLER

        private async Task<TurmaViewModel> ObterTurma(Guid id)
        {
            var turma = _mapper.Map<TurmaViewModel>(await _turmaRepository.ObterTurmaClasseAreaDeConhecimento(id));
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterTodos());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterTodos());

            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasClassesAreaDeConhecimentosEtapaUm(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaUm());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoEtapaUm());
            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasClassesAreaDeConhecimentosEtapaDois(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaDois());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoEtapaDois());
            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasClassesAreaDeConhecimentosEtapaTres(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaTres());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoEtapaTres());
            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasClassesAreaDeConhecimentos(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIniciacao());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIniciacao());
            return turma;
        }

        private async Task<TurmaViewModel> PopularTurmasEnsinoPrimario(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesEnsinoPrimario());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoEnsinoPrimario());
            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasICiclo(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesICiclo());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoICiclo());
            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasClassesCursoFb(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesCursoFb());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIICicloFB());
            return turma;
        }
        private async Task<TurmaViewModel> PopularTurmasClassesCursoEj(TurmaViewModel turma)
        {
            turma.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesCursoEj());
            turma.AreaDeConhecimentos = _mapper.Map<IEnumerable<AreaDeConhecimentoViewModel>>(await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIICicloEJ());
            return turma;
        }
        #endregion
    }
}

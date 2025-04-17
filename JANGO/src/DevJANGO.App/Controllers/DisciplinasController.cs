using AutoMapper;
using DevJANGO.App.Extensions;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Controllers
{
    public class DisciplinasController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly INiveisDeEnsinoRepository _niveisDeEnsinoRepository;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public DisciplinasController( 
                                  INiveisDeEnsinoRepository niveisDeEnsinoRepository,
                                                    IDisciplinaRepository disciplinaRepository,
                                                    IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _niveisDeEnsinoRepository= niveisDeEnsinoRepository;
            _disciplinaRepository = disciplinaRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-da-iniciacao")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsino()));
        }

        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-do-ensino-primario")]
        public async Task<IActionResult> IndexEnsinoPrimario()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoEnsinoPrimario()));
        }

        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-do-primeiro-ciclo")]
        public async Task<IActionResult> IndexICiclo()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoICiclo()));
        }

        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-do-segundo-ciclo")]
        public async Task<IActionResult> IndexIICiclo()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoIICiclo()));
        }

        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-da-etapa-um")]
        public async Task<IActionResult> IndexEtapaUm()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaUm()));
        }

        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-da-etapa-dois")]
        public async Task<IActionResult> IndexEtapaDois()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaDois()));
        }

        [ClaimsAuthorize("Disciplinas", "VI")]
        [Route("lista-de-disciplina-da-etapa-tres")]
        public async Task<IActionResult> IndexEtapaTres()
        {
            return View(_mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaTres()));
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO

        #region INICIAÇÃO
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-iniciacao")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-iniciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);
            return RedirectToAction("Index");
        }
        #endregion

        #region ENSINO PRIMÁRIO
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-do-ensino-primario")]
        public IActionResult CreateEnsinoPrimario()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-do-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnsinoPrimario(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);
         
            return RedirectToAction("IndexEnsinoPrimario");
        }
        #endregion

        #region I CILO
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-do-primeiro-ciclo")]
        public IActionResult CreateICiclo()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-do-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateICiclo(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);

            return RedirectToAction("IndexICiclo");
        }
        #endregion

        #region II CICLO
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-do-segundo-ciclo")]
        public IActionResult CreateIICiclo()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-do-segundo-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICiclo(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);

            return RedirectToAction("IndexIICiclo");
        }
        #endregion

        #region ETAPA I
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-etapa-um")]
        public IActionResult CreateEtapaUm()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaUm(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);

            return RedirectToAction("IndexEtapaUm");
        }
        #endregion

        #region ETAPA II
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-etapa-dois")]
        public IActionResult CreateEtapaDois()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaDois(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);

            return RedirectToAction("IndexEtapaDois");
        }
        #endregion

        #region ETAPA III
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-etapa-tres")]
        public IActionResult CreateEtapaTres()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("nova-disciplina-da-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaTres(DisciplinaViewModel disciplinaViewModel)
        {
            var niveisDeEnsinoId = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaTres();
            disciplinaViewModel.NiveisDeEnsinoId = niveisDeEnsinoId.Id;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("NiveisDeEnsinoId");
            #endregion

            if (!ModelState.IsValid) return View(disciplinaViewModel);
            var disciplina = _mapper.Map<Disciplina>(disciplinaViewModel);
            await _disciplinaRepository.Adicionar(disciplina);

            return RedirectToAction("IndexEtapaTres");
        }
        #endregion
        #endregion

        #region MÉTODO PARA EDITAR
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("editar-disciplina/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var disciplinaViewModel = await ObterDisciplina(id);

            if (disciplinaViewModel == null)
            {
                return NotFound();
            }

            return View(disciplinaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Disciplinas", "AD")]
        [Route("editar-disciplina/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, DisciplinaViewModel disciplinaViewModel)
        {
            if (id != disciplinaViewModel.Id) return NotFound();

            var disciplinaAtualizacao = await ObterDisciplina(id);
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsino(disciplinaAtualizacao.NiveisDeEnsinoId);
            if (!ModelState.IsValid) return View(disciplinaViewModel);
            disciplinaAtualizacao.NomeDisciplina = disciplinaViewModel.NomeDisciplina;

            await _disciplinaRepository.Atualizar(_mapper.Map<Disciplina>(disciplinaAtualizacao));
          
            if (nivel.NomeNiveisDeEnsino == "Primário")
            {
                return RedirectToAction("IndexEnsinoPrimario");
            }
            if (nivel.NomeNiveisDeEnsino == "I Ciclo")
            {
                return RedirectToAction("IndexICiclo");
            }
            if (nivel.NomeNiveisDeEnsino == "II Ciclo")
            {
                return RedirectToAction("IndexIICiclo");
            }
            if (nivel.NomeNiveisDeEnsino == "Etapa I")
            {
                return RedirectToAction("IndexEtapaUm");
            }
            if (nivel.NomeNiveisDeEnsino == "Etapa II")
            {
                return RedirectToAction("IndexEtapaDois");
            }
            if (nivel.NomeNiveisDeEnsino == "Etapa III")
            {
                return RedirectToAction("IndexEtapaTres");
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        [ClaimsAuthorize("Disciplinas", "EX")]
        [Route("excluir-disciplina/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var disciplina = await ObterDisciplina(id);

            if (disciplina == null)
            {
                return NotFound();
            }

            return View(disciplina);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Disciplinas", "EX")]
        [Route("excluir-disciplina/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var disciplina = await ObterDisciplina(id);
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsino(disciplina.NiveisDeEnsinoId);
           
            if (disciplina == null) return NotFound();

            await _disciplinaRepository.Remover(id);
            TempData["Sucesso"] = "Disciplina excluida com sucesso!";
            if (nivel.NomeNiveisDeEnsino == "Primário")
            {
                return RedirectToAction("IndexEnsinoPrimario");
            }
            if (nivel.NomeNiveisDeEnsino == "I Ciclo")
            {
                return RedirectToAction("IndexICiclo");
            }
            if (nivel.NomeNiveisDeEnsino == "II Ciclo")
            {
                return RedirectToAction("IndexIICiclo");
            }
            if (nivel.NomeNiveisDeEnsino == "Etapa I")
            {
                return RedirectToAction("IndexEtapaUm");
            }
            if (nivel.NomeNiveisDeEnsino == "Etapa II")
            {
                return RedirectToAction("IndexEtapaDois");
            }
            if (nivel.NomeNiveisDeEnsino == "Etapa III")
            {
                return RedirectToAction("IndexEtapaTres");
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<DisciplinaViewModel> ObterDisciplina(Guid id)
        {
            var disciplina = _mapper.Map<DisciplinaViewModel>(await _disciplinaRepository.ObterDisciplina(id));
            disciplina.NiveisDeEnsinos = _mapper.Map<IEnumerable<NiveisDeEnsinoViewModel>>(await _niveisDeEnsinoRepository.ObterTodos());
            return disciplina;
        }
        #endregion
    }
}

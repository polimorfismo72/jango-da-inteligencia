using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.App.Extensions;

namespace DevJANGO.App.Controllers
{
    public class ClassesController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IClasseRepository _classeRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly INiveisDeEnsinoRepository _niveisDeEnsinoRepository;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ClassesController(
            IClasseRepository classeRepository,
            ICursoRepository cursoRepository,
            INiveisDeEnsinoRepository niveisDeEnsinoRepository,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _classeRepository = classeRepository;
            _cursoRepository = cursoRepository;
            _niveisDeEnsinoRepository = niveisDeEnsinoRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA LISTAR
        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-iniciacao")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesIniciacao()));
        }

        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-ensino-primario")]
        public async Task<IActionResult> IndexEnsinoPrimario()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesEnsinoPrimario()));
        }
        
        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-primeiro-ciclo")]
        public async Task<IActionResult> IndexICiclo()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesICiclo()));
        }
     
        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-segundo-ciclo-fb")]
        public async Task<IActionResult> IndexFb()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesCursoFb()));
        }
        
        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-segundo-ciclo-ej")]
        public async Task<IActionResult> IndexEj()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesCursoEj()));
        }

        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-na-etapa-um")]
        public async Task<IActionResult> IndexEtapaI()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaUm()));
        }

        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-na-etapa-dois")]
        public async Task<IActionResult> IndexEtapaII()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaDois()));
        }

        [ClaimsAuthorize("Classes", "VI")]
        [Route("lista-de-classe-na-etapa-tres")]
        public async Task<IActionResult> IndexEtapaIII()
        {
            return View(_mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaTres()));
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [ClaimsAuthorize("Classes", "VI")]
        [Route("dados-de-classe/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var aluno = await ObterClasse(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-iciacao")]
        public IActionResult Create()
        {
            //var classeViewModel = await PopularClassesCursos(new ClasseViewModel());
            //return View(classeViewModel);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-iciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClasseViewModel classeViewModel)
        {
            //classeViewModel = await PopularClassesCursos(classeViewModel);
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();

            classeViewModel.NiveisDeEnsinoId = nivel.Id;
            classeViewModel.CursoId = curso.Id;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");

            #endregion
            if (!ModelState.IsValid) return View(classeViewModel);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classeViewModel));
            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-ensino-primario")]
        public IActionResult CreateEnsinoPrimario()
        {
            //var classe = await PopularClassesCursos(new ClasseViewModel());
            //return View(classeViewModel);
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnsinoPrimario(ClasseViewModel classe)
        {
            //classe= await PopularClassesCursos(classe);
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");
            #endregion

            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexEnsinoPrimario");
        }

        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-primeiro-ciclo")]
        public IActionResult CreateICiclo()
        {
            return View();
        }
        
        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateICiclo(ClasseViewModel classe)
        {
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");

            #endregion
            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexICiclo");
        }

        #region EtapaI

        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-etapa-um")]
        public IActionResult CreateEtapaI()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaI(ClasseViewModel classe)
        {
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");
            #endregion

            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexEtapaI");
        }

        #endregion

        #region EtapaII
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-etapa-dois")]
        public IActionResult CreateEtapaII()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaII(ClasseViewModel classe)
        {
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");
            #endregion

            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexEtapaII");
        }

        #endregion

        #region EtapaIII
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-etapa-tres")]
        public IActionResult CreateEtapaIII()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-na-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaIII(ClasseViewModel classe)
        {
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaTres();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");
            #endregion

            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexEtapaIII");
        }

        #endregion

        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-segundo-ciclo-fb")]
        public IActionResult CreateIICiclofb()
        {
            return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloFb(ClasseViewModel classe)
        {
            //classe= await PopularClassesCursos(classe);
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloFb();  

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");

            #endregion
            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexFb");
        }

        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-segundo-ciclo-ej")]
        public IActionResult CreateIICicloEj()
        {
          return View();
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [Route("nova-classe-no-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloEj(ClasseViewModel classe)
        {
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloEj();

            classe.NiveisDeEnsinoId = nivel.Id;
            classe.CursoId = curso.Id;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("CursoId");

            #endregion
            if (!ModelState.IsValid) return View(classe);
            await _classeRepository.Adicionar(_mapper.Map<Classe>(classe));
            return RedirectToAction("IndexEj");
        }
        #endregion

        #region MÉTODO PARA EDITAR

        [ClaimsAuthorize("Classes", "AD")]
        [Route("editar-classe/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var classe = await ObterClasse(id);
            if (classe == null)
            {
                return NotFound();
            }
            return View(classe);
        }

        [HttpPost]
        [ClaimsAuthorize("Classes", "AD")]
        [ValidateAntiForgeryToken]
        [Route("editar-classe/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ClasseViewModel classeViewModel)
        {
            if (id != classeViewModel.Id) return NotFound();
            //var classeAtualizacao = await ObterClasse(id);
            var nivel = await _classeRepository.ObterClassePeloId(id);

            if (!ModelState.IsValid) return View(classeViewModel);
            var fornecedor = _mapper.Map<Classe>(classeViewModel);
            await _classeRepository.Atualizar(fornecedor);

            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário")
            {
                return RedirectToAction("IndexEnsinoPrimario");
            }

            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo")
            {
                return RedirectToAction("IndexICiclo");
            }

            if (nivel.Curso.Nome == "Ciências Economicas e Jurídicas")
            {
                return RedirectToAction("IndexEj");
            }

            if (nivel.Curso.Nome == "Ciências Fisicas e Biologicas")
            {
                return RedirectToAction("IndexFb"); 
            }

            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I")
            {
                return RedirectToAction("IndexEtapaI");
            }

            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II")
            {
                return RedirectToAction("IndexEtapaII");
            }

            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III")
            {
                return RedirectToAction("IndexEtapaIII");
            }

            return RedirectToAction("Index");
        }

        #endregion

        #region MÉTODO PARA EXCLUIR
        [ClaimsAuthorize("Classes", "DG")]
        [Route("excluir-classe/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var classe = await ObterClasse(id);

            if (classe == null)
            {
                return NotFound();
            }

            return View(classe);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Classes", "DG")]
        [Route("excluir-classe/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var classe = await ObterClasse(id);
          
            if (classe == null) return NotFound();
            await _classeRepository.Remover(id);

            TempData["Sucesso"] = "Classe excluido com sucesso!";
            if (classe.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário")
            {
                return RedirectToAction("IndexEnsinoPrimario");
            }
            if (classe.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo")
            {
                return RedirectToAction("IndexICiclo");
            }
            if (classe.NiveisDeEnsino.NomeNiveisDeEnsino == "II Ciclo")
            {
                return RedirectToAction("IndexIICiclo");
            }
            if (classe.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I")
            {
                return RedirectToAction("IndexEtapaI");
            }
            if (classe.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II")
            {
                return RedirectToAction("IndexEtapaII");
            }
            if (classe.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III")
            {
                return RedirectToAction("IndexEtapaIII");
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<ClasseViewModel> ObterClasse(Guid id)
        {
            var classe = _mapper.Map<ClasseViewModel>(await _classeRepository.ObterClasseNiveisDeEnsinoCurso(id));
            classe.NiveisDeEnsinos = _mapper.Map<IEnumerable<NiveisDeEnsinoViewModel>>(await _niveisDeEnsinoRepository.ObterTodos());
            classe.Cursos = _mapper.Map<IEnumerable<CursoViewModel>>(await _cursoRepository.ObterTodos());
            return classe;
        } 
        #endregion
    }
}

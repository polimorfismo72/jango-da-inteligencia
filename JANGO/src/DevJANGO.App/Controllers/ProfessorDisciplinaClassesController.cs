using AutoMapper;
using DevJANGO.App.Extensions;
using DevJANGO.App.Queries;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Controllers
{
    public class ProfessorDisciplinaClassesController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly JangoDbContext _context;
        private readonly IMesRepository _mesRepository;
        private readonly IProfessorRepository _professorRepository;
        private readonly IDisciplinaRepository _disciplinaRepository;
        private readonly IClasseRepository _classeRepository;
        private readonly IProfessorDisciplinaClasseQueries _professorDisciplinaClasseQueries;
        private readonly IProfessorDisciplinaClasseRepository _professorDisciplinaClasseRepository;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ProfessorDisciplinaClassesController(IProfessorRepository professorRepository,
                                                     JangoDbContext context,
                                                    IMesRepository mesRepository,   
                                                    IDisciplinaRepository disciplinaRepository,
                                                    IClasseRepository classeRepository,
                                                    IProfessorDisciplinaClasseQueries professorDisciplinaClasseQueries,
                                                    IProfessorDisciplinaClasseRepository professorDisciplinaClasseRepository,
                                                    IMapper mapper,
                                     INotificador notificador) : base(notificador)
        {
            _context = context;
            _professorRepository = professorRepository;
             _mesRepository= mesRepository;
            _disciplinaRepository = disciplinaRepository;
            _classeRepository = classeRepository;
            _professorDisciplinaClasseQueries = professorDisciplinaClasseQueries;
            _professorDisciplinaClasseRepository = professorDisciplinaClasseRepository;
            _mapper = mapper;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "VI")]
        [Route("lista-de-disciplina-do-professores")]
        public async Task<IActionResult> Index([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var professores = await _professorDisciplinaClasseQueries.ObterTodosProfessoresDisciplinaClasses(ps, page, q);
            ViewBag.Pesquisa = q;
            professores.ReferenceAction = "lista-de-disciplina-do-professores";
            return View(professores);
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        //[AllowAnonymous]
        [Route("dados-da-disciplina-do-professor/{id:guid}")]
        //public async Task<IActionResult> Details(Guid id)
        //{
        //    var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseProfessor(id);

        //    if (professorDisciplinaClasseViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(professorDisciplinaClasseViewModel);
        //}
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO

        #region INICIAÇÃO
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-iniciacao")]
        public async Task<IActionResult> Create()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-iniciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoIniciacao(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseId();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId; 
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"]= $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);

            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region ENSINO PRIMARIO
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-ensino-primario")]
        public async Task<IActionResult> CreateEnsinoPrimario()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoPrimario(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnsinoPrimario(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoPrimario(professorDisciplinaClasseViewModel);
           
            var classe = await _classeRepository.ObterClasseIdEnsinoPrimario();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);
            
            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            //return RedirectToAction("Index");
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region I CILO
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-primeiro-ciclo")]
        public async Task<IActionResult> CreateICiclo()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoICiclo(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateICiclo(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoICiclo(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseIdICiclo();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);
            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            //return RedirectToAction("Index");
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region ETAPA I
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-etapa-um")]
        public async Task<IActionResult> CreateEtapaUm()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoEtapaUm(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaUm(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoEtapaUm(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseIdEtapaUm();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);
            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            //return RedirectToAction("Index");
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region ETAPA II
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-etapa-dois")]
        public async Task<IActionResult> CreateEtapaDois()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoEtapaDois(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaDois(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoEtapaDois(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseIdEtapaDois();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);
            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            //return RedirectToAction("Index");
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region ETAPA III
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-etapa-tres")]
        public async Task<IActionResult> CreateEtapaTres()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoEtapaTres(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-da-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaTres(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoEtapaTres(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseIdEtapaTres();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);
            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region II CILO FISICAS BIOLOGICAS
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-segundo-ciclo-fb")]
        public async Task<IActionResult> CreateIICicloFisicasBiologica()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoIICicloFb(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloFisicasBiologica(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoIICicloFb(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseIdIICiclo();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeNome = classe.Nome;
            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion
            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);

            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            //return RedirectToAction("Index");
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #region II CILO ECONOMICAS E JURIDICAS
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-segundo-ciclo-ej")]
        public async Task<IActionResult> CreateIICicloEconomicaJuridica()
        {
            var disciplinasDoProfessor = await PopularDisciplinasNiveisDeEnsinoIICicloEj(new ProfessorDisciplinaClasseViewModel());
            return View(disciplinasDoProfessor);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("nova-disciplina-ao-professor-do-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloEconomicaJuridica(ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoIICicloEj(professorDisciplinaClasseViewModel);
            var classe = await _classeRepository.ObterClasseIdIICiclo();
            var telefone = await _professorRepository.ObterProfessorPeloTelefone(professorDisciplinaClasseViewModel.TelefoneBI);
            if (telefone == null)
            {
                TempData["Erro"] = "Telefone ou BI não existe!";
                return View(professorDisciplinaClasseViewModel);
            }
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;
            var classeNome = classe.Nome;

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("ProfessorId");
            ModelState.Remove("ClasseId");
            #endregion

            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            professorDisciplinaClasseViewModel.ProfessorId = telefone.Id;
            professorDisciplinaClasseViewModel.ClasseId = classe.Id;
            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (_professorDisciplinaClasseRepository.Buscar(a => a.DisciplinaId == professorDisciplinaClasseViewModel.DisciplinaId && a.ClasseId == classe.Id).Result.Any())
            {
                TempData["Erro"] = $"A disciplina que pretende atribuir ao professor para a {classeNome} classe já foi atribuida!";
                return View(professorDisciplinaClasseViewModel);
            }
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            await _professorDisciplinaClasseRepository.Adicionar(professorDisciplinaClasse);

            TempData["Sucesso"] = "Disciplina atribuida com sucesso!";
            //return RedirectToAction("Index");
            return View(professorDisciplinaClasseViewModel);
        }
        #endregion

        #endregion

        #region MÉTODO PARA EDITAR
        /*
        [Route("editar-disciplina-do-professor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
             await PopularDisciplinasDoProfessor(new ProfessorDisciplinaClasseViewModel());

            var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseProfessor(id);
            if (professorDisciplinaClasseViewModel == null)
            {
                return NotFound();
            }

            return View(professorDisciplinaClasseViewModel);
        }
        */

        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-disciplina-do-professor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            professorDisciplinaClasseViewModel = await PopularDisciplinasDoProfessor(professorDisciplinaClasseViewModel);
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            var classeId = professorDisciplinaClasseViewModel.ClasseId;
            var nomeDaClasse = await ObterClasse(classeId);
            var classeNome = nomeDaClasse.Nome;

            if (id != professorDisciplinaClasseViewModel.Id) return NotFound();

            professorDisciplinaClasseViewModel.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseViewModel.NomeClasse = classeNome;

            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);
        
            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseViewModel);
            //await _professorDisciplinaClasseService .Atualizar(professorDisciplinaClasse);
            await _professorDisciplinaClasseRepository.Atualizar(professorDisciplinaClasse);

            //if (!OperacaoValida()) return View(await ObterProfessorDisciplinaClasseProfessors(id));

            return RedirectToAction("Index");
        }
        */
        #endregion

        #region MÉTODO PARA EDITAR DISCIPLINA
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("editar-disciplina-que-o-professor-leciona/{id:guid}")]
        public async Task<IActionResult> EditarDisciplina(Guid id)
        {
            var professor = await ObterProfessorPorDisciplinaClasseProfessor(id);
            var nivel = await _classeRepository.ObterClassePeloId(professor.ClasseId);
            ViewBag.Nome = professor.Professor.Nome;
          
            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Primário")
            {
                var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClassePrimario(id);
                //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

                if (professorDisciplinaClasseViewModel == null)
                {
                    return NotFound();
                }
                return View(professorDisciplinaClasseViewModel);
            }
           
            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "I Ciclo")
            {
                var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseICiclo(id);
                //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

                if (professorDisciplinaClasseViewModel == null)
                {
                    return NotFound();
                }
                return View(professorDisciplinaClasseViewModel);
            }
         
            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "II Ciclo")
            {
                var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseIICiclo(id);
                //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

                if (professorDisciplinaClasseViewModel == null)
                {
                    return NotFound();
                }
                return View(professorDisciplinaClasseViewModel);
            }
          
            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa I")
            {
                var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseEtapaUm(id);
                //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

                if (professorDisciplinaClasseViewModel == null)
                {
                    return NotFound();
                }
                return View(professorDisciplinaClasseViewModel);
            }
          
            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa II")
            {
                var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseEtapaDois(id);
                //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

                if (professorDisciplinaClasseViewModel == null)
                {
                    return NotFound();
                }
                return View(professorDisciplinaClasseViewModel);
            }
          
            if (nivel.NiveisDeEnsino.NomeNiveisDeEnsino == "Etapa III")
            {
                var professorDisciplinaClasseViewModel = await ObterProfessorPorDisciplinaClasseEtapaTres(id);
                //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

                if (professorDisciplinaClasseViewModel == null)
                {
                    return NotFound();
                }
                return View(professorDisciplinaClasseViewModel);
            }



            var professorDiscipl = await ObterProfessorPorDisciplinaClasseIniciacao(id);
            //var p =  await PopularDisciplinasNiveisDeEnsinoIniciacao(new ProfessorDisciplinaClasseViewModel());

            if (professorDiscipl == null)
            {
                return NotFound();
            }
            return View(professorDiscipl);
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [ValidateAntiForgeryToken]
        [Route("editar-disciplina-que-o-professor-leciona/{id:guid}")]
        public async Task<IActionResult> EditarDisciplina(Guid id, ProfessorDisciplinaClasseViewModel professorDisciplinaClasseViewModel)
        {
            //professorDisciplinaClasseViewModel = await PopularDisciplinasNiveisDeEnsinoIniciacao(professorDisciplinaClasseViewModel);
            var professorDisciplinaClasseAtualizacao = await ObterProfessorPorDisciplinaClasseProfessor(id);
            var disciplinaId = professorDisciplinaClasseViewModel.DisciplinaId;
            var professorId = professorDisciplinaClasseAtualizacao.ProfessorId;
            Guid idp = professorId;
            var nomeDaDisciplina = await ObterDisciplina(disciplinaId);
            var disciplinaNome = nomeDaDisciplina.NomeDisciplina;

            professorDisciplinaClasseViewModel.Professor = professorDisciplinaClasseAtualizacao.Professor;

            var classeId = professorDisciplinaClasseViewModel.ClasseId;
            var nomeDaClasse = await ObterClasse(classeId);
            var classeNome = nomeDaClasse.Nome;

            if (id != professorDisciplinaClasseViewModel.Id) return NotFound();

            professorDisciplinaClasseAtualizacao.Disciplina= professorDisciplinaClasseViewModel.Disciplina;
            professorDisciplinaClasseAtualizacao.DisciplinaId = disciplinaId;
            professorDisciplinaClasseAtualizacao.Classe = professorDisciplinaClasseViewModel.Classe;
            professorDisciplinaClasseAtualizacao.ClasseId = classeId;
            professorDisciplinaClasseAtualizacao.NomeDisciplina = disciplinaNome;
            professorDisciplinaClasseAtualizacao.NomeClasse = classeNome;

            if (!ModelState.IsValid) return View(professorDisciplinaClasseViewModel);

            var professorDisciplinaClasse = _mapper.Map<ProfessorDisciplinaClasse>(professorDisciplinaClasseAtualizacao);
            await _professorDisciplinaClasseRepository.Atualizar(professorDisciplinaClasse);
            TempData["Sucesso"] = "Disciplina alterada com sucesso!";
            var rota = $"../detalhes-do-professor/{idp}";
            return Redirect($"{rota}");
        }
        #endregion

        #region MÉTODO PARA ATRIBUIR DISCIPLINA
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [Route("atribuir-disciplina-ao-professor/{id:guid}")]
        public async Task<IActionResult> AtribuirDisciplina(Guid id)
        {
            var disciplinasDoProfessor = await PopularDisciplinasDoProfessorIniciacao(new ProfessorDisciplinaClasseViewModel());

            var atribuirDisciplina = await ObterProfessorDisciplinaClasse1(id); 

            var professor = await ObterProfessorDisciplinaClassePorProfessor(id);

            if (professor == null)
            {
                return RedirectToAction("Create", atribuirDisciplina);
            }
            ViewBag.Nome = professor.Professor.Nome;

            if (atribuirDisciplina == null)
            {
                return NotFound();
            }
            return View(disciplinasDoProfessor);
            //return View(atribuirDisciplina);
            //return RedirectToAction("Create", atribuirDisciplina); 
        }

        [HttpPost]
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "AD")]
        [ValidateAntiForgeryToken] 
        [Route("atribuir-disciplina-ao-professor/{id:guid}")]
        public async Task<IActionResult> AtribuirDisciplina(Guid id, ProfessorDisciplinaClasseViewModel atribuirDisciplina)
        {
            //var disciplinasDoProfessor = await PopularDisciplinasDoProfessorIniciacao(new ProfessorDisciplinaClasseViewModel());
            atribuirDisciplina  = await ObterProfessorDisciplinaClasse1(id);

            var professor = await ObterProfessorDisciplinaClassePorProfessor(id);
            if (professor == null)
            {
                return RedirectToAction("Create", atribuirDisciplina);
            }

            if (atribuirDisciplina == null)
            {
                return NotFound();
            }
            return RedirectToAction("Create", atribuirDisciplina);
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        [ClaimsAuthorize("ProfessorDisciplinaClasses", "DG")]
        public async Task<IActionResult> Apagar(Guid id)
        {
            var professorDisciplinaClasse = await ObterProfessorPorDisciplinaClasseProfessor(id);
            var professorId = professorDisciplinaClasse.ProfessorId;
            Guid idp = professorId;
            if (professorDisciplinaClasse == null) return NotFound();

            await _professorDisciplinaClasseRepository.Remover(id);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            TempData["Sucesso"] = "Disciplina excluida com sucesso!";
            var rota = $"detalhes-do-professor";
            //var d = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterProfessorPorId(professorId));

            return RedirectToRoute(new { controller = "..", action = $"{rota}", id = idp });
            //return RedirectToRoute(new { controller = "..", action = $"{rota}", id = idp, d });
        }
        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorDisciplinaClasse1(Guid id)
        {
            return _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorDisciplinaClasseProfessor(id));
        }

        #region POPULAR CADASTRO
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoIniciacao(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsino());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoPrimario(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoEnsinoPrimario());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoICiclo(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoICiclo());
            return disciplinaDoprofessor;
        }

        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoEtapaUm(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaUm());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoEtapaDois(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaDois());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoEtapaTres(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaTres());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoIICicloFb(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoIICiclo());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasNiveisDeEnsinoIICicloEj(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoIICiclo());
            return disciplinaDoprofessor;
        }

        #endregion

        #region EDITAR DISCIPLINA
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseIniciacao(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsino());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesIniciacao());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClassePrimario(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoEnsinoPrimario());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesEnsinoPrimario());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseICiclo(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoICiclo());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesICiclo());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseIICiclo(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoIICiclo());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICiclo());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseEtapaUm(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaUm());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaUm());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseEtapaDois(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaDois());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaDois());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseEtapaTres(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEtapaTres());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaTres());
            return disciplinaDoprofessor;
        }


        #endregion

        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorPorDisciplinaClasseProfessor(Guid id)
        {
            var disciplinaDoprofessor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorPorDisciplinaClasseProfessor(id));
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterTodos());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterTodos());
            return disciplinaDoprofessor;
        }

        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasDoProfessor(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterTodos());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterTodos());
            return disciplinaDoprofessor;
        }
      
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasDoProfessorIniciacao(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsino());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIniciacao());
            return disciplinaDoprofessor;
        }

        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasDoProfessorEnsinoPrimario(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoEnsinoPrimario());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoPrimario());
            return disciplinaDoprofessor;
        }

        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasDoProfessorEnsinoICiclo(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoICiclo());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoICiclo());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasDoProfessorEnsinoIICicloFb(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoIICiclo());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloFb());
            return disciplinaDoprofessor;
        }
        private async Task<ProfessorDisciplinaClasseViewModel> PopularDisciplinasDoProfessorEnsinoIICicloEj(ProfessorDisciplinaClasseViewModel disciplinaDoprofessor)
        {
            disciplinaDoprofessor.Professores = _mapper.Map<IEnumerable<ProfessorViewModel>>(await _professorRepository.ObterTodos());
            disciplinaDoprofessor.Disciplinas = _mapper.Map<IEnumerable<DisciplinaViewModel>>(await _disciplinaRepository.ObterDisciplinasNiveisDeEnsinoIICiclo());
            disciplinaDoprofessor.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloEj());
            return disciplinaDoprofessor;
        }
        private async Task<ClasseViewModel> ObterClasse(Guid id)
        {
            return _mapper.Map<ClasseViewModel>(await _classeRepository.ObterClasse(id));
        }
        private async Task<DisciplinaViewModel> ObterDisciplina(Guid id)
        {
            return _mapper.Map<DisciplinaViewModel>(await _disciplinaRepository.ObterDisciplina(id));
        }
        private async Task<ProfessorDisciplinaClasseViewModel> ObterProfessorDisciplinaClassePorProfessor(Guid id)
        {
            var professor = _mapper.Map<ProfessorDisciplinaClasseViewModel>(await _professorDisciplinaClasseRepository.ObterProfessorDisciplinaClassePorProfessor(id));
            return professor;
        }
        #endregion
    }
}

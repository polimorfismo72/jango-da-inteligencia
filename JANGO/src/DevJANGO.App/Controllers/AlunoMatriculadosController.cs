using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Context;
using Microsoft.AspNetCore.Authorization;
using DevJANGO.App.Extensions;
using Rotativa.AspNetCore;
using Microsoft.EntityFrameworkCore;
using DevJANGO.App.Queries;
using DevJANGO.Data.Repository;
using System.Diagnostics.Metrics;


namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class AlunoMatriculadosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private string numDeVagas;
        private readonly JangoDbContext _context;
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;
        private readonly IClasseRepository _classeRepository;
        private readonly ITurmaRepository _turmaRepository;
        private readonly INiveisDeEnsinoRepository _niveisDeEnsinoRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IEncarregadoRepository _encarregadoRepository;
        private readonly IGrauDeParentescoRepository _grauDeParentescoRepository;
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;

        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        private readonly IAlunoMatriculadoService _alunoMatriculadoService; 
        private readonly IPagamentoPropinaService _pagamentoPropinaService; 

        private readonly IPropinaRepository _propinaRepository;
        private readonly IMultaRepository _multaRepository;
        private readonly IPagamentoPropinaRepository _pagamentoPropinaRepository;
        //private readonly IPagamentoPropinaItemRepository _pagamentoPropinaItemRepository;
        private readonly IMesRepository _mesRepository;
        private readonly IAlunoMatriculadoQueries _alunoMatriculadoQueries;

        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AlunoMatriculadosController(
              JangoDbContext context,
            IAlunoInscritoRepository alunoInscritoRepository,
            IClasseRepository classeRepository,
            ITurmaRepository turmaRepository,
            INiveisDeEnsinoRepository niveisDeEnsinoRepository,
            ICursoRepository cursoRepository,
            IEncarregadoRepository encarregadoRepository,
            IGrauDeParentescoRepository grauDeParentescoRepository,
            IFuncionarioCaixaRepository funcionarioCaixaRepository,
            IAlunoMatriculadoRepository alunoMatriculadoRepository,
            IMapper mapper,
            INotificador notificador,
            IAlunoMatriculadoService alunoMatriculadoService,
            IPagamentoPropinaService pagamentoPropinaService,
            IPropinaRepository propinaRepository,
            IMultaRepository multaRepository,
            IPagamentoPropinaRepository pagamentoPropinaRepository,
            //IPagamentoPropinaItemRepository pagamentoPropinaItemRepository,
            IMesRepository mesRepository
            ,IAlunoMatriculadoQueries alunoMatriculadoQueries) : base(notificador)
        {
            _context = context;
            _alunoInscritoRepository = alunoInscritoRepository;
            _classeRepository = classeRepository;
            _turmaRepository = turmaRepository;
            _niveisDeEnsinoRepository = niveisDeEnsinoRepository;
            _cursoRepository = cursoRepository;
            _encarregadoRepository = encarregadoRepository;
            _grauDeParentescoRepository = grauDeParentescoRepository;
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
            _alunoMatriculadoRepository = alunoMatriculadoRepository;
            _alunoMatriculadoService = alunoMatriculadoService;
            _pagamentoPropinaService = pagamentoPropinaService;
            _mapper = mapper;
            _propinaRepository = propinaRepository;
            _multaRepository = multaRepository;
            _pagamentoPropinaRepository = pagamentoPropinaRepository;
            //_pagamentoPropinaItemRepository = pagamentoPropinaItemRepository;
            _mesRepository = mesRepository;
            _alunoMatriculadoQueries = alunoMatriculadoQueries;
        }
        #endregion

        #region MÉTODO PARA LISTAR
        [ClaimsAuthorize("AlunoMatriculados", "DG")]
        [Route("estatistica-financeira-de-alunos-matriculados")]
        public async Task<IActionResult> EstatisticaAlunoMatriculado()
        {
            return View(_mapper.Map<IEnumerable<AlunoMatriculadoViewModel>>(await _alunoMatriculadoRepository.ObterAlunosMatriculados()));
        }
        #region LISTA PENDENTE DE MATRICULAS 
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-na-iniciacao")]
        public async Task<IActionResult> IndexMatriculaPendenteIniciacao([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosIniciacaoPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-na-iniciacao";
            return View(alunos);
        }
       
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-no-ensino-primario")]
        public async Task<IActionResult> IndexMatriculaPendenteEnsinoPrimario([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosPrimarioPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-no-ensino-primario";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-no-primeiro-ciclo")]
        public async Task<IActionResult> IndexMatriculaPendenteICiclo([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosICicloPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-no-primeiro-ciclo";
            return View(alunos);
        }
      

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-no-segundo-ciclo-fb")]
        public async Task<IActionResult> IndexMatriculaPendenteFb([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosIICicloFisicasBiologicaPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-no-segundo-ciclo-fb";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-no-segundo-ciclo-ej")]
        public async Task<IActionResult> IndexMatriculaPendenteEj([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosIICicloEconomicaJuridicaPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-no-segundo-ciclo-ej";
            return View(alunos);
        }
        #endregion

        #region LISTA PENDENTE DE MATRICULAS DAS ETAPAS  
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-na-etapa-um")]
        public async Task<IActionResult> IndexMatriculaPendenteEtapaUm([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEtapaUmPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-na-etapa-um";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-na-etapa-dois")]
        public async Task<IActionResult> IndexMatriculaPendenteEtapaDois([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEtapaDoisPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-na-etapa-dois";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-pendente-na-etapa-tres")]
        public async Task<IActionResult> IndexMatriculaPendenteEtapaTres([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEtapaTresPendente(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pendente-na-etapa-tres";
            return View(alunos);
        }

        #endregion

        #region LISTA DE MATRICULAS CONCLUIDAS
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-na-iniciacao")]
        public async Task<IActionResult> IndexMatriculaEfetuada([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosIniciacao(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-na-iniciacao";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-no-ensino-primario")]
        public async Task<IActionResult> IndexMatriculaEfetuadaEnsinoPrimario([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosPrimario(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-no-ensino-primario";
            return View(alunos);
        }
      

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-no-primeiro-ciclo")]
        public async Task<IActionResult> IndexMatriculaEfetuadaICiclo([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosICiclo(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-no-primeiro-ciclo";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-no-segundo-ciclo-fb")]
       
        public async Task<IActionResult> IndexMatriculaEfetuadaFb([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosIICicloFisicasBiologica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-no-segundo-ciclo-fb";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-no-segundo-ciclo-ej")]
        public async Task<IActionResult> IndexMatriculaEfetuadaEj([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosIICicloEconomicaJuridica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-no-segundo-ciclo-ej";
            return View(alunos);
        }
        #endregion

        #region LISTA DE MATRICULAS CONCLUIDAS DAS ETAPAS
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-na-etapa-um")]
        public async Task<IActionResult> IndexMatriculaEfetuadaNaEtapaUm([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEtapaUm(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-na-etapa-um";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-na-etapa-dois")]
        public async Task<IActionResult> IndexMatriculaEfetuadaNaEtapaDois([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEtapaDois(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-na-etapa-dois";
            return View(alunos);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-com-matriculas-concluidas-na-etapa-tres")]
        public async Task<IActionResult> IndexMatriculaEfetuadaNaEtapaTres([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEtapaTres(ps, page, q);
            ViewBag.Pesquisa = q;
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-concluidas-na-etapa-tres";
            return View(alunos);
        }

        #endregion

        #endregion

        #region LISTA DE TURMAS COM ALUNOS
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("lista-de-alunos-na-turma")]
        public async Task<IActionResult> IndexMatriculaTurma([FromQuery] string turma)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTurmaComAlunos(turma);
            ViewBag.Pesquisa = turma;
            alunos.ReferenceAction = "lista-de-alunos-na-turma";
            return View(alunos);
        }

        /*
              [ClaimsAuthorize("AlunoMatriculados", "VI")]
             [Route("lista-de-alunos-com-divida-de-propina-turma")]
             public async Task<IActionResult> IndexAlunoComDividaPropinaPorTurma([FromQuery] string turma)
             {
                 var alunos = await _alunoMatriculadoQueries.ObterAlunosComDividaPropinaPorTurma(turma);
                 ViewBag.Pesquisa = turma;
                 alunos.ReferenceAction = "lista-de-alunos-com-divida-de-propina-turma";
                 return View(alunos);
             }
         */
        #endregion

        #region MÉTODO LISTAR DE MATRICULAS PENDENTE PELO ENCARREGADO
        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-na-iniciacao")]
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteIniciacao([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoIniciacao(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-na-iniciacao";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-na-etapa-um")]
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteEtapaUm([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoEtapaUm(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-na-etapa-um";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-na-etapa-dois")]
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteEtapaDois([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoEtapaDois(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-na-etapa-dois";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-na-etapa-tres")]
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteEtapaTres([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoEtapaTres(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-na-etapa-tres";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-no-ensino-primario")]
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteEnsinoPrimario([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoEnsinoPrimario(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-no-ensino-primario";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-no-primeiro-ciclo")]
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteICiclo([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoICiclo(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-no-primeiro-ciclo";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-no-segundo-ciclo-fb")]
     
        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteFb([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoIICicloFisicasBiologica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-no-segundo-ciclo-fb";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-de-alunos-com-matriculas-pelo-encarregado-no-segundo-ciclo-ej")]

        public async Task<IActionResult> IndexEncarregadoMatriculaPendenteEj([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoMatriculadoQueries.ObterTodosEncarregadoIICicloEconomicaJuridica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-com-matriculas-pelo-encarregado-no-segundo-ciclo-ej";
            return View(alunos);
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("dados-do-aluno-matriculado/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var aluno = await ObterPropinasAlunoMatriculado(id);
            //var aluno = await ObterPropinasPorPagamentoPropina(id);
            //var aluno1 = await ObterPropinasPorPagamentoPropina(id);
            //var a = aluno1.Id;
            //ObterPropinaAlunoMatriculado
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("dados-do-aluno-matriculado-ensino-primeiro/{id:guid}")]
        public async Task<IActionResult> DetailsEnsinoPrimario(Guid id)
        {
            var aluno = await ObterPropinasAlunoMatriculado(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("dados-do-aluno-matriculado-primeiro-ciclo/{id:guid}")]
        public async Task<IActionResult> DetailsICiclo(Guid id)
        {
            var aluno = await ObterPropinasAlunoMatriculado(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("dados-do-aluno-matriculado-fb/{id:guid}")]
        public async Task<IActionResult> DetailsIICicloFisicasBiologica(Guid id)
        {
            var aluno = await ObterPropinasAlunoMatriculado(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "VI")]
        [Route("dados-do-aluno-matriculado-ej/{id:guid}")]
        public async Task<IActionResult> DetailsIICicloEconomicaJuridica(Guid id)
        {
            var aluno = await ObterPropinasAlunoMatriculado(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        #endregion

        #region NOVO CADASTRO PELA ESCOLA

        #region MÉTODO PARA CADASTRAR PARA A INICIAÇÃO 
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-iniciacao")]
        public async Task<IActionResult> Create()
        {
            var alunoInscritoInicicao = await PopularAlunoMatriculadoIniciacao(new AlunoMatriculadoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-iniciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoIniciacao(alunoMatriculadoIniciacao);
            
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseId();
          
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoPre(alunoMatriculadoIniciacao)) return RedirectToAction("Create");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("Create");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("Create");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("Create");
            }
            
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion

            if (!await ValidarDataFuncionario()) return RedirectToAction("Create");
            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            //ModelState.Remove("NumeroDeTransacaoDePagamento");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);
            
            if(!await ValidarIdadeIniciacao(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("Create");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexMatriculaPendenteIniciacao");
        }
     
        [HttpPost]
        public async Task<IActionResult> Buscar(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("Create"); }

            //var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumentoIniciacao(aluno.NumDocumento);
            if (alunoInscrito == null)
            {
                TempData["Erro"] = $"Opa ): Não pertence a classe que solicita!";
                return RedirectToAction("Create");
            }

            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("Create");
        }

        #endregion

        #region MÉTODO PARA CADASTRAR PARA ETAPAS 

        #region ETAPA I
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-etapa-um")]
        public async Task<IActionResult> CreateEtapaUm()
        {
            var alunoInscritoInicicao = await PopularAlunoMatriculadoEtapaUm(new AlunoMatriculadoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaUm(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoEtapaUm(alunoMatriculadoIniciacao);
          
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseId();

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoEtapaUm(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEtapaUm");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEtapaUm");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEtapaUm");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEtapaUm");
            }

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion
            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateEtapaUm");
            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeEtapaUm(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEtapaUm");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexMatriculaPendenteEtapaUm");
        }

        [HttpPost]
        public async Task<IActionResult> BuscarEtapaUm(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEtapaUm"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
           
            if (alunoInscrito.Classe.Nome != "I Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence a classe que solicita!";
                return RedirectToAction("CreateEtapaUm");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEtapaUm");
        }
        #endregion

        #region ETAPA II
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-etapa-dois")]
        public async Task<IActionResult> CreateEtapaDois()
        {
            var alunoInscritoInicicao = await PopularAlunoMatriculadoEtapaDois(new AlunoMatriculadoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaDois(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoEtapaDois(alunoMatriculadoIniciacao);
          
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseId();

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoEtapaDois(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEtapaDois");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEtapaDois");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEtapaDois");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEtapaDois");
            }

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion
            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateEtapaDois");
            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeEtapaDois(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEtapaDois");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexMatriculaPendenteEtapaDois");
        }

        [HttpPost]
        public async Task<IActionResult> BuscarEtapaDois(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEtapaDois"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.Classe.Nome != "II Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence a classe que solicita!";
                return RedirectToAction("CreateEtapaDois");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEtapaDois");
        }
        #endregion

        #region ETAPA III
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-etapa-tres")]
        public async Task<IActionResult> CreateEtapaTres()
        {
            var alunoInscritoInicicao = await PopularAlunoMatriculadoEtapaTres(new AlunoMatriculadoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaTres(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoEtapaTres(alunoMatriculadoIniciacao);
          
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaTres();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseId();

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoEtapaTres(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEtapaTres");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEtapaTres");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();
          
            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEtapaTres");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEtapaTres");
            }

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion
            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateEtapaTres");
            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeEtapaTres(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEtapaTres");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexMatriculaPendenteEtapaTres");
        }

        [HttpPost]
        public async Task<IActionResult> BuscarEtapaTres(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEtapaTres"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
          
            if (alunoInscrito.Classe.Nome != "III Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence a classe que solicita!";
                return RedirectToAction("CreateEtapaTres");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEtapaTres");
        }
        #endregion
        #endregion

        #region MÉTODO PARA CADASTRAR PARA ENSINO PRIMARIO
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-ensino-primario")]
        public async Task<IActionResult> CreateEnsinoPrimario()
        {
            var alunoEnsinoPrimario = await PopularAlunoMatriculadoEnsinoPrimario(new AlunoMatriculadoViewModel());
            return View(alunoEnsinoPrimario);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnsinoPrimario(AlunoMatriculadoViewModel alunoMatriculadoEnsinoPrimario)
        {
            alunoMatriculadoEnsinoPrimario = await PopularAlunoMatriculadoEnsinoPrimario(alunoMatriculadoEnsinoPrimario);
          
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoEnsinoPrimario.TurmaId);

            if (!await ValidarNunumerDocumentoEnsinoPrimario(alunoMatriculadoEnsinoPrimario)) return RedirectToAction("CreateEnsinoPrimario");

            var alunoId = alunoMatriculadoEnsinoPrimario.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
         
            if (alunoInscrito == null) return NotFound();
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEnsinoPrimario");
            }
            var turmaId = alunoMatriculadoEnsinoPrimario.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEnsinoPrimario");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEnsinoPrimario");
            }
           
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoEnsinoPrimario.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateEnsinoPrimario");

            #region REMOVER DO MODEL STATE  
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoEnsinoPrimario);
            if (!await ValidarIdadeEnsinoPrimario(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEnsinoPrimario");
            
            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoEnsinoPrimario);
            }

            return RedirectToAction("IndexMatriculaPendenteEnsinoPrimario");
        }
        
        [HttpPost]
        public async Task<IActionResult> BuscarEnsinoPrimario(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEnsinoPrimario"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "Primário")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEnsinoPrimario");
            }

            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateEnsinoPrimario");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA PRIMARIO CICLO
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-primeiro-ciclo")]
        public async Task<IActionResult> CreateICiclo()
        {
            var alunoICiclo = await PopularAlunoMatriculadoICiclo(new AlunoMatriculadoViewModel());
            return View(alunoICiclo);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateICiclo(AlunoMatriculadoViewModel alunoMatriculadoICiclo)
        {
            alunoMatriculadoICiclo = await PopularAlunoMatriculadoICiclo(alunoMatriculadoICiclo);
         
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoICiclo.TurmaId);

            if (!await ValidarNunumerDocumentoPrimeiroCiclo(alunoMatriculadoICiclo)) return RedirectToAction("CreateICiclo");
            var alunoId = alunoMatriculadoICiclo.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito == null) return NotFound();
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateICiclo");
            }
            var turmaId = alunoMatriculadoICiclo.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateICiclo");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateICiclo");
            }

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoICiclo.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateICiclo");

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoICiclo);
            if (!await ValidarIdadeICiclo(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateICiclo");
            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida()) 
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoICiclo);
            }
            return RedirectToAction("IndexMatriculaPendenteICiclo");
        }
       
        [HttpPost]
        public async Task<IActionResult> BuscarICiclo(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateICiclo"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "I Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateICiclo");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateICiclo");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA SEGUNDO CICLO FB
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-segundo-ciclo-fb")]
        public async Task<IActionResult> CreateIICicloFb()
        {
            var alunoIICicloFb = await PopularAlunoMatriculadoIICicloFisicasBiologica(new AlunoMatriculadoViewModel());
            return View(alunoIICicloFb);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloFb(AlunoMatriculadoViewModel alunoMatriculadoIICicloFb)
        {
            alunoMatriculadoIICicloFb = await PopularAlunoMatriculadoIICicloFisicasBiologica(alunoMatriculadoIICicloFb);
            
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloFb();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIICicloFb.TurmaId);

            if (!await ValidarNunumerDocumentoSegundoCicloFb(alunoMatriculadoIICicloFb)) return RedirectToAction("CreateIICicloFb");
            var alunoId = alunoMatriculadoIICicloFb.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito == null) return NotFound();
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateIICicloFb");
            }
            var turmaId = alunoMatriculadoIICicloFb.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateIICicloFb");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateICiclo");
            }
           
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIICicloFb.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateIICicloFb");

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIICicloFb);
            if (!await ValidarIdadeICiclo(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateIICicloFb");
          
            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIICicloFb);
            }
            return RedirectToAction("IndexMatriculaPendenteFb");
        }

        [HttpPost]
        public async Task<IActionResult> BuscarIICicloFb(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateIICicloFb"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
           
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "II Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateIICicloFb");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateIICicloFb");
        }

        #endregion

        #region MÉTODO PARA CADASTRAR PARA SEGUNDO CICLO EJ
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("nova-matricula-para-segundo-ciclo-ej")]
        public async Task<IActionResult> CreateIICicloEj()
        {
            var alunoIICicloEj = await PopularAlunoMatriculadoIICicloEconomicaJuridica(new AlunoMatriculadoViewModel());
            return View(alunoIICicloEj);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [Route("nova-matricula-para-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloEj(AlunoMatriculadoViewModel alunoMatriculadoIICicloEj)
        {
            alunoMatriculadoIICicloEj = await PopularAlunoMatriculadoIICicloEconomicaJuridica(alunoMatriculadoIICicloEj);
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloEj();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIICicloEj.TurmaId);

            if (!await ValidarNunumerDocumentoSegundoCicloEj(alunoMatriculadoIICicloEj)) return RedirectToAction("CreateIICicloEj");

            var alunoId = alunoMatriculadoIICicloEj.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito == null) return NotFound();
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateIICicloEj");
            }
           
            var turmaId = alunoMatriculadoIICicloEj.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateIICicloEj");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateICiclo");
            }

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIICicloEj.TurmaId,
                FuncionarioCaixaId = emailFuncionario.Id,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };
            if (!await ValidarDataFuncionario()) return RedirectToAction("CreateIICicloEj");

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIICicloEj);
            if (!await ValidarIdadeICiclo(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateIICicloEj");
            
            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIICicloEj);
            }
            return RedirectToAction("IndexMatriculaPendenteEj");
        }

        [HttpPost]
        public async Task<IActionResult> BuscarIICicloEj(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateIICicloEj"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "II Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateIICicloEj");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateIICicloEj");
        }
        #endregion

        #endregion

        #region NOVO CADASTRO PELO ENCARREGADO
       
        #region INICIAÇÃO
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-iniciacao")]
        public async Task<IActionResult> CreateEncarregado()
        {
            var alunoInscritoInicicao = await PopularAlunoMatriculadoIniciacao(new AlunoMatriculadoViewModel());
            return View(alunoInscritoInicicao);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-iniciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregado(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoIniciacao(alunoMatriculadoIniciacao);
           
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseId();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoPre(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEncarregado");

           
            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregado");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregado");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregado");
            }

            //#region PEGAR O USUARIO
            //var usuario = HttpContext.User.Identity;
            //var nomeUsuarioLogado = usuario.Name;
            //var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            //#endregion
            //if (!await ValidarDataFuncionario()) return RedirectToAction("CreateEncarregado");
            //alunoMatriculadoIniciacao.FuncionarioCaixaId = encarregado;

            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            //ModelState.Remove("NumeroDeTransacaoDePagamento");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("ValorDaMatricula");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeIniciacao(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregado");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendenteIniciacao");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregado(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregado"); }

            //var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumentoIniciacao(aluno.NumDocumento);
           
            if (alunoInscrito == null)
            {
                TempData["Erro"] = $"Opa ): Não pertence a classe que solicita!";
                return RedirectToAction("CreateEncarregado");
            }

            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEncarregado");
        }

        #endregion

        #region ENSINO PRIMARIO
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-ensino-primario")]
        public async Task<IActionResult> CreateEncarregadoEnsinoPrimario()
        {
            var alunoEnsinoPrimario = await PopularAlunoMatriculadoEnsinoPrimario(new AlunoMatriculadoViewModel());
            return View(alunoEnsinoPrimario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEnsinoPrimario(AlunoMatriculadoViewModel alunoMatriculadoEnsinoPrimario)
        {
            alunoMatriculadoEnsinoPrimario = await PopularAlunoMatriculadoEnsinoPrimario(alunoMatriculadoEnsinoPrimario);
         
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoEnsinoPrimario.TurmaId);
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");
            if (!await ValidarNunumerDocumentoEnsinoPrimario(alunoMatriculadoEnsinoPrimario)) return RedirectToAction("CreateEncarregadoEnsinoPrimario");

            var alunoId = alunoMatriculadoEnsinoPrimario.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoEnsinoPrimario");
            }
            if (alunoInscrito == null) return NotFound();
            var turmaId = alunoMatriculadoEnsinoPrimario.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEnsinoPrimario");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEnsinoPrimario");
            }

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoEnsinoPrimario.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoEnsinoPrimario);
            if (!await ValidarIdadeEnsinoPrimario(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoEnsinoPrimario");

            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoEnsinoPrimario);
            }

            return RedirectToAction("IndexEncarregadoMatriculaPendenteEnsinoPrimario");
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoEnsinoPrimario(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoEnsinoPrimario"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);

            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "Primário")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoEnsinoPrimario");
            }


            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateEncarregadoEnsinoPrimario");
        }
        #endregion

        #region ETAPAS I
      
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-etapa-um")]
        public async Task<IActionResult> CreateEncarregadoEtapaUm()
        {
            var alunoEnsinoPrimario = await PopularAlunoMatriculadoEtapaUm(new AlunoMatriculadoViewModel());
            return View(alunoEnsinoPrimario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEtapaUm(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoEtapaUm(alunoMatriculadoIniciacao);
           
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseIdEtapaUm();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoEtapaUm(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEncarregadoEtapaUm");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoEtapaUm");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEtapaUm");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEtapaUm");
            }
            

            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            //ModelState.Remove("NumeroDeTransacaoDePagamento");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("ValorDaMatricula");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeEtapaUm(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoEtapaUm");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendenteEtapaUm");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoEtapaUm(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoEtapaUm"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);

            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "Etapa I")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoEtapaUm");
            }

            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEncarregadoEtapaUm");
        }
        #endregion

        #region ETAPAS II

        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-etapa-dois")]
        public async Task<IActionResult> CreateEncarregadoEtapaDois()
        {
            var alunoEnsinoPrimario = await PopularAlunoMatriculadoEtapaDois(new AlunoMatriculadoViewModel());
            return View(alunoEnsinoPrimario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEtapaDois(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoEtapaDois(alunoMatriculadoIniciacao);
         
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseIdEtapaDois();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoEtapaDois(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEncarregadoEtapaDois");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoEtapaDois");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEtapaDois");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEtapaDois");
            }


            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            //ModelState.Remove("NumeroDeTransacaoDePagamento");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("ValorDaMatricula");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeEtapaDois(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoEtapaDois");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendenteEtapaDois");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoEtapaDois(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoEtapaDois"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);

            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "Etapa II")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoEtapaDois");
            }

            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEncarregadoEtapaDois");
        }
        #endregion

        #region ETAPAS III
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-etapa-tres")]
        public async Task<IActionResult> CreateEncarregadoEtapaTres()
        {
            var alunoEnsinoPrimario = await PopularAlunoMatriculadoEtapaTres(new AlunoMatriculadoViewModel());
            return View(alunoEnsinoPrimario);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEtapaTres(AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {
            alunoMatriculadoIniciacao = await PopularAlunoMatriculadoEtapaTres(alunoMatriculadoIniciacao);
           
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaTres();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classe = await _classeRepository.ObterClasseIdEtapaDois();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIniciacao.TurmaId);

            if (!await ValidarNunumerDocumentoEtapaTres(alunoMatriculadoIniciacao)) return RedirectToAction("CreateEncarregadoEtapaTres");

            var alunoId = alunoMatriculadoIniciacao.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoEtapaTres");
            }
            if (alunoMatriculadoIniciacao == null) return NotFound();

            var turmaId = alunoMatriculadoIniciacao.TurmaId;

            var turma = await _turmaRepository.ObterTurma(turmaId);

            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEtapaTres");
            }

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoEtapaTres");
            }


            alunoMatriculadoIniciacao.AnoLetivo = CalcularIAnoLetivo();
            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIniciacao.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("CursoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("CodigoAluno");
            ModelState.Remove("Descricao");
            //ModelState.Remove("NumeroDeTransacaoDePagamento");
            ModelState.Remove("Imagem");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("ValorDaMatricula");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("NumDocumento");

            ModelState.Remove("AlunoInscrito.DataCadastro");
            ModelState.Remove("AlunoInscrito.Datanascimento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);

            if (!await ValidarIdadeEtapaDois(classe.Nome, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoEtapaTres");

            var alunoMatriculado = _mapper.Map<AlunoMatriculado>(Matricular);
            await _alunoMatriculadoService.Adicionar(alunoMatriculado);

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIniciacao);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendenteEtapaTres");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoEtapaTres(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoEtapaTres"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "Etapa III")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoEtapaTres");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["DataCadastro"] = alunoInscrito.DataCadastro.ToString("ddMMyyyy"); ;
            TempData["Datanascimento"] = alunoInscrito.Datanascimento.ToString("ddMMyyyy");
            TempData["AnoLetivo"] = alunoInscrito.AnoLetivo;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;
            TempData["Codigo"] = alunoInscrito.Codigo;
            TempData["NiveisDeEnsinoId"] = alunoInscrito.NiveisDeEnsinoId;
            TempData["EncarregadoId"] = alunoInscrito.EncarregadoId;
            TempData["GrauDeParentescoId"] = alunoInscrito.GrauDeParentescoId;
            TempData["AreaDeConhecimentoId"] = alunoInscrito.AreaDeConhecimentoId;
            TempData["NomeDoPai"] = alunoInscrito.NomeDoPai;
            TempData["NomeDaMae"] = alunoInscrito.NomeDaMae;
            TempData["Imagem"] = alunoInscrito.Imagem;
            TempData["TipoDocumento"] = alunoInscrito.TipoDocumento;
            TempData["NumDocumento"] = alunoInscrito.NumDocumento;
            TempData["EscolaDeOrgigem"] = alunoInscrito.EscolaDeOrgigem;
            TempData["Sexo"] = alunoInscrito.Sexo;
            TempData["Endereco"] = alunoInscrito.Endereco;
            TempData["FuncionarioCaixaId"] = alunoInscrito.FuncionarioCaixaId;

            return RedirectToAction("CreateEncarregadoEtapaTres");
        }
        #endregion
       
        #region PRIMARIO CICLO
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-primeiro-ciclo")]
        public async Task<IActionResult> CreateEncarregadoICiclo()
        {
            var alunoICiclo = await PopularAlunoMatriculadoICiclo(new AlunoMatriculadoViewModel());
            return View(alunoICiclo);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoICiclo(AlunoMatriculadoViewModel alunoMatriculadoICiclo)
        {
            alunoMatriculadoICiclo = await PopularAlunoMatriculadoICiclo(alunoMatriculadoICiclo);
          
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoICiclo.TurmaId);
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            if (!await ValidarNunumerDocumentoPrimeiroCiclo(alunoMatriculadoICiclo)) return RedirectToAction("CreateEncarregadoICiclo");
            var alunoId = alunoMatriculadoICiclo.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoICiclo");
            }
            if (alunoInscrito == null) return NotFound();
            var turmaId = alunoMatriculadoICiclo.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoICiclo");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoICiclo");
            }

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoICiclo.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoICiclo);
            if (!await ValidarIdadeICiclo(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoICiclo");
            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoICiclo);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendenteICiclo");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoICiclo(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoICiclo"); }
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "I Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoICiclo");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateEncarregadoICiclo");
        }
        #endregion

        #region SEGUNDO CICLO EJ
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-segundo-ciclo-ej")]
        public async Task<IActionResult> CreateEncarregadoIICicloEj()
        {
            var alunoIICicloEj = await PopularAlunoMatriculadoIICicloEconomicaJuridica(new AlunoMatriculadoViewModel());
            return View(alunoIICicloEj);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoIICicloEj(AlunoMatriculadoViewModel alunoMatriculadoIICicloEj)
        {
            alunoMatriculadoIICicloEj = await PopularAlunoMatriculadoIICicloEconomicaJuridica(alunoMatriculadoIICicloEj);
        
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloEj();
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIICicloEj.TurmaId);
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            if (!await ValidarNunumerDocumentoSegundoCicloEj(alunoMatriculadoIICicloEj)) return RedirectToAction("CreateEncarregadoIICicloEj");

            var alunoId = alunoMatriculadoIICicloEj.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoIICicloEj");
            }
            if (alunoInscrito == null) return NotFound();
            var turmaId = alunoMatriculadoIICicloEj.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoIICicloEj");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoIICicloEj");
            }

            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIICicloEj.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");

            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIICicloEj);
            if (!await ValidarIdadeICiclo(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoIICicloEj");

            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIICicloEj);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendentePendenteEj");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoIICicloEj(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoIICicloEj"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "II Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoIICicloEj");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateEncarregadoIICicloEj");
        }
        #endregion

        #region SEGUNDO CICLO FB
        [AllowAnonymous]
        [Route("nova-matricula-feita-pelo-encarregado-para-segundo-ciclo-fb")]
        public async Task<IActionResult> CreateEncarregadoIICicloFb()
        {
            var alunoIICicloFb = await PopularAlunoMatriculadoIICicloFisicasBiologica(new AlunoMatriculadoViewModel());
            return View(alunoIICicloFb);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-matricula-feita-pelo-encarregado-para-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoIICicloFb(AlunoMatriculadoViewModel alunoMatriculadoIICicloFb)
        {
            alunoMatriculadoIICicloFb = await PopularAlunoMatriculadoIICicloFisicasBiologica(alunoMatriculadoIICicloFb);
         
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloFb();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");
            var classePelaTurma = await _turmaRepository.ObterTurmaClasse(alunoMatriculadoIICicloFb.TurmaId);

            if (!await ValidarNunumerDocumentoSegundoCicloFb(alunoMatriculadoIICicloFb)) return RedirectToAction("CreateEncarregadoIICicloFb");
            var alunoId = alunoMatriculadoIICicloFb.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Pagamento pendente): Este aluno não deve ser matriculado!";
                return RedirectToAction("CreateEncarregadoIICicloFb");
            }
            if (alunoInscrito == null) return NotFound();
            var turmaId = alunoMatriculadoIICicloFb.TurmaId;
            var turma = await _turmaRepository.ObterTurma(turmaId);
            if (turma.NumDeVagas == 0)
            {
                TempData["Erro"] = $"Opa ): Não há vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoIICicloFb");
            }
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma {turma.NomeTurma}!";
                return RedirectToAction("CreateEncarregadoIICicloFb");
            }


            AlunoMatriculadoViewModel Matricular = new()
            {
                ClasseId = classePelaTurma.ClasseId,
                NiveisDeEnsinoId = nivel.Id,
                CursoId = curso.Id,
                AlunoInscritoId = alunoInscrito.Id,
                TurmaId = alunoMatriculadoIICicloFb.TurmaId,
                FuncionarioCaixaId = encarregado,
                Imagem = alunoInscrito.Imagem,
                CodigoAluno = alunoInscrito.Codigo,
                Nome = alunoInscrito.Nome,
                Sexo = alunoInscrito.Sexo,
                NumDocumento = alunoInscrito.NumDocumento,
                EncarregadoId = alunoInscrito.EncarregadoId,
                GrauDeParentescoId = alunoInscrito.GrauDeParentescoId,
                AnoLetivo = CalcularIAnoLetivo(),
                Idade = CalcularIdade(alunoInscrito.Datanascimento),
                ValorDaMatricula = 0,
                Estado = false,
                Bolseiro = false,
            };

            //if (!await ValidarDataFuncionario()) return RedirectToAction("CreateEncarregadoIICicloFb");

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Nome");
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDaMatricula");
            ModelState.Remove("AlunoInscrito.NomeDaMae");
            ModelState.Remove("AlunoInscrito.NomeDoPai");
            ModelState.Remove("AlunoInscrito.Endereco");
            ModelState.Remove("AlunoInscrito.EscolaDeOrgigem");
            ModelState.Remove("AlunoInscrito.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIICicloFb);
            if (!await ValidarIdadeICiclo(classePelaTurma.ClasseId, alunoInscrito.Datanascimento)) return RedirectToAction("CreateEncarregadoIICicloFb");

            await _alunoMatriculadoService.Adicionar(_mapper.Map<AlunoMatriculado>(Matricular));

            if (!OperacaoValida())
            {
                await ManipularNumeroDeVagasNaTurma(turmaId);
                return View(alunoMatriculadoIICicloFb);
            }
            return RedirectToAction("IndexEncarregadoMatriculaPendenteFb");
        }
       
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> BuscarPeloEncarregadoIICicloFb(AlunoInscritoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregadoIICicloFb"); }

            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito.NiveisDeEnsino.NomeNiveisDeEnsino != "II Ciclo")
            {
                TempData["Erro"] = $"Opa ): Não pertence ao nível de ensino que solicita!";
                return RedirectToAction("CreateEncarregadoIICicloFb");
            }
            TempData["Id"] = alunoInscrito.Id;
            TempData["Nome"] = alunoInscrito.Nome;
            TempData["Idade"] = alunoInscrito.Idade;
            TempData["Classe"] = alunoInscrito.Classe.Nome;

            return RedirectToAction("CreateEncarregadoIICicloFb");
        }

        #endregion

        #endregion

        #region MÉTODO PARA EDITAR

        #region MÉTODO PARA EDITAR MATRICULAR  INICIAÇÂO
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-na-iniciacao/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoIniciacao(id);
            ViewBag.Nome = aluno.Nome;

            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("matricular-na-iniciacao/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, AlunoMatriculadoViewModel alunoMatriculadoIniciacao)
        {

            if (id != alunoMatriculadoIniciacao.Id) return NotFound();
            
            var alunoMatriculadoIniciacaoAtualizacao = await ObterAluno(id);
            //var alunoMatriculadoIniciacaoAtualizacao = await ObterAlunoMatriculadoIniciacao(id);
            var turmaId = alunoMatriculadoIniciacao.TurmaId;
            
            //var nomeTurma = alunoMatriculadoIniciacaoAtualizacao.Turma.NomeTurma;
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
           
            var mes= alunoMatriculadoIniciacao.QuantidadeMes;
            alunoMatriculadoIniciacao.AlunoInscrito = alunoMatriculadoIniciacaoAtualizacao.AlunoInscrito;
            if (alunoMatriculadoIniciacao.ValorDaMatricula > 0 && alunoMatriculadoIniciacao.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("Edit");
            }
            if (alunoMatriculadoIniciacao.QuantidadeMes == 0 && alunoMatriculadoIniciacao.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("Edit");
            }
            if (alunoMatriculadoIniciacao.ValorDaMatricula == 0 && alunoMatriculadoIniciacao.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): O valor da matrícula não foi inserido!";
                return RedirectToAction("Edit");
            }
           
            //alunoMatriculadoIniciacao.ClasseId = alunoMatriculadoIniciacao.ClasseId;
            //alunoMatriculadoIniciacao.NiveisDeEnsinoId = nivel.Id;
            //alunoMatriculadoIniciacao.CursoId = curso.Id;

            //alunoMatriculadoIniciacaoAtualizacao.TurmaId = alunoMatriculadoIniciacao.TurmaId;
            //alunoMatriculadoIniciacaoAtualizacao.EncarregadoId = alunoMatriculadoIniciacao.EncarregadoId;
            //alunoMatriculadoIniciacaoAtualizacao.GrauDeParentescoId = alunoMatriculadoIniciacao.GrauDeParentescoId;
            //alunoMatriculadoIniciacaoAtualizacao.ValorDaMatricula = alunoMatriculadoIniciacao.ValorDaMatricula;

            //alunoMatriculadoIniciacaoAtualizacao.Estado = true;
            alunoMatriculadoIniciacao.ClasseId = alunoMatriculadoIniciacaoAtualizacao.ClasseId;
            alunoMatriculadoIniciacao.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoIniciacao.CursoId = curso.Id;
            alunoMatriculadoIniciacao.TurmaId = alunoMatriculadoIniciacao.TurmaId;
            alunoMatriculadoIniciacao.EncarregadoId = alunoMatriculadoIniciacaoAtualizacao.EncarregadoId;
            alunoMatriculadoIniciacao.GrauDeParentescoId = alunoMatriculadoIniciacaoAtualizacao.GrauDeParentescoId;
            alunoMatriculadoIniciacao.ValorDaMatricula = alunoMatriculadoIniciacao.ValorDaMatricula;
            alunoMatriculadoIniciacao.AnoLetivo = alunoMatriculadoIniciacaoAtualizacao.AnoLetivo;
            alunoMatriculadoIniciacao.Nome = alunoMatriculadoIniciacaoAtualizacao.Nome;
            alunoMatriculadoIniciacao.Imagem = alunoMatriculadoIniciacaoAtualizacao.Imagem;
            alunoMatriculadoIniciacao.Estado = true;
            alunoMatriculadoIniciacao.NumDocumento = alunoMatriculadoIniciacaoAtualizacao.NumDocumento;
            alunoMatriculadoIniciacao.Sexo = alunoMatriculadoIniciacaoAtualizacao.Sexo;
            alunoMatriculadoIniciacao.Idade = alunoMatriculadoIniciacaoAtualizacao.Idade;
            alunoMatriculadoIniciacao.Bolseiro = alunoMatriculadoIniciacao.Bolseiro;
            
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Nome");
            ModelState.Remove("Descricao");
            ModelState.Remove("NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIniciacao);
          
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("Edit");
            #endregion
            
            //var pagamentoId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
            
            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoIniciacao.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoIniciacao, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIniciacao, mes))
                //if (!await AlterarPagamentoPropinaAoMatricular(id, alunoMatriculadoIniciacao, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("IndexMatriculaPendenteIniciacao");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIniciacao, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("Edit");
                }
            }

            alunoMatriculadoIniciacao.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoIniciacao.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoIniciacao.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoIniciacao);
                }

                alunoMatriculadoIniciacao.Imagem = imgPrefixo + alunoMatriculadoIniciacao.ImagemUpload.FileName;
            }

          

            await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoIniciacaoAtualizacao));

            if (!OperacaoValida()) return View(alunoMatriculadoIniciacao);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("Edit");
            }

            return RedirectToAction("IndexMatriculaEfetuada");
        }

        public async Task<IActionResult> VoltarIniciacao(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoIniciacao(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await VoltarNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }
            return RedirectToAction("IndexMatriculaPendenteIniciacao");
            //return View(aluno);
        }

        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA ENSINO PRIMARIO
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-no-ensino-primario/{id:guid}")]
        public async Task<IActionResult> EditEnsinoPrimario(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoEnsinoPrimario(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("matricular-no-ensino-primario/{id:guid}")]
        public async Task<IActionResult> EditEnsinoPrimario(Guid id, AlunoMatriculadoViewModel alunoMatriculadoEnsinoPrimario)
        {
            if (id != alunoMatriculadoEnsinoPrimario.Id) return NotFound();
            var alunoMatriculadoEnsinoPrimarioAtualizacao = await ObterAlunoMatriculadoEnsinoPrimario(id);
            var turmaId = alunoMatriculadoEnsinoPrimario.TurmaId;
            var nomeTurma = alunoMatriculadoEnsinoPrimarioAtualizacao.Turma.NomeTurma;

            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var mes = alunoMatriculadoEnsinoPrimario.QuantidadeMes;
            var turmaClasseId = alunoMatriculadoEnsinoPrimario.TurmaId;
            if (alunoMatriculadoEnsinoPrimario.ValorDaMatricula > 0 && alunoMatriculadoEnsinoPrimario.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditEnsinoPrimario");
            }
            if (alunoMatriculadoEnsinoPrimario.QuantidadeMes == 0 && alunoMatriculadoEnsinoPrimario.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditEnsinoPrimario");
            }
            if (alunoMatriculadoEnsinoPrimario.ValorDaMatricula == 0 && alunoMatriculadoEnsinoPrimario.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): O valor da matrícula não foi inserido!";
                return RedirectToAction("EditEnsinoPrimario");
            }
            var classeId = alunoMatriculadoEnsinoPrimario.ClasseId;
            //if (nivel.NomeNiveisDeEnsino == "Primário")
            //{
                if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditEnsinoPrimario");
            //}

            alunoMatriculadoEnsinoPrimario.ClasseId = alunoMatriculadoEnsinoPrimarioAtualizacao.ClasseId;
            alunoMatriculadoEnsinoPrimario.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoEnsinoPrimario.CursoId = curso.Id;
            alunoMatriculadoEnsinoPrimario.TurmaId = alunoMatriculadoEnsinoPrimario.TurmaId;
            alunoMatriculadoEnsinoPrimario.EncarregadoId = alunoMatriculadoEnsinoPrimarioAtualizacao.EncarregadoId;
            alunoMatriculadoEnsinoPrimario.GrauDeParentescoId = alunoMatriculadoEnsinoPrimarioAtualizacao.GrauDeParentescoId;
            alunoMatriculadoEnsinoPrimario.ValorDaMatricula = alunoMatriculadoEnsinoPrimario.ValorDaMatricula;
            alunoMatriculadoEnsinoPrimario.Bolseiro = alunoMatriculadoEnsinoPrimario.Bolseiro;
            alunoMatriculadoEnsinoPrimario.AnoLetivo = alunoMatriculadoEnsinoPrimarioAtualizacao.AnoLetivo;
            alunoMatriculadoEnsinoPrimario.Nome = alunoMatriculadoEnsinoPrimarioAtualizacao.Nome;
            alunoMatriculadoEnsinoPrimario.Imagem = alunoMatriculadoEnsinoPrimarioAtualizacao.Imagem;
            alunoMatriculadoEnsinoPrimario.Estado = true;
            alunoMatriculadoEnsinoPrimario.NumDocumento = alunoMatriculadoEnsinoPrimarioAtualizacao.NumDocumento;
            alunoMatriculadoEnsinoPrimario.Sexo = alunoMatriculadoEnsinoPrimarioAtualizacao.Sexo;
            alunoMatriculadoEnsinoPrimario.Idade = alunoMatriculadoEnsinoPrimarioAtualizacao.Idade;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago"); 
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoEnsinoPrimario);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditEnsinoPrimario");
            #endregion
            
            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoEnsinoPrimario.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoEnsinoPrimario, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEnsinoPrimario, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEnsinoPrimario, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
 
            alunoMatriculadoEnsinoPrimario.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoEnsinoPrimario.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoEnsinoPrimario.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoEnsinoPrimario);
                }
                //alunoMatriculadoEnsinoPrimario.Imagem = imgPrefixo + alunoMatriculadoEnsinoPrimario.ImagemUpload.FileName;
                alunoMatriculadoEnsinoPrimario.Imagem = imgPrefixo + alunoMatriculadoEnsinoPrimario.ImagemUpload.FileName;
            }

            await _alunoMatriculadoRepository.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoEnsinoPrimario));
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditEnsinoPrimario");
            }
        
            return RedirectToAction("IndexMatriculaEfetuadaEnsinoPrimario");
        }
        public async Task<IActionResult> VoltarEnsinoPrimario(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoEnsinoPrimario(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await VoltarNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }
            return RedirectToAction("IndexMatriculaPendenteEnsinoPrimario");
            //return View(aluno);
        }

        public IActionResult BuscarEnsinoPrimario(string mumDocumento, AlunoMatriculadoViewModel alunoMatriculadoEnsinoPrimario)
        {
            if (mumDocumento != alunoMatriculadoEnsinoPrimario.NumDocumento) return NotFound();
         
            return RedirectToAction("CreateEnsinoPrimario");
        }
        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA PRIMARIO CICLO
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-no-primeiro-ciclo/{id:guid}")]
        public async Task<IActionResult> EditICiclo(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoICiclo(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("matricular-no-primeiro-ciclo/{id:guid}")]
        public async Task<IActionResult> EditICiclo(Guid id, AlunoMatriculadoViewModel alunoMatriculadoICiclo)
        {

            if (id != alunoMatriculadoICiclo.Id) return NotFound();
            //var classe = await _classeRepository.ObterClasseId();
            var alunoMatriculadoICicloAtualizacao = await ObterAlunoMatriculadoICiclo(id);
            var turmaId = alunoMatriculadoICiclo.TurmaId;
            //var nomeTurma = alunoMatriculadoICicloAtualizacao.Turma.NomeTurma;
            //var nomeAluno = alunoMatriculadoICicloAtualizacao.Nome;
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var mes = alunoMatriculadoICiclo.QuantidadeMes;
            var turmaClasseId = alunoMatriculadoICiclo.TurmaId;

            if (alunoMatriculadoICiclo.ValorDaMatricula > 0 && alunoMatriculadoICiclo.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditICiclo");
            }
            if (alunoMatriculadoICiclo.QuantidadeMes == 0 && alunoMatriculadoICiclo.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditICiclo");
            }

            if (alunoMatriculadoICiclo.ValorDaMatricula == 0 && alunoMatriculadoICiclo.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve fazer o pagamento da matrícula!";
                return RedirectToAction("EditICiclo");
            }
            var classeId = alunoMatriculadoICiclo.ClasseId;
            if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditICiclo");

            /*
              alunoMatriculadoICiclo.ClasseId = alunoMatriculadoICicloAtualizacao.ClasseId;
              alunoMatriculadoICiclo.NiveisDeEnsinoId = nivel.Id;
              alunoMatriculadoICiclo.CursoId = curso.Id;
              alunoMatriculadoICicloAtualizacao.EncarregadoId = alunoMatriculadoICiclo.EncarregadoId;
              alunoMatriculadoICicloAtualizacao.GrauDeParentescoId = alunoMatriculadoICiclo.GrauDeParentescoId;
              alunoMatriculadoICicloAtualizacao.ValorDaMatricula = alunoMatriculadoICiclo.ValorDaMatricula;

              alunoMatriculadoICicloAtualizacao.Estado = true;
              */
            alunoMatriculadoICiclo.ClasseId = alunoMatriculadoICicloAtualizacao.ClasseId;
            alunoMatriculadoICiclo.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoICiclo.CursoId = curso.Id;
            alunoMatriculadoICiclo.TurmaId = alunoMatriculadoICiclo.TurmaId;
            alunoMatriculadoICiclo.EncarregadoId = alunoMatriculadoICicloAtualizacao.EncarregadoId;
            alunoMatriculadoICiclo.GrauDeParentescoId = alunoMatriculadoICicloAtualizacao.GrauDeParentescoId;
            alunoMatriculadoICiclo.ValorDaMatricula = alunoMatriculadoICiclo.ValorDaMatricula;
            alunoMatriculadoICiclo.AnoLetivo = alunoMatriculadoICicloAtualizacao.AnoLetivo;
            alunoMatriculadoICiclo.Nome = alunoMatriculadoICicloAtualizacao.Nome;
            alunoMatriculadoICiclo.Imagem = alunoMatriculadoICicloAtualizacao.Imagem;
            alunoMatriculadoICiclo.Estado = true;
            alunoMatriculadoICiclo.NumDocumento = alunoMatriculadoICicloAtualizacao.NumDocumento;
            alunoMatriculadoICiclo.Sexo = alunoMatriculadoICicloAtualizacao.Sexo;
            alunoMatriculadoICiclo.Idade = alunoMatriculadoICicloAtualizacao.Idade;


            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago");
            //ModelState.Remove("PercentualDesconto");
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoICiclo);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditICiclo");
            #endregion

            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoICiclo.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoICiclo, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoICiclo, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditICiclo");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoICiclo, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditICiclo");
                }
            }
 
            alunoMatriculadoICiclo.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoICiclo.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoICiclo.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoICiclo);
                }

                alunoMatriculadoICiclo.Imagem = imgPrefixo + alunoMatriculadoICiclo.ImagemUpload.FileName;
            }
      
            //alunoMatriculadoICicloAtualizacao.Nome = alunoMatriculadoICiclo.Nome;

            //alunoMatriculadoICiclo.NumDocumento = alunoMatriculadoICicloAtualizacao.NumDocumento;


            //if (!await ValidarIdadeICiclo(alunoMatriculadoICiclo)) return RedirectToAction("Edit");
            //if (!await ValidarIdadeICiclo(classe.Nome, datanascimento)) return RedirectToAction("Edit");
             
            await _alunoMatriculadoRepository.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoICiclo));

            //await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoICicloAtualizacao));

            //if (!OperacaoValida()) return View(alunoMatriculadoICiclo);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditICiclo");
            }

            return RedirectToAction("IndexMatriculaEfetuadaICiclo");
        }
        public async Task<IActionResult> VoltarICiclo(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoICiclo(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await VoltarNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }
            return RedirectToAction("IndexMatriculaPendenteICiclo");
            //return View(aluno);
        }
        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA ETAPA UM
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-na-etapa-um/{id:guid}")]
        public async Task<IActionResult> EditEtapaUm(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoEtapaI(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("matricular-na-etapa-um/{id:guid}")]
        public async Task<IActionResult> EditEtapaUm(Guid id, AlunoMatriculadoViewModel alunoMatriculadoEtapaI)
        {

            if (id != alunoMatriculadoEtapaI.Id) return NotFound();
            var alunoMatriculadoEtapaIAtualizacao = await ObterAlunoMatriculadoEtapaI(id);
            var turmaId = alunoMatriculadoEtapaI.TurmaId;
            //var nomeTurma = alunoMatriculadoEtapaIAtualizacao.Turma.NomeTurma;

            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var mes = alunoMatriculadoEtapaI.QuantidadeMes;
            var turmaClasseId = alunoMatriculadoEtapaI.TurmaId;

            if (alunoMatriculadoEtapaI.ValorDaMatricula > 0 && alunoMatriculadoEtapaI.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditEtapaUm");
            }
            if (alunoMatriculadoEtapaI.QuantidadeMes == 0 && alunoMatriculadoEtapaI.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditEtapaUm");
            }
            if (alunoMatriculadoEtapaI.ValorDaMatricula == 0 && alunoMatriculadoEtapaI.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): O valor da matrícula não foi inserido!";
                return RedirectToAction("EditEtapaUm");
            }
        
            var classeId = alunoMatriculadoEtapaI.ClasseId;
            if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditEtapaUm");

            //alunoMatriculadoEtapaI.ClasseId = alunoMatriculadoEtapaIAtualizacao.ClasseId;
            //alunoMatriculadoEtapaI.NiveisDeEnsinoId = nivel.Id;
            //alunoMatriculadoEtapaI.CursoId = curso.Id;
            //alunoMatriculadoEtapaIAtualizacao.EncarregadoId = alunoMatriculadoEtapaI.EncarregadoId;
            //alunoMatriculadoEtapaIAtualizacao.GrauDeParentescoId = alunoMatriculadoEtapaI.GrauDeParentescoId;
            //alunoMatriculadoEtapaIAtualizacao.ValorDaMatricula = alunoMatriculadoEtapaI.ValorDaMatricula;
            //alunoMatriculadoEtapaIAtualizacao.Estado = true;
            alunoMatriculadoEtapaI.ClasseId = alunoMatriculadoEtapaIAtualizacao.ClasseId;
            alunoMatriculadoEtapaI.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoEtapaI.CursoId = curso.Id;
            alunoMatriculadoEtapaI.TurmaId = alunoMatriculadoEtapaI.TurmaId;
            alunoMatriculadoEtapaI.EncarregadoId = alunoMatriculadoEtapaIAtualizacao.EncarregadoId;
            alunoMatriculadoEtapaI.GrauDeParentescoId = alunoMatriculadoEtapaIAtualizacao.GrauDeParentescoId;
            alunoMatriculadoEtapaI.ValorDaMatricula = alunoMatriculadoEtapaI.ValorDaMatricula;
            alunoMatriculadoEtapaI.AnoLetivo = alunoMatriculadoEtapaIAtualizacao.AnoLetivo;
            alunoMatriculadoEtapaI.Nome = alunoMatriculadoEtapaIAtualizacao.Nome;
            alunoMatriculadoEtapaI.Imagem = alunoMatriculadoEtapaIAtualizacao.Imagem;
            alunoMatriculadoEtapaI.Estado = true;
            alunoMatriculadoEtapaI.NumDocumento = alunoMatriculadoEtapaIAtualizacao.NumDocumento;
            alunoMatriculadoEtapaI.Sexo = alunoMatriculadoEtapaIAtualizacao.Sexo;
            alunoMatriculadoEtapaI.Idade = alunoMatriculadoEtapaIAtualizacao.Idade;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago");
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoEtapaI);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditEtapaUm");
            #endregion

            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoEtapaI.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoEtapaI, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEtapaI, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEtapaUm");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEtapaI, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditEtapaUm");
                }
            }

            alunoMatriculadoEtapaI.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoEtapaI.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoEtapaI.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoEtapaI);
                }

                alunoMatriculadoEtapaI.Imagem = imgPrefixo + alunoMatriculadoEtapaI.ImagemUpload.FileName;
            }

            await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoEtapaIAtualizacao));

            if (!OperacaoValida()) return View(alunoMatriculadoEtapaI);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditEtapaUm");
            }

            return RedirectToAction("IndexMatriculaEfetuadaNaEtapaUm");
        }

        //public IActionResult BuscarEtapaII(string mumDocumento, AlunoMatriculadoViewModel alunoMatriculadoEnsinoPrimario)
        //{
        //    if (mumDocumento != alunoMatriculadoEnsinoPrimario.NumDocumento) return NotFound();

        //    return RedirectToAction("CreateEtapaUm");
        //}
        
        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA ETAPA DOIS
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-na-etapa-dois/{id:guid}")]
        public async Task<IActionResult> EditEtapaDois(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoEtapaII(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("matricular-na-etapa-dois/{id:guid}")]
        public async Task<IActionResult> EditEtapaDois(Guid id, AlunoMatriculadoViewModel alunoMatriculadoEtapaII)
        {

            if (id != alunoMatriculadoEtapaII.Id) return NotFound();
            var alunoMatriculadoEtapaIIAtualizacao = await ObterAlunoMatriculadoEtapaII(id);
            var turmaId = alunoMatriculadoEtapaII.TurmaId;
            //var nomeTurma = alunoMatriculadoEtapaIIAtualizacao.Turma.NomeTurma;

            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var mes = alunoMatriculadoEtapaII.QuantidadeMes;
            var turmaClasseId = alunoMatriculadoEtapaII.TurmaId;

            if (alunoMatriculadoEtapaII.ValorDaMatricula > 0 && alunoMatriculadoEtapaII.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditEtapaDois");
            }
            if (alunoMatriculadoEtapaII.QuantidadeMes == 0 && alunoMatriculadoEtapaII.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditEtapaDois");
            }

            if (alunoMatriculadoEtapaII.ValorDaMatricula == 0 && alunoMatriculadoEtapaII.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): O valor da matrícula não foi inserido!";
                return RedirectToAction("EditEtapaDois");
            }
          
            var classeId = alunoMatriculadoEtapaII.ClasseId;
            if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditEtapaDois");

            //alunoMatriculadoEtapaII.ClasseId = alunoMatriculadoEtapaIIAtualizacao.ClasseId;
            //alunoMatriculadoEtapaII.NiveisDeEnsinoId = nivel.Id;
            //alunoMatriculadoEtapaII.CursoId = curso.Id;
            //alunoMatriculadoEtapaIIAtualizacao.EncarregadoId = alunoMatriculadoEtapaII.EncarregadoId;
            //alunoMatriculadoEtapaIIAtualizacao.GrauDeParentescoId = alunoMatriculadoEtapaII.GrauDeParentescoId;
            //alunoMatriculadoEtapaIIAtualizacao.ValorDaMatricula = alunoMatriculadoEtapaII.ValorDaMatricula;
            //alunoMatriculadoEtapaIIAtualizacao.Estado = true;
            alunoMatriculadoEtapaII.ClasseId = alunoMatriculadoEtapaIIAtualizacao.ClasseId;
            alunoMatriculadoEtapaII.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoEtapaII.CursoId = curso.Id;
            alunoMatriculadoEtapaII.TurmaId = alunoMatriculadoEtapaII.TurmaId;
            alunoMatriculadoEtapaII.EncarregadoId = alunoMatriculadoEtapaIIAtualizacao.EncarregadoId;
            alunoMatriculadoEtapaII.GrauDeParentescoId = alunoMatriculadoEtapaIIAtualizacao.GrauDeParentescoId;
            alunoMatriculadoEtapaII.ValorDaMatricula = alunoMatriculadoEtapaII.ValorDaMatricula;
            alunoMatriculadoEtapaII.AnoLetivo = alunoMatriculadoEtapaIIAtualizacao.AnoLetivo;
            alunoMatriculadoEtapaII.Nome = alunoMatriculadoEtapaIIAtualizacao.Nome;
            alunoMatriculadoEtapaII.Imagem = alunoMatriculadoEtapaIIAtualizacao.Imagem;
            alunoMatriculadoEtapaII.Estado = true;
            alunoMatriculadoEtapaII.NumDocumento = alunoMatriculadoEtapaIIAtualizacao.NumDocumento;
            alunoMatriculadoEtapaII.Sexo = alunoMatriculadoEtapaIIAtualizacao.Sexo;
            alunoMatriculadoEtapaII.Idade = alunoMatriculadoEtapaIIAtualizacao.Idade;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago");
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoEtapaII);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditEtapaDois");
            #endregion

            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoEtapaII.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoEtapaII, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEtapaII, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEtapaII");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEtapaII, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditEtapaII");
                }
            } 

            alunoMatriculadoEtapaII.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoEtapaII.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoEtapaII.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoEtapaII);
                }

                alunoMatriculadoEtapaII.Imagem = imgPrefixo + alunoMatriculadoEtapaII.ImagemUpload.FileName;
            }


            await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoEtapaIIAtualizacao));

            if (!OperacaoValida()) return View(alunoMatriculadoEtapaII);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditEtapaDois");
            }

            return RedirectToAction("IndexMatriculaEfetuadaNaEtapaDois");
        }
 
        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA ETAPA TRES
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-na-etapa-tres/{id:guid}")]
        public async Task<IActionResult> EditEtapaTres(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoEtapaIII(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("matricular-na-etapa-tres/{id:guid}")]
        public async Task<IActionResult> EditEtapaTres(Guid id, AlunoMatriculadoViewModel alunoMatriculadoEtapaIII)
        {

            if (id != alunoMatriculadoEtapaIII.Id) return NotFound();
            var alunoMatriculadoEtapaIIIAtualizacao = await ObterAlunoMatriculadoEtapaII(id);
            var turmaId = alunoMatriculadoEtapaIII.TurmaId;
            //var nomeTurma = alunoMatriculadoEtapaIIIAtualizacao.Turma.NomeTurma;

            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var mes = alunoMatriculadoEtapaIII.QuantidadeMes;
            var turmaClasseId = alunoMatriculadoEtapaIII.TurmaId;

            if (alunoMatriculadoEtapaIII.ValorDaMatricula > 0 && alunoMatriculadoEtapaIII.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditEtapaTres");
            }
            if (alunoMatriculadoEtapaIII.QuantidadeMes == 0 && alunoMatriculadoEtapaIII.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditEtapaTres");
            }
            if (alunoMatriculadoEtapaIII.ValorDaMatricula == 0 && alunoMatriculadoEtapaIII.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): O valor da matrícula não foi inserido!";
                return RedirectToAction("EditEtapaTres");
            }
          
            var classeId = alunoMatriculadoEtapaIII.ClasseId;
            if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditEtapaTres");

            //alunoMatriculadoEtapaIII.ClasseId = alunoMatriculadoEtapaIIIAtualizacao.ClasseId;
            //alunoMatriculadoEtapaIII.NiveisDeEnsinoId = nivel.Id;
            //alunoMatriculadoEtapaIII.CursoId = curso.Id;
            //alunoMatriculadoEtapaIIIAtualizacao.EncarregadoId = alunoMatriculadoEtapaIII.EncarregadoId;
            //alunoMatriculadoEtapaIIIAtualizacao.GrauDeParentescoId = alunoMatriculadoEtapaIII.GrauDeParentescoId;
            //alunoMatriculadoEtapaIIIAtualizacao.ValorDaMatricula = alunoMatriculadoEtapaIII.ValorDaMatricula;
            //alunoMatriculadoEtapaIIIAtualizacao.Estado = true;
            alunoMatriculadoEtapaIII.ClasseId = alunoMatriculadoEtapaIIIAtualizacao.ClasseId;
            alunoMatriculadoEtapaIII.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoEtapaIII.CursoId = curso.Id;
            alunoMatriculadoEtapaIII.TurmaId = alunoMatriculadoEtapaIII.TurmaId;
            alunoMatriculadoEtapaIII.EncarregadoId = alunoMatriculadoEtapaIIIAtualizacao.EncarregadoId;
            alunoMatriculadoEtapaIII.GrauDeParentescoId = alunoMatriculadoEtapaIIIAtualizacao.GrauDeParentescoId;
            alunoMatriculadoEtapaIII.ValorDaMatricula = alunoMatriculadoEtapaIII.ValorDaMatricula;
            alunoMatriculadoEtapaIII.AnoLetivo = alunoMatriculadoEtapaIIIAtualizacao.AnoLetivo;
            alunoMatriculadoEtapaIII.Nome = alunoMatriculadoEtapaIIIAtualizacao.Nome;
            alunoMatriculadoEtapaIII.Imagem = alunoMatriculadoEtapaIIIAtualizacao.Imagem;
            alunoMatriculadoEtapaIIIAtualizacao.Estado = true;
            //alunoMatriculadoEtapaIII.Estado = true;
            alunoMatriculadoEtapaIII.NumDocumento = alunoMatriculadoEtapaIIIAtualizacao.NumDocumento;
            alunoMatriculadoEtapaIII.Sexo = alunoMatriculadoEtapaIIIAtualizacao.Sexo;
            alunoMatriculadoEtapaIII.Idade = alunoMatriculadoEtapaIIIAtualizacao.Idade;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago");
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoEtapaIII);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditEtapaTres");
            #endregion
            
            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoEtapaIII.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoEtapaIII, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEtapaIII, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEtapaTres");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoEtapaIII, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditEtapaTres");
                }
            }
 
            alunoMatriculadoEtapaIII.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoEtapaIII.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoEtapaIII.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoEtapaIII);
                }

                alunoMatriculadoEtapaIII.Imagem = imgPrefixo + alunoMatriculadoEtapaIII.ImagemUpload.FileName;
            }

            await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoEtapaIIIAtualizacao));

            if (!OperacaoValida()) return View(alunoMatriculadoEtapaIII);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditEtapaTres");
            }

            return RedirectToAction("IndexMatriculaEfetuadaNaEtapaTres");
        }

        
        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA  II CILO FISICAS BIOLOGICAS
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-no-segundo-ciclo-fb/{id:guid}")]
        public async Task<IActionResult> EditIICicloFb(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoIICicloFb(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("matricular-no-segundo-ciclo-fb/{id:guid}")]
        public async Task<IActionResult> EditIICicloFb(Guid id, AlunoMatriculadoViewModel alunoMatriculadoIICicloFb)
        {
            
            if (id != alunoMatriculadoIICicloFb.Id) return NotFound();

            var alunoMatriculadoIICicloFbAtualizacao = await ObterAlunoMatriculadoIICicloFb(id);
            var turmaId = alunoMatriculadoIICicloFbAtualizacao.TurmaId;
           
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloFb();
            var mes = alunoMatriculadoIICicloFb.QuantidadeMes;
            var turmaClasseId = alunoMatriculadoIICicloFb.TurmaId;

            if (alunoMatriculadoIICicloFb.ValorDaMatricula > 0 && alunoMatriculadoIICicloFb.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditIICicloFb");
            }
            if (alunoMatriculadoIICicloFb.QuantidadeMes == 0 && alunoMatriculadoIICicloFb.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditIICicloFb");
            }
            if (alunoMatriculadoIICicloFb.ValorDaMatricula == 0 && alunoMatriculadoIICicloFb.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve fazer o pagamento da matrícula!";
                return RedirectToAction("EditIICicloFb");
            }
            var classeId = alunoMatriculadoIICicloFb.ClasseId;
            if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditIICicloFb");


            //alunoMatriculadoIICicloFb.ClasseId = alunoMatriculadoIICicloFbAtualizacao.ClasseId;
            //alunoMatriculadoIICicloFb.NiveisDeEnsinoId = nivel.Id;
            //alunoMatriculadoIICicloFb.CursoId = curso.Id;
            //alunoMatriculadoIICicloFbAtualizacao.EncarregadoId = alunoMatriculadoIICicloFb.EncarregadoId;
            //alunoMatriculadoIICicloFbAtualizacao.GrauDeParentescoId = alunoMatriculadoIICicloFb.GrauDeParentescoId;
            //alunoMatriculadoIICicloFbAtualizacao.ValorDaMatricula = alunoMatriculadoIICicloFb.ValorDaMatricula;

            //alunoMatriculadoIICicloFbAtualizacao.Estado = true;
            alunoMatriculadoIICicloFb.ClasseId = alunoMatriculadoIICicloFbAtualizacao.ClasseId;
            alunoMatriculadoIICicloFb.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoIICicloFb.CursoId = curso.Id;
            alunoMatriculadoIICicloFb.TurmaId = alunoMatriculadoIICicloFb.TurmaId;
            alunoMatriculadoIICicloFb.EncarregadoId = alunoMatriculadoIICicloFbAtualizacao.EncarregadoId;
            alunoMatriculadoIICicloFb.GrauDeParentescoId = alunoMatriculadoIICicloFbAtualizacao.GrauDeParentescoId;
            alunoMatriculadoIICicloFb.ValorDaMatricula = alunoMatriculadoIICicloFb.ValorDaMatricula;
            alunoMatriculadoIICicloFb.AnoLetivo = alunoMatriculadoIICicloFbAtualizacao.AnoLetivo;
            alunoMatriculadoIICicloFb.Nome = alunoMatriculadoIICicloFbAtualizacao.Nome;
            alunoMatriculadoIICicloFb.Imagem = alunoMatriculadoIICicloFbAtualizacao.Imagem;
            alunoMatriculadoIICicloFb.Estado = true;
            alunoMatriculadoIICicloFb.NumDocumento = alunoMatriculadoIICicloFbAtualizacao.NumDocumento;
            alunoMatriculadoIICicloFb.Sexo = alunoMatriculadoIICicloFbAtualizacao.Sexo;
            alunoMatriculadoIICicloFb.Idade = alunoMatriculadoIICicloFbAtualizacao.Idade;
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago");
            //ModelState.Remove("PercentualDesconto");
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIICicloFb);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditIICicloFb");
            #endregion

            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoIICicloFb.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoIICicloFb, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIICicloFb, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditIICicloFb");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIICicloFb, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditIICicloFb");
                }
            }
  
            alunoMatriculadoIICicloFb.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoIICicloFb.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoIICicloFb.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoIICicloFb);
                }

                alunoMatriculadoIICicloFb.Imagem = imgPrefixo + alunoMatriculadoIICicloFb.ImagemUpload.FileName;
            }

            if (alunoMatriculadoIICicloFb.ValorDaMatricula == 0)
            {
                TempData["Erro"] = $"Opa ): Deve fazer o pagamento da matrícula!";
                return RedirectToAction("EditIICicloFb");
            }
            await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoIICicloFbAtualizacao));

            if (!OperacaoValida()) return View(alunoMatriculadoIICicloFb);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditIICicloFb");
            }
            return RedirectToAction("IndexMatriculaEfetuadaFb");
        }
        public async Task<IActionResult> VoltarFb(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoIICicloFb(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await VoltarNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }
            return RedirectToAction("IndexMatriculaPendenteFb");
            //return View(aluno);
        }
        #endregion

        #region MÉTODO PARA EDITAR MATRICULAR PARA II CILO ECONOMICAS E JURIDICAS
        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [Route("matricular-no-segundo-ciclo-ej/{id:guid}")]
        public async Task<IActionResult> EditIICicloEj(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoIICicloEj(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await ManipularNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [ClaimsAuthorize("AlunoMatriculados", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("matricular-no-segundo-ciclo-ej/{id:guid}")]
        public async Task<IActionResult> EditIICicloEj(Guid id, AlunoMatriculadoViewModel alunoMatriculadoIICicloEj)
        {
            if (id != alunoMatriculadoIICicloEj.Id) return NotFound();
           
            var alunoMatriculadoIICicloEjAtualizacao = await ObterAlunoMatriculadoIICicloEj(id);
            var turmaId = alunoMatriculadoIICicloEj.TurmaId;
         
            
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var curso = await _cursoRepository.ObterCursoIICicloEj();
            var mes = alunoMatriculadoIICicloEj.QuantidadeMes;

            var turmaClasseId = alunoMatriculadoIICicloEj.TurmaId;

            if (alunoMatriculadoIICicloEj.ValorDaMatricula > 0 && alunoMatriculadoIICicloEj.Bolseiro == true)
            {
                TempData["Erro"] = $"Opa ): O bolseiro não paga o valor da matricula!";
                return RedirectToAction("EditIICicloEj");
            }
            if (alunoMatriculadoIICicloEj.QuantidadeMes == 0 && alunoMatriculadoIICicloEj.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve selecionar o números de meses a pagar!";
                return RedirectToAction("EditIICicloEj");
            }
            if (alunoMatriculadoIICicloEj.ValorDaMatricula == 0 && alunoMatriculadoIICicloEj.Bolseiro == false)
            {
                TempData["Erro"] = $"Opa ): Deve fazer o pagamento da matrícula!";
                return RedirectToAction("EditIICicloEj");
            }
            var classeId = alunoMatriculadoIICicloEj.ClasseId;
            if (!await ValidarTurmaClasseComClasseId(turmaClasseId, classeId)) return RedirectToAction("EditIICicloEj");


            //alunoMatriculadoIICicloEj.ClasseId = alunoMatriculadoIICicloEjAtualizacao.ClasseId;
            //alunoMatriculadoIICicloEj.NiveisDeEnsinoId = nivel.Id;
            //alunoMatriculadoIICicloEj.CursoId = curso.Id;
            //alunoMatriculadoIICicloEjAtualizacao.EncarregadoId = alunoMatriculadoIICicloEj.EncarregadoId;
            //alunoMatriculadoIICicloEjAtualizacao.GrauDeParentescoId = alunoMatriculadoIICicloEj.GrauDeParentescoId;
            //alunoMatriculadoIICicloEjAtualizacao.ValorDaMatricula = alunoMatriculadoIICicloEj.ValorDaMatricula;

            //alunoMatriculadoIICicloEjAtualizacao.Estado = true;
            alunoMatriculadoIICicloEj.ClasseId = alunoMatriculadoIICicloEjAtualizacao.ClasseId;
            alunoMatriculadoIICicloEj.NiveisDeEnsinoId = nivel.Id;
            alunoMatriculadoIICicloEj.CursoId = curso.Id;
            alunoMatriculadoIICicloEj.TurmaId = alunoMatriculadoIICicloEj.TurmaId;
            alunoMatriculadoIICicloEj.EncarregadoId = alunoMatriculadoIICicloEjAtualizacao.EncarregadoId;
            alunoMatriculadoIICicloEj.GrauDeParentescoId = alunoMatriculadoIICicloEjAtualizacao.GrauDeParentescoId;
            alunoMatriculadoIICicloEj.ValorDaMatricula = alunoMatriculadoIICicloEj.ValorDaMatricula;
            alunoMatriculadoIICicloEj.AnoLetivo = alunoMatriculadoIICicloEjAtualizacao.AnoLetivo;
            alunoMatriculadoIICicloEj.Nome = alunoMatriculadoIICicloEjAtualizacao.Nome;
            alunoMatriculadoIICicloEj.Imagem = alunoMatriculadoIICicloEjAtualizacao.Imagem;
            alunoMatriculadoIICicloEj.Estado = true;
            alunoMatriculadoIICicloEj.NumDocumento = alunoMatriculadoIICicloEjAtualizacao.NumDocumento;
            alunoMatriculadoIICicloEj.Sexo = alunoMatriculadoIICicloEjAtualizacao.Sexo;
            alunoMatriculadoIICicloEj.Idade = alunoMatriculadoIICicloEjAtualizacao.Idade;

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("Nome");
            ModelState.Remove("Sexo");
            ModelState.Remove("Idade");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            ModelState.Remove("TotalPago");
            //ModelState.Remove("PercentualDesconto");
            ModelState.Remove("Descricao");
            #endregion

            if (!ModelState.IsValid) return View(alunoMatriculadoIICicloEj);

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario()) return RedirectToAction("EditIICicloEj");
            #endregion

            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            if (propinaId != null && alunoMatriculadoIICicloEj.Bolseiro == true)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                    await _propinaRepository.Remover(propinasId.Id);
                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAlunoBolseiro(id, alunoMatriculadoIICicloEj, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditEnsinoPrimario");
                }
            }
            else if (propinaId != null)
            {
                var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == propinaId.PagamentoPropinaId && c.AnoLetivo == CalcularIAnoLetivo())
                .Select(c => c.PagamentoPropinaId).Count();

                for (int propinas = 1; propinas <= contador; propinas++)
                {
                    var propinasId = await _propinaRepository.ObterPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());

                    await _propinaRepository.Remover(propinasId.Id);

                    await _context.SaveChangesAsync();
                    _context.ChangeTracker.Clear();
                }

                var pagamentoId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoAnoLetivo(id, CalcularIAnoLetivo());
                await _pagamentoPropinaRepository.Remover(pagamentoId.Id);

                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIICicloEj, mes))
                {
                    TempData["Erro"] = $"Opa ): O número de transação de pagamento infomado já existe!";
                    return RedirectToAction("EditIICicloEj");
                }
            }
            else
            {
                if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIICicloEj, mes))
                {
                    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
                    return RedirectToAction("EditIICicloEj");
                }
            }
            //if (!await PagarPropinaAoMatricular(id, alunoMatriculadoIICicloEj, mes))
            //{
            //    TempData["Erro"] = $"Opa ): Já existe um aluno com este número de transação de pagamento informado!";
            //    //TempData["Erro"] = $"Opa ): Algo deu errado ao pagar a propina do(a) {nomeAluno}!";
            //    return RedirectToAction("EditIICicloEj");
            //}
            alunoMatriculadoIICicloEj.FuncionarioCaixaId = emailFuncionario.Id;

            if (alunoMatriculadoIICicloEj.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoMatriculadoIICicloEj.ImagemUpload, imgPrefixo))
                {
                    return View(alunoMatriculadoIICicloEj);
                }
                alunoMatriculadoIICicloEj.Imagem = imgPrefixo + alunoMatriculadoIICicloEj.ImagemUpload.FileName;
            }
            if (alunoMatriculadoIICicloEj.ValorDaMatricula == 0)
            {
                TempData["Erro"] = $"Opa ): Deve fazer o pagamento da matrícula!";
                return RedirectToAction("EditIICicloEj");
            }
            await _alunoMatriculadoService.Atualizar(_mapper.Map<AlunoMatriculado>(alunoMatriculadoIICicloEjAtualizacao));

            if (!OperacaoValida()) return View(alunoMatriculadoIICicloEj);

            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                return RedirectToAction("EditIICicloEj");
            }
            return RedirectToAction("IndexMatriculaEfetuadaEj");
        }
        public async Task<IActionResult> VoltarEj(Guid id)
        {
            var aluno = await ObterAlunoMatriculadoIICicloEj(id);
            ViewBag.Nome = aluno.Nome;
            var turmaId = aluno.TurmaId;
            await VoltarNumeroDeVagasNaTurma(turmaId);
            if (aluno == null)
            {
                return NotFound();
            }
            return RedirectToAction("IndexMatriculaPendenteEj");
            //return View(aluno);
        }
        #endregion

        #endregion

        #region MÉTODO PARA EXCLUIR

        #region EXCLUIR INICIAÇÂO
        [ClaimsAuthorize("AlunoMatriculados", "DG")]
        [Route("excluir-aluno-matriculado/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //var aluno = await ObterAlunoMatriculadoIniciacao(id);
            var aluno = await ObterAluno(id);
          
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);

        }

        [ClaimsAuthorize("AlunoMatriculados", "DG")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-aluno-matriculado/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            var aluno = await ObterAluno(id);
            //var aluno = await ObterAlunoMatriculadoIniciacao(id);
            var propinaId = await _propinaRepository.ObterPropinaPeloAluno(id);
            var pagamentoPropinaId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAluno(id);
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var turmaId = aluno.TurmaId;
            var alunoCurso = curso.Nome;
            //var p = propinaId.Id;
            //var pg = pagamentoPropinaId.Id;
            var alunoNivel = aluno.NiveisDeEnsino;
            if (aluno == null) return NotFound();


            await _pagamentoPropinaRepository.Remover(pagamentoPropinaId.Id);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            //_context.Remove(propinaId.Id);
            await _propinaRepository.Remover(propinaId.Id);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

  
            await _alunoMatriculadoRepository.Remover(id);

            //if (!OperacaoValida()) return View(aluno);
            if (!await AlterarNumeroDeVagasNaTurma(turmaId))
            {
                TempData["Erro"] = $"Opa ): Algo deu errado ao Alterar o número de vagas na turma!";
                if (alunoNivel.NomeNiveisDeEnsino.Equals("Iniciação"))
                {
                    return RedirectToAction("IndexMatriculaEfetuada");
                }
                if (alunoNivel.NomeNiveisDeEnsino.Equals("Primário"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaEnsinoPrimario");
                }
                if (alunoNivel.NomeNiveisDeEnsino.Equals("I Ciclo"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaICiclo");
                }

                if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa I"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaNaEtapaUm");
                }
                if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa II"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaNaEtapaDois");
                }
                if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa III"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaNaEtapaTres");
                }

                if (alunoCurso.Equals("Ciências Fisicas e Biologicas"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaFb");
                }
                if (alunoCurso.Equals("Ciências Economicas e Jurídicas"))
                {
                    return RedirectToAction("IndexMatriculaEfetuadaEj");
                }
            }

            TempData["Sucesso"] = "Pagamento excluido com sucesso!";

            if (alunoNivel.NomeNiveisDeEnsino.Equals("Iniciação"))
            {
                return RedirectToAction("IndexMatriculaEfetuada");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Primário"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaEnsinoPrimario");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("I Ciclo"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaICiclo");
            }
           
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa I"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaNaEtapaUm");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa II"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaNaEtapaDois");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa III"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaNaEtapaTres");
            }

            if (alunoCurso.Equals("Ciências Fisicas e Biologicas"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaFb");
            }
            if (alunoCurso.Equals("Ciências Economicas e Jurídicas"))
            {
                return RedirectToAction("IndexMatriculaEfetuadaEj");
            }

            return RedirectToAction("IndexMatriculaEfetuada");
        }

        #endregion

        //#region EXCLUIR PRIMARIO
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-ensino-primario/{id:guid}")]
        //public async Task<IActionResult> DeleteEnsinoPrimario(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aluno);

        //}

        //[HttpPost, ActionName("DeleteEnsinoPrimario")]
        //[ValidateAntiForgeryToken]
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-ensino-primario/{id:guid}")]
        //public async Task<IActionResult> DeleteEnsinoPrimarioConfirmed(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null) return NotFound();
        //    var alunoMatriculado = await _alunoMatriculadoRepository.ObterAlunoInscritoJaMatriculado(id);
        //    if (alunoMatriculado != null)
        //    {
        //        TempData["Erro"] = $"O aluno já está matriculado por isso não é permitido apagar :(";
        //        return View(aluno);
        //    }
        //    //await _alunoInscritoRepository.Remover(id);
        //    await _alunoInscritoService.Remover(id);
        //    if (!OperacaoValida()) return View(aluno);

        //    TempData["Sucesso"] = "Candidadto excluido com sucesso!";
        //    return RedirectToAction("IndexEnsinoPrimario");
        //}

        //#endregion

        //#region EXCLUIR I CICLO
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-primeiro-ciclo/{id:guid}")]
        //public async Task<IActionResult> DeleteICiclo(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aluno);

        //}

        //[HttpPost, ActionName("DeleteICiclo")]
        //[ValidateAntiForgeryToken]
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-primeiro-ciclo/{id:guid}")]
        //public async Task<IActionResult> DeleteICicloConfirmed(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null) return NotFound();
        //    var alunoMatriculado = await _alunoMatriculadoRepository.ObterAlunoInscritoJaMatriculado(id);
        //    if (alunoMatriculado != null)
        //    {
        //        TempData["Erro"] = $"O aluno já está matriculado por isso não é permitido apagar :(";
        //        return View(aluno);
        //    }

        //    await _alunoInscritoIniciacaoService.Remover(id);
        //    if (!OperacaoValida()) return View(aluno);

        //    TempData["Sucesso"] = "Candidadto excluido com sucesso!";
        //    return RedirectToAction("IndexICiclo");
        //}

        //#endregion

        //#region EXCLUIR FB
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-fb/{id:guid}")]
        //public async Task<IActionResult> DeleteIICicloFisicasBiologica(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aluno);

        //}

        //[HttpPost, ActionName("DeleteIICicloFisicasBiologica")]
        //[ValidateAntiForgeryToken]
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-fb/{id:guid}")]
        //public async Task<IActionResult> DeleteIICicloFisicasBiologicaConfirmed(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null) return NotFound();
        //    var alunoMatriculado = await _alunoMatriculadoRepository.ObterAlunoInscritoJaMatriculado(id);
        //    if (alunoMatriculado != null)
        //    {
        //        TempData["Erro"] = $"O aluno já está matriculado por isso não é permitido apagar :(";
        //        return View(aluno);
        //    }
        //    await _alunoInscritoIniciacaoService.Remover(id);
        //    if (!OperacaoValida()) return View(aluno);

        //    TempData["Sucesso"] = "Candidadto excluido com sucesso!";
        //    return RedirectToAction("IndexIICicloFisicasBiologica");
        //}

        //#endregion

        //#region EXCLUIR EJ
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-ej/{id:guid}")]
        //public async Task<IActionResult> DeleteIICicloEconomicaJuridica(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aluno);

        //}

        //[HttpPost, ActionName("DeleteIICicloEconomicaJuridica")]
        //[ValidateAntiForgeryToken]
        //[ClaimsAuthorize("AlunoInscritos", "DG")]
        //[Route("excluir-aluno-inscrito-para-ej/{id:guid}")]
        //public async Task<IActionResult> DeleteIICicloEconomicaJuridicaConfirmed(Guid id)
        //{
        //    var aluno = await ObterAlunoInscrito(id);

        //    if (aluno == null) return NotFound();
        //    var alunoMatriculado = await _alunoMatriculadoRepository.ObterAlunoInscritoJaMatriculado(id);
        //    if (alunoMatriculado != null)
        //    {
        //        TempData["Erro"] = $"O aluno já está matriculado por isso não é permitido apagar :(";
        //        return View(aluno);
        //    }
        //    await _alunoInscritoIniciacaoService.Remover(id);
        //    if (!OperacaoValida()) return View(aluno);

        //    TempData["Sucesso"] = "Candidadto excluido com sucesso!";
        //    return RedirectToAction("IndexIICicloEconomicaJuridica");
        //}

        //#endregion

        #endregion

        public IActionResult ImprimirMatricula(AlunoMatriculadoViewModel alunoMatriculadoViewModel)
        {
            AlunoMatriculadoViewModel modelo = _context.AlunoMatriculados.Include(dv => dv.Propinas).Include(dv => dv.PagamentoPropinas).Where(v => v.Id == alunoMatriculadoViewModel.Id)
              .Select(v => new AlunoMatriculadoViewModel()
              {
                  Id = v.Id,
                  CodigoAluno = v.CodigoAluno,
                  NumDocumento = v.NumDocumento,
                  NomeClasse = v.Classe.Nome,
                  NomeTurma = v.Turma.NomeTurma,
                  Nome = v.Nome,
                  Imagem = v.Imagem,
                  Idade = v.Idade,
                  AnoLetivo = v.AnoLetivo,
                  ValorDaMatricula = v.ValorDaMatricula,
                  DataCadastro = v.DataCadastro,
                  AnoLetivoCodigo = "JI_" + v.AnoLetivo + '_' + v.CodigoAluno,
                  PagamentoPropinaDetalhe = v.PagamentoPropinas.Where(v => v.Ativo == true && v.PagamentoMaticula == true).Select(pg => new PagamentoPropinaViewModel()
                  {
                      Id = pg.Id,
                      Codigo = pg.Codigo,
                      Nome = pg.AlunoMatriculado.Nome,
                      Documento = pg.AlunoMatriculado.NumDocumento,
                      ValorDesconto = pg.ValorDesconto,
                      TotalPago = pg.TotalPago,
                  }).ToList(),

                  PropinaDetalhe = v.Propinas.Where(v => ((int)v.Situacao) == 1 && v.PagamentoPropina.PagamentoMaticula == true).Select(dv => new PropinaViewModel()
                  {
                      DescricaoPropina = "Pagamento" + " " + dv.DescricaoPropina,
                      PrecoPropina = dv.PrecoPropina,
                  }).ToList()
              }).FirstOrDefault();

            return new ViewAsPdf("ImprimirMatricula", modelo)
            {
                FileName = $"Matricula {modelo.AnoLetivoCodigo}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };

        }

        public async Task<IActionResult> ImprimirAlunoNaTurma(AlunoMatriculadoViewModel alunoMatriculadoViewModel)
        {
            #region ANO LECTIVO
            string anoLetivo;
            DateTime agora = DateTime.Today;
            int ano = agora.Year;
            int mes = agora.Month;
            int dia = agora.Day;

            if (mes <= 6 && dia <= 31)
            {
                int anoTransato = ano - 1;
                anoLetivo = $"{anoTransato}-{ano}";
            }
            else
            {
                int anoAtual = ano + 1;
                anoLetivo = $"{ano}-{anoAtual}";
            }
            #endregion

            var alunos = await _alunoMatriculadoRepository.ObterTurmaDoAluno(alunoMatriculadoViewModel.Id, anoLetivo);
            if (alunos==null)
            {
                TempData["Erro"] = $"Opa ): Primeiro deves a pesquisar a lista da turma!";
                return RedirectToAction("IndexMatriculaTurma");
            }

            string turma = alunos.Turma.NomeTurma;
           var alunosNaTurma = _mapper.Map<IEnumerable<AlunoMatriculadoViewModel>>(await _alunoMatriculadoRepository.ObterTurmaComAlunos(turma));
           
            return new ViewAsPdf("ImprimirAlunoNaTurma", alunosNaTurma)
            {
                FileName = $"Turma {turma}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        public async Task<IActionResult> ImprimirAlunoNaTurmaComDivida(AlunoMatriculadoViewModel alunoMatriculadoViewModel)
        {
            #region ANO LECTIVO
            string anoLetivo;
            DateTime agora = DateTime.Today;
            int ano = agora.Year;
            int mes = agora.Month;
            int dia = agora.Day;

            if (mes <= 6 && dia <= 31)
            {
                int anoTransato = ano - 1;
                anoLetivo = $"{anoTransato}-{ano}";
            }
            else
            {
                int anoAtual = ano + 1;
                anoLetivo = $"{ano}-{anoAtual}";
            }
            #endregion

            var alunos = await _alunoMatriculadoRepository.ObterTurmaDoAluno(alunoMatriculadoViewModel.Id, anoLetivo);
            if (alunos == null)
            {
                TempData["Erro"] = $"Opa ): Primeiro deves a pesquisar a lista da turma!";
                return RedirectToAction("IndexAlunoComDividaPropinaPorTurma");
            }

            string turma = alunos.Turma.NomeTurma;
            var alunosNaTurma = _mapper.Map<IEnumerable<AlunoMatriculadoViewModel>>(await _alunoMatriculadoRepository.ObterTurmaComAlunos(turma));

            return new ViewAsPdf("ImprimirAlunoNaTurmaComDivida", alunosNaTurma)
            {
                FileName = $"Turma {turma}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        #region METODOS PRIVADOS DA CONTROLLER

        #region POPULAR
        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoIniciacao(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIniciacao());
            return aluno;
        }

        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoEnsinoPrimario(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEnsinoPrimario());
            return aluno;
        }

        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoEtapaUm(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaUm());
            return aluno;
        }

        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoEtapaDois(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaDois());
            return aluno;
        }

        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoEtapaTres(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaTres());
            return aluno;
        }

        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoICiclo(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasICiclo());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoIICicloFisicasBiologica(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIICicloFb());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> PopularAlunoMatriculadoIICicloEconomicaJuridica(AlunoMatriculadoViewModel aluno)
        {
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIICicloEj());
            return aluno;
        }

        #endregion

        #region OBTER
        private async Task<AlunoMatriculadoViewModel> ObterAluno(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculado(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterTodos());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterTodos());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTodos());
            aluno.NiveisDeEnsinos = _mapper.Map<IEnumerable<NiveisDeEnsinoViewModel>>(await _niveisDeEnsinoRepository.ObterTodos());
            aluno.Cursos = _mapper.Map<IEnumerable<CursoViewModel>>(await _cursoRepository.ObterTodos());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            aluno.FuncionarioCaixas = _mapper.Map<IEnumerable<FuncionarioCaixaViewModel>>(await _funcionarioCaixaRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoIniciacao(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIniciacao());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIniciacao());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoEnsinoPrimario(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEnsinoPrimeiro());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoPrimario());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEnsinoPrimario());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoEtapaI(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaUm());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaUm());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaUm());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoEtapaII(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaDois());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaDois());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaDois());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoEtapaIII(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaTres());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaTres());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasEtapaTres());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoICiclo(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauICiclo());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoICiclo());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasICiclo());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoIICicloEj(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloEj());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloEj());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIICicloEj());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculadoIICicloFb(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            aluno.AlunoInscritos = _mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloFb());
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloFb());
            aluno.Turmas = _mapper.Map<IEnumerable<TurmaViewModel>>(await _turmaRepository.ObterTurmasIICicloFb());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterPropinasAlunoMatriculado(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterPropinasAlunoMatriculado(id));
            return aluno;
        }
        private async Task<AlunoMatriculadoViewModel> ObterPropinasPorPagamentoPropina(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterPropinasPorPagamentoPropina(id));
            return aluno;
        }
        #endregion

        #region VALIDAR
         private async Task<bool> ValidarDataFuncionario()
         {
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
          
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);

            if (emailFuncionario == null)
            {
                TempData["Erro"] = "Opa! Este Funcionário não Existe, deve solicitar ao Administrador :(";
                return false;
            }

            if (emailFuncionario.Ativo == false)
            {
                TempData["Erro"] = "Opa! Este Funcionário não tem permissão para esta operação, deve solicitar ao Administrador :(";
                return false;
            }
            return true;
         }
         private async Task<bool> ValidarTurmaClasseComClasseId(Guid turmaClasseId, Guid classeId)
         {
            var t = await _turmaRepository.ObterTurma(turmaClasseId);
            var turmaClasseObitida = await _classeRepository.ObterClasse(t.ClasseId);
            var classeObitida = await _classeRepository.ObterClasse(classeId);

            if (t.ClasseId != classeId)
            {
                TempData["Erro"] = $"Opa ): Está Matricular o aluno na {classeObitida.Nome} classe mas a turma que pretende inseri-lo é da {turmaClasseObitida.Nome} classe!";
                return false;
            }
            return true;
         }

         #region DOCUMENTO
        private async Task<bool> ValidarDocumento(AlunoInscritoViewModel aluno)
        {
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscritoPorDocumento(aluno.NumDocumento);
            if (alunoInscrito == null)
            {
                TempData["Erro"] = $"Opa ): Este documento com o número '{aluno.NumDocumento}', não existe!";
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarNunumerDocumentoPre(AlunoMatriculadoViewModel alunoMatriculado)
        {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "Pré")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarNunumerDocumentoEtapaUm(AlunoMatriculadoViewModel alunoMatriculado)
        {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "I Ciclo")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarNunumerDocumentoEtapaDois(AlunoMatriculadoViewModel alunoMatriculado)
        {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "II Ciclo")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarNunumerDocumentoEtapaTres(AlunoMatriculadoViewModel alunoMatriculado)
        {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "III Ciclo")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
        }
         private async Task<bool> ValidarNunumerDocumentoEnsinoPrimario(AlunoMatriculadoViewModel alunoMatriculado)
         {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "1ª" && alunoInscrito.Classe.Nome != "2ª"
              && alunoInscrito.Classe.Nome != "3ª" && alunoInscrito.Classe.Nome != "4ª"
              && alunoInscrito.Classe.Nome != "5ª" && alunoInscrito.Classe.Nome != "6ª")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
         }
         private async Task<bool> ValidarNunumerDocumentoPrimeiroCiclo(AlunoMatriculadoViewModel alunoMatriculado)
         {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "7ª" && alunoInscrito.Classe.Nome != "8ª"
              && alunoInscrito.Classe.Nome != "9ª")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
         }
         private async Task<bool> ValidarNunumerDocumentoSegundoCicloFb(AlunoMatriculadoViewModel alunoMatriculado)
         {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "10ª" && alunoInscrito.Classe.Nome != "11ª"
              && alunoInscrito.Classe.Nome != "12ª")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
         }
         private async Task<bool> ValidarNunumerDocumentoSegundoCicloEj(AlunoMatriculadoViewModel alunoMatriculado)
         {
            var alunoId = alunoMatriculado.AlunoInscrito.Id;
            var alunoInscrito = await _alunoInscritoRepository.ObterAlunoInscrito(alunoId);
            if (alunoInscrito.Classe.Nome != "10ª" && alunoInscrito.Classe.Nome != "11ª"
              && alunoInscrito.Classe.Nome != "12ª")
            {
                TempData["Erro"] = $"Opa ): A classe que pretende matricular não corresponde com a classe selecionada!";
                return false;
            }
            return true;
         }
         #endregion

         #region IDADE
        private async Task<bool> ValidarIdadeIniciacao(string classeMatricula, DateTime datanascimento)
        {
             var classe = await _classeRepository.ObterClasseId();
             #region CALCULAR IDADE
             DateTime agora = DateTime.Today;
             int idade = agora.Year - datanascimento.Year;
             #endregion

             if ((idade > 5 || idade < 5) && classe.Nome == classeMatricula)
             {
                 TempData["Erro"] = $"O aluno tem {idade} anos de idade de idade! Deve ter 5 anos a completar até Dezembro deste ano :(";
                 return false;
             }
             
             return true;
        }
        private async Task<bool> ValidarIdadeEtapaUm(string classeMatricula, DateTime datanascimento)
        {
            var classe = await _classeRepository.ObterClasseId();
            #region CALCULAR IDADE
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            #endregion

            if ((idade > 14 || idade < 8) && classe.Nome == classeMatricula)
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade de idade! Deve ter no mínimo 8 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeEtapaDois(string classeMatricula, DateTime datanascimento)
        {
            var classe = await _classeRepository.ObterClasseId();
            #region CALCULAR IDADE
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            #endregion

            if ((idade > 14 || idade < 10) && classe.Nome == classeMatricula)
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade de idade! Deve ter no mínimo 10 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeEtapaTres(string classeMatricula, DateTime datanascimento)
        {
            var classe = await _classeRepository.ObterClasseId();
            #region CALCULAR IDADE
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            #endregion

            if ((idade > 14 || idade < 12) && classe.Nome == classeMatricula)
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade de idade! Deve ter no mínimo 12 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeEnsinoPrimario(Guid classeId, DateTime datanascimento)
        {
               var classeObitida = await _classeRepository.ObterClasse(classeId);
               var classe = classeObitida.Nome;
            /*
             //6, 7 e 8
             //7, 8 e 9 
            //8, 9 e 10 
             //9, 10 e 11 
         //11, 12 e 13 
             */
            #region CALCULAR IDADE
            DateTime agora = DateTime.Today;
               int idade = agora.Year - datanascimento.Year;
            #endregion
               if ((idade > 8 || idade < 6) && classe == "1ª")
               //if ((idade > 7 || idade < 6) && classe == "1ª")
               {
                   TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 6 anos  a completar até Dezembro deste ano :(";
                      return false;
               }
               if ((idade > 9 || idade < 7) && classe == "2ª")
                //if ((idade > 8 || idade < 7) && classe == "2ª")
               {
                   TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 7 anos  a completar até Dezembro deste ano :(";
                   return false;
               }

               if ((idade > 10 || idade < 8) && classe == "3ª")
               //if ((idade > 9 || idade < 8) && classe == "3ª")
               {
                      TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 8 anos  a completar até Dezembro deste ano :(";
                      return false;
               }

               if ((idade > 11 || idade < 8) && classe == "4ª")
               //if ((idade > 10 || idade < 9) && classe == "4ª")
               {
                      TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 9 anos a completar até Dezembro deste ano :(";
                      return false;
               }
               if ((idade > 12 || idade < 10) && classe == "5ª") //10, 11 e 12 
               //if ((idade > 11 || idade < 10) && classe == "5ª")
               {
                   TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 10 anos a completar até Dezembro deste ano :(";
                   return false;
               }

               if ((idade > 13 || idade < 11) && classe == "6ª")
                //if ((idade > 12 || idade < 11) && classe == "6ª")
               {
                   TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 11 anos a completar até Dezembro deste ano :(";
                   return false;
               }
               return true;
        }
        private async Task<bool> ValidarIdadeICiclo(Guid classeId, DateTime datanascimento)
        {
            var classeObitida = await _classeRepository.ObterClasse(classeId);
            var classe = classeObitida.Nome;
            #region CALCULAR IDADE
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            #endregion

            if (idade < 12 && classe == "7ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 12 anos a completar até Dezembro deste ano :(";
                return false;
            }
            if (idade < 13 && classe == "8ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 13 anos a completar até Dezembro deste ano :(";
                return false;
            }
            if (idade < 14 && classe == "9ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 14 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeIICiclo(Guid classeId, DateTime datanascimento)
        {
            var classeObitida = await _classeRepository.ObterClasse(classeId);
            var classe = classeObitida.Nome;

            #region CALCULAR IDADE
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
         
            #endregion
            if (idade < 14 && classe == "10ª")

            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 15 anos a completar até Dezembro deste ano :(";
                return false;
            }
            if (idade < 15 && classe == "11ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 16 anos a completar até Dezembro deste ano :(";
                return false;
            }
            if (idade < 16 && classe == "12ª")
            //if ((idadeAluno > 17 && idadeAluno < 16) && classe == "12ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 17 anos a completar até Dezembro deste ano :(";
                return false;
            }
            return true;
        }
         #endregion

        #endregion

        #region UploadIMAGEM
        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using var stream = new FileStream(path, FileMode.Create);
            await arquivo.CopyToAsync(stream);

            return true;
        }
        #endregion

        #region CALCULAR
        private static string CalcularIAnoLetivo()
        {
            string anoLetivo;
            //DateTime agora = new DateTime(2025,7,1, 00,00,00);
            DateTime agora = DateTime.Today;
            int ano = agora.Year;
            int mes = agora.Month;
            int dia = agora.Day;

            //if (mes < 8 && dia <= 31)
            if (mes <= 6 && dia <= 31)
            {
                int anoTransato = ano - 1;
                anoLetivo = $"{anoTransato}-{ano}";
            }
            else
            {
                int anoAtual = ano + 1;
                anoLetivo = $"{ano}-{anoAtual}";
            }
            return anoLetivo;
        }
        private static int CalcularIdade(DateTime datanascimento)
        {
            int idadeAluno;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade))
            {
                idadeAluno = idade - 1;
            }
            else
            {
                idadeAluno = idade;
            }

            return idadeAluno;
        }
        #endregion

        #region AUXILIAR AO CADASTRO
        private async Task<bool> PagarPropinaAlunoBolseiro(Guid alunoMatriculadoId, AlunoMatriculadoViewModel alunoMatriculado, int mes)
        {
            var alunoId = await ObterAlunoMatriculado(alunoMatriculadoId);
            var classeObitida = await _classeRepository.ObterClasse(alunoId.ClasseId);
            var preco = classeObitida.PrecoPropina;
            var classeDeExame = classeObitida.ClassDeExame;
            //var nomeMesPrimeiro = await _mesRepository.ObterMesPeloCodigo(1);
            //var nomeMesUltimo = await _mesRepository.ObterMesPeloCodigo(11);
            //var nomeMesUltimo1 = await _mesRepository.ObterMesPeloCodigo(10);
            PagamentoPropinaViewModel PagamentoPropinas = new()
            {
                AlunoMatriculadoId = alunoId.Id,
                ValorDesconto = 0,
                TotalPago = 0,
                TipoPagamento = 5,
                Ativo = true,
                PagamentoMaticula = false,
                FuncionarioCaixaId = alunoId.FuncionarioCaixaId,
                PrecoPropina = preco,
                NumeroDeMeses = 0,
            };
            if (classeDeExame == true)
            {
                PagamentoPropinas.Descricao = "Pago os meses de Setembro a Julho";
                await _pagamentoPropinaService.Adicionar(_mapper.Map<PagamentoPropina>(PagamentoPropinas));
            }
            else
            {
                PagamentoPropinas.Descricao = "Pago os meses de Setembro a Junho";
                await _pagamentoPropinaService.Adicionar(_mapper.Map<PagamentoPropina>(PagamentoPropinas));
            }

           
            _context.ChangeTracker.Clear();
                var pagamentoPropina1Id = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoMatriculado(alunoMatriculadoId);
                if (pagamentoPropina1Id == null)
                {
                    return false;
                }

                PropinaViewModel propina1 = new()
                {
                    AlunoMatriculadoId = alunoId.Id,
                    ClasseId = alunoId.ClasseId,
                    TurmaId = alunoId.TurmaId,
                    AnoLetivo = alunoId.AnoLetivo,
                    PagamentoPropinaId = pagamentoPropina1Id.Id

                };

                for (int meses = 1; meses <= 11; meses++)
                {
                    var mesPropina = await _mesRepository.ObterMesPeloCodigo(meses);
                        if (classeDeExame == true)
                        {
                            propina1.MesId = mesPropina.Id;
                            propina1.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                            propina1.PrecoPropina = 0;
                            propina1.Situacao = 5;

                            var propinalunoMatriculado = _mapper.Map<Propina>(propina1);
                            await _propinaRepository.Adicionar(propinalunoMatriculado);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            if (meses == 11) { break; }
                            propina1.MesId = mesPropina.Id;
                            propina1.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                            propina1.PrecoPropina = 0;
                            propina1.Situacao = 5;

                            var propinalunoMatriculado = _mapper.Map<Propina>(propina1);
                            await _propinaRepository.Adicionar(propinalunoMatriculado);
                            await _context.SaveChangesAsync();
                        }
                }

            return true;
        }
        private async Task<bool> PagarPropinaAoMatricular(Guid alunoMatriculadoId, AlunoMatriculadoViewModel alunoMatriculado, int mes)
        {
            var alunoId = await ObterAlunoMatriculado(alunoMatriculadoId);
            var classeObitida = await _classeRepository.ObterClasse(alunoId.ClasseId);
            var preco = classeObitida.PrecoPropina;
            var classeDeExame = classeObitida.ClassDeExame;
            PagamentoPropinaViewModel PagamentoPropinas = new()
            {
                AlunoMatriculadoId = alunoId.Id, 
                ValorDesconto = alunoMatriculado.ValorDesconto,
                TotalPago = (preco * mes),
                TipoPagamento = alunoMatriculado.TipoPagamento, 
                Ativo = true,
                PagamentoMaticula = true,
                FuncionarioCaixaId = alunoId.FuncionarioCaixaId,
                PrecoPropina = preco,
                NumeroDeMeses = mes,
            };
             
            int primeiroMes = (mes / mes);
            int ultimoMes = mes;
            var nomeMesPrimeiro = await _mesRepository.ObterMesPeloCodigo(primeiroMes);
            var nomeMesUltimo = await _mesRepository.ObterMesPeloCodigo(ultimoMes);

            if (mes == 1)
            {
                PagamentoPropinas.Descricao = $"Pago o mês de {nomeMesPrimeiro.NomeMes}";
                var pagamentoUmMes = _mapper.Map<PagamentoPropina>(PagamentoPropinas);
                await _pagamentoPropinaService.Adicionar(pagamentoUmMes);
                if (!OperacaoValida())
                {
                    return false;
                }
            }
            else
            {
                PagamentoPropinas.Descricao = $"Pago os meses de {nomeMesPrimeiro.NomeMes} a {nomeMesUltimo.NomeMes}";

                await _pagamentoPropinaService.Adicionar(_mapper.Map<PagamentoPropina>(PagamentoPropinas));
                if (!OperacaoValida())
                {
                    TempData["Erro"] = $"Opa ):Já existe um aluno com este número de transação de pagamento infomado. !";
                    return false;
                }
            }

            _context.ChangeTracker.Clear();
            var pagamentoPropinaId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoMatriculado(alunoMatriculadoId);

            PropinaViewModel propina = new()
            {
                AlunoMatriculadoId = alunoId.Id,
                ClasseId = alunoId.ClasseId,
                TurmaId = alunoId.TurmaId,
                AnoLetivo = alunoId.AnoLetivo,
                PagamentoPropinaId = pagamentoPropinaId.Id

            };

            for (int meses = 1; meses <= 11; meses++)
            {
                var mesPropina = await _mesRepository.ObterMesPeloCodigo(meses);
                if(mes >= meses)
                {
                    if (classeDeExame == true)
                    {
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.MesId = mesPropina.Id;
                        propina.Situacao = 1;
                        propina.PrecoPropina = preco;
                        var propinaalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinaalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.MesId = mesPropina.Id;
                        propina.Situacao = 1;
                        propina.PrecoPropina = preco;
                        var propinaalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinaalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    if (classeDeExame == true)
                    {
                        propina.MesId = mesPropina.Id;
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.PrecoPropina = 0;
                        propina.Situacao = 4;

                        var propinalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (meses == 11) { break; }
                        propina.MesId = mesPropina.Id;
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.PrecoPropina = 0;
                        propina.Situacao = 4;

                        var propinalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }

                }
            }

            return true;
        }
        private async Task<bool> AlterarPagamentoPropinaAoMatricular(Guid alunoMatriculadoId, AlunoMatriculadoViewModel alunoMatriculado, int mes)
        {
            var alunoId = await ObterAlunoMatriculado(alunoMatriculadoId);
            var classeObitida = await _classeRepository.ObterClasse(alunoId.ClasseId);
            var preco = classeObitida.PrecoPropina;
            var classeDeExame = classeObitida.ClassDeExame;
            PagamentoPropinaViewModel PagamentoPropinas = new()
            {
                AlunoMatriculadoId = alunoId.Id,
                ValorDesconto = alunoMatriculado.ValorDesconto,
                TotalPago = (preco * mes),
                TipoPagamento = alunoMatriculado.TipoPagamento,
                Ativo = true,
                PagamentoMaticula = true,
                FuncionarioCaixaId = alunoId.FuncionarioCaixaId,
                PrecoPropina = preco,
                NumeroDeMeses = mes,
            };
            
            int primeiroMes = (mes / mes);
            int ultimoMes = mes;
            var nomeMesPrimeiro = await _mesRepository.ObterMesPeloCodigo(primeiroMes);
            var nomeMesUltimo = await _mesRepository.ObterMesPeloCodigo(ultimoMes);

            if (mes == 1)
            {
                PagamentoPropinas.Descricao = $"Pago o mês de {nomeMesPrimeiro.NomeMes}";
                var pagamentoUmMes = _mapper.Map<PagamentoPropina>(PagamentoPropinas);
                await _pagamentoPropinaService.Atualizar(pagamentoUmMes);
                if (!OperacaoValida())
                {
                    return false;
                }
            }
            else
            {
                PagamentoPropinas.Descricao = $"Pago os meses de {nomeMesPrimeiro.NomeMes} a {nomeMesUltimo.NomeMes}";
                await _pagamentoPropinaService.Atualizar(_mapper.Map<PagamentoPropina>(PagamentoPropinas));
                if (!OperacaoValida())
                {
                    TempData["Erro"] = $"Opa ):Já existe um aluno com este número de transação de pagamento infomado. !";
                    return false;
                }
            }

            _context.ChangeTracker.Clear();
            var pagamentoPropinaId = await _pagamentoPropinaRepository.ObterPagamentoPropinaPeloAlunoMatriculado(alunoMatriculadoId);

            PropinaViewModel propina = new()
            {
                AlunoMatriculadoId = alunoId.Id,
                ClasseId = alunoId.ClasseId,
                TurmaId = alunoId.TurmaId,
                AnoLetivo = alunoId.AnoLetivo,
                PagamentoPropinaId = pagamentoPropinaId.Id
            };

            for (int meses = 1; meses <= 11; meses++)
            {
                var mesPropina = await _mesRepository.ObterMesPeloCodigo(meses);

                if (mes >= meses)
                {
                    if (classeDeExame == true)
                    {
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.MesId = mesPropina.Id;
                        propina.Situacao = 1;
                        propina.PrecoPropina = preco;
                        var propinaalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinaalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.MesId = mesPropina.Id;
                        propina.Situacao = 1;
                        propina.PrecoPropina = preco;
                        var propinaalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinaalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }
                }
                else
                {
                    if (classeDeExame == true)
                    {
                        propina.MesId = mesPropina.Id;
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.PrecoPropina = 0;
                        propina.Situacao = 4;

                        var propinalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        if (meses == 11) { break; }
                        propina.MesId = mesPropina.Id;
                        propina.DescricaoPropina = $"Referente ao mês de {mesPropina.NomeMes}";
                        propina.PrecoPropina = 0;
                        propina.Situacao = 4;

                        var propinalunoMatriculado = _mapper.Map<Propina>(propina);
                        await _propinaRepository.Adicionar(propinalunoMatriculado);
                        await _context.SaveChangesAsync();
                    }

                }
            }

            return true;
        }
        private async Task<bool> ManipularNumeroDeVagasNaTurma(Guid id)
        {
            var turmaId = await _turmaRepository.ObterTurma(id);
            _context.ChangeTracker.Clear();
            turmaId.NumDeVagas++;
            await _turmaRepository.Atualizar(_mapper.Map<Turma>(turmaId));
            await _context.SaveChangesAsync();
            return true;
        }
        private async Task<bool> VoltarNumeroDeVagasNaTurma(Guid id)
        {
            var turmaId = await _turmaRepository.ObterTurma(id);
            _context.ChangeTracker.Clear();
            turmaId.NumDeVagas--;
            await _turmaRepository.Atualizar(_mapper.Map<Turma>(turmaId));
            await _context.SaveChangesAsync();
            return true;
        }
        private async Task<bool> AlterarNumeroDeVagasNaTurma(Guid id)
        {
            var turmaId = await _turmaRepository.ObterTurma(id);
            _context.ChangeTracker.Clear();
            turmaId.NumDeVagas--;
            await _turmaRepository.Atualizar(_mapper.Map<Turma>(turmaId));
            await _context.SaveChangesAsync();
            return true;
        }
        #endregion

        public IActionResult GetNumeroDeVagaNaTurma(Guid turmaId)
        {
            int Vagas = _context.Turmas.Single(model => model.Id == turmaId).NumDeVagas;
            string nome = _context.Turmas.Single(model => model.Id == turmaId).NomeTurma;
            Guid classeId = _context.Turmas.Single(model => model.Id == turmaId).ClasseId;
            string classe = _context.Classes.Single(model => model.Id == classeId).Nome;
            if (Vagas == 0)
            { numDeVagas = $"Não há vagas na turma {nome}";}
            else { numDeVagas = $"{classe} classe turma {nome}, há {Vagas} vagas"; }
            return Json(numDeVagas);
        }

        [AllowAnonymous]
        public IActionResult GetNumeroDeVagaNaTurmaComEncarregado(Guid turmaId)
        {
            var turmaGuid = Guid.Parse("00000000-0000-0000-0000-000000000000");
            if (turmaId == turmaGuid)
            {
                TempData["Erro"] = $"Opa ): Turma não existe!";
                return RedirectToAction("CreateEncarregado");
            }
            int Vagas = _context.Turmas.Single(model => model.Id == turmaId).NumDeVagas;
            string nome = _context.Turmas.Single(model => model.Id == turmaId).NomeTurma;
            Guid classeId = _context.Turmas.Single(model => model.Id == turmaId).ClasseId;
            string classe = _context.Classes.Single(model => model.Id == classeId).Nome;
            if (Vagas == 0)
            { numDeVagas = $"Não há vagas na turma {nome}"; }
            else { numDeVagas = $"{classe} classe turma {nome}, há {Vagas} vagas"; }
            return Json(numDeVagas);
        }
        public IActionResult GetIdade(Guid alunoInscritoId)
        {
            int Idades = _context.AlunoInscritos.Single(model => model.Id == alunoInscritoId).Idade;
            Guid classeId = _context.AlunoInscritos.Single(model => model.Id == alunoInscritoId).ClasseId;
            string classe = _context.Classes.Single(model => model.Id == classeId).Nome;

            string idadeAluno;
             idadeAluno = $"{Idades} anos de idade para {classe} classe";
            return Json(idadeAluno);
        }
        #endregion

    }
}

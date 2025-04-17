using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Context;
using DevJANGO.App.Extensions;
using Microsoft.AspNetCore.Authorization;
using DevJANGO.App.Queries;
using DevJANGO.Data.Repository;

namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class AlunoInscritosController : BaseController
    {
         #region DECLARAR AS DEPENDENCIA
        private readonly JangoDbContext _context;
        private readonly INiveisDeEnsinoRepository _niveisDeEnsinoRepository;
        private readonly IClasseRepository _classeRepository;
        private readonly ICursoRepository _cursoRepository;
        private readonly IEncarregadoRepository _encarregadoRepository;
        private readonly IGrauDeParentescoRepository _grauDeParentescoRepository;
        private readonly IAreaDeConhecimentoRepository _areaDeConhecimentoRepository;
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        private readonly IAlunoInscritoService _alunoInscritoService;
        private readonly IAlunoInscritoIniciacaoService _alunoInscritoIniciacaoService;
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;
        private readonly IAlunoInscritoQueries _alunoInscritoQueries;

        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public AlunoInscritosController(
              JangoDbContext context,
            INiveisDeEnsinoRepository niveisDeEnsinoRepository,
            IClasseRepository classeRepository,
            IEncarregadoRepository encarregadoRepository,
            IGrauDeParentescoRepository grauDeParentescoRepository,
            IAreaDeConhecimentoRepository areaDeConhecimentoRepository,
            IAlunoInscritoRepository alunoInscritoRepository,
            IAlunoMatriculadoRepository alunoMatriculadoRepository,
            IAlunoInscritoService alunoInscritoService,
            IFuncionarioCaixaRepository funcionarioCaixaRepository,
            IMapper mapper,
            INotificador notificador,
            IAlunoInscritoIniciacaoService alunoInscritoIniciacaoService
           , IAlunoInscritoQueries alunoInscritoQueries,
             ICursoRepository cursoRepository) : base(notificador)
        {
            _context = context;
            _niveisDeEnsinoRepository = niveisDeEnsinoRepository;
            _classeRepository = classeRepository;
            _encarregadoRepository = encarregadoRepository;
            _grauDeParentescoRepository = grauDeParentescoRepository;
            _areaDeConhecimentoRepository = areaDeConhecimentoRepository;
            _alunoInscritoRepository = alunoInscritoRepository;
            _alunoMatriculadoRepository = alunoMatriculadoRepository;
            _alunoInscritoService = alunoInscritoService;
            _alunoInscritoIniciacaoService = alunoInscritoIniciacaoService;
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
            _mapper = mapper;
            _alunoInscritoQueries = alunoInscritoQueries;
            _cursoRepository = cursoRepository;
        }
        #endregion

        #region MÉTODO PARA LISTAR
        [ClaimsAuthorize("AlunoInscritos", "DG")]
        [Route("estatistica-financeira-de-alunos-inscritos")]
        public async Task<IActionResult> EstatisticaAlunoInscritos()
        {
            //var alunos = await _alunoInscritoQueries.ObterLista();
            //return View(alunos);
            return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAluno()));
        }
        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-todos-alunos-inscritos")]
        public async Task<IActionResult> Index()
        {
            var alunos = await _alunoInscritoQueries.ObterLista();
            return View(alunos);
            //return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrau()));
        }

        #endregion

        #region MÉTODO PARA LISTAR ALUNOS DA INICIAÇÂO
        
        [ClaimsAuthorize("AlunoInscritos", "VI")]
      
        [Route("lista-de-alunos-inscritos-na-iniciacao")]
        public async Task<IActionResult> IndexIniciacao([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null, string returnUrl = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosIniciacao(ps, page, q);
            ViewBag.Pesquisa = q;
             alunos.ReferenceAction = "lista-de-alunos-inscritos-na-iniciacao";
            return View(alunos);
        }
     

        [AllowAnonymous]
        [Route("lista-liberada-para-inscricao-na-iniciacao")]
        public async Task<IActionResult> IndexEncarregadoIniciacao([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoIniciacao(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-inscricao-na-iniciacao";
            return View(alunos);
        }
        #endregion

        #region MÉTODO PARA LISTAR  ALUNOS DO ENSINO PRIMARIO

        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-no-ensino-primario")]
        public async Task<IActionResult> IndexEnsinoPrimario([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-no-ensino-primario";
            return View(alunos);
        }
        
        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-na-etapa-um")]
        //public async Task<IActionResult> IndexEtapaUm()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaUm()));
        //    //var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
        //    //ViewBag.Pesquisa = q;
        //    //alunos.ReferenceAction = "lista-de-alunos-inscritos-na-etapa-um";
        //}
        public async Task<IActionResult> IndexEtapaUm([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEtapaUm(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-na-etapa-um";

            return View(alunos);
        }
        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-na-etapa-dois")]
        //public async Task<IActionResult> IndexEtapaDois()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaDois()));
        //    //var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
        //    //ViewBag.Pesquisa = q;
        //    //alunos.ReferenceAction = "lista-de-alunos-inscritos-na-etapa-dois";
        //}
        public async Task<IActionResult> IndexEtapaDois([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEtapaDois(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-na-etapa-dois";

            return View(alunos);
        }

        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-na-etapa-tres")]
        //public async Task<IActionResult> IndexEtapaTres()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaTres()));
        //    //var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
        //    //ViewBag.Pesquisa = q;
        //    //alunos.ReferenceAction = "lista-de-alunos-inscritos-na-etapa-tres";
        //}
        public async Task<IActionResult> IndexEtapaTres([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEtapaTres(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-na-etapa-tres";

            return View(alunos);
        }
        [AllowAnonymous]
        [Route("lista-liberada-para-inscricao-no-ensino-primario")]
        public async Task<IActionResult> IndexEncarregadoEnsinoPrimario([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        //public async Task<IActionResult> IndexEncarregadoEnsinoPrimario()
        {
            //return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEnsinoPrimeiro()));
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoEnsinoPrimario(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-inscricao-no-ensino-primario";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-liberada-para-etapa-um")]
        //public async Task<IActionResult> IndexEncarregadoEtapaUm()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaUm()));
        //    //var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
        //    //ViewBag.Pesquisa = q;
        //    //alunos.ReferenceAction = "lista-liberada-para-etapa-um";
        //}
        public async Task<IActionResult> IndexEncarregadoEtapaUm([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoEtapaUm(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-etapa-um";
            return View(alunos);
        }
        [AllowAnonymous]
        [Route("lista-liberada-para-etapa-dois")]
        //public async Task<IActionResult> IndexEncarregadoEtapaDois()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaDois()));
        //    //var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
        //    //ViewBag.Pesquisa = q;
        //    //alunos.ReferenceAction = "lista-liberada-para-etapa-dois";
        //}
        public async Task<IActionResult> IndexEncarregadoEtapaDois([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoEtapaDois(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-etapa-dois";
            return View(alunos);
        }
       
        [AllowAnonymous]
        [Route("lista-liberada-para-etapa-tres")]
        //public async Task<IActionResult> IndexEncarregadoEtapaTres()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauEtapaTres()));
        //    //var alunos = await _alunoInscritoQueries.ObterTodosPrimario(ps, page, q);
        //    //ViewBag.Pesquisa = q;
        //    //alunos.ReferenceAction = "lista-liberada-para-etapa-tres";
        //}
        public async Task<IActionResult> IndexEncarregadoEtapaTres([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoEtapaTres(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-etapa-tres";
            return View(alunos);
        }
        #endregion

        #region MÉTODO PARA LISTAR  ALUNOS DO I CICLO

        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-no-primeiro-ciclo")]
        //public async Task<IActionResult> IndexICiclo()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauICiclo()));
        //}
        public async Task<IActionResult> IndexICiclo([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosICiclo(ps, page, q); 
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-no-primeiro-ciclo";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-liberada-para-inscricao-no-primeiro-ciclo")]
        //public async Task<IActionResult> IndexEncarregadoICiclo()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauICiclo()));
        //}
        public async Task<IActionResult> IndexEncarregadoICiclo([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoICiclo(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-inscricao-no-primeiro-ciclo";
            return View(alunos);
        }
        #endregion

        #region MÉTODO PARA LISTAR  ALUNOS DO II CICLO FB

        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-no-segundo-ciclo-fb")]
        //public async Task<IActionResult> IndexIICicloFisicasBiologica()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloFb()));
        //}
        
        public async Task<IActionResult> IndexIICicloFisicasBiologica([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosIICicloFisicasBiologica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-no-segundo-ciclo-fb";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-liberada-para-inscricao-no-segundo-ciclo-fb")]
        //public async Task<IActionResult> IndexEncarregadoIICicloFisicasBiologica()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloFb()));
        //}
        public async Task<IActionResult> IndexEncarregadoIICicloFisicasBiologica([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoIICicloFisicasBiologica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-inscricao-no-segundo-ciclo-fb";
            return View(alunos);
        }

        #endregion

        #region MÉTODO PARA LISTAR  ALUNOS DO II CICLO EJ

        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("lista-de-alunos-inscritos-no-segundo-ciclo-ej")]
        //public async Task<IActionResult> IndexIICicloEconomicaJuridica()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloEj()));
        //}
       
        public async Task<IActionResult> IndexIICicloEconomicaJuridica([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            
            var alunos = await _alunoInscritoQueries.ObterTodosIICicloEconomicaJuridica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-de-alunos-inscritos-no-segundo-ciclo-ej";
            return View(alunos);
        }

        [AllowAnonymous]
        [Route("lista-liberada-para-inscricao-no-segundo-ciclo-ej")]
        //public async Task<IActionResult> IndexEncarregadoIICicloEconomicaJuridica()
        //{
        //    return View(_mapper.Map<IEnumerable<AlunoInscritoViewModel>>(await _alunoInscritoRepository.ObterAlunoInscritoEncarregaosNiveisAreaGrauIICicloEj()));
        //}
        public async Task<IActionResult> IndexEncarregadoIICicloEconomicaJuridica([FromQuery] int ps = 10, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var alunos = await _alunoInscritoQueries.ObterTodosEncarregadoIICicloEconomicaJuridica(ps, page, q);
            ViewBag.Pesquisa = q;
            alunos.ReferenceAction = "lista-liberada-para-inscricao-no-segundo-ciclo-ej";
            return View(alunos);
        }
        #endregion

        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL
        [ClaimsAuthorize("AlunoInscritos", "VI")]
        [Route("dados-do-aluno-inscrito/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var aluno = await ObterAlunoInscrito(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        [AllowAnonymous]
        [Route("dados-do-aluno-inscrito-pelo-encarregado/{id:guid}")]
        public async Task<IActionResult> DetailsEncarregado(Guid id)
        {
            var aluno = await ObterAlunoInscrito(id);
            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);
        }

        #endregion

        #region NOVO CADASTRO PELO FUNCIONARIO

        #region MÉTODO PARA CADASTRAR PARA A INICIAÇÃO
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-iniciacao")]
        public async Task<IActionResult> Create()
        {
            var alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(new AlunoInscritoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-iniciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AlunoInscritoViewModel alunoInscritoInicicao)
        {
            alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(alunoInscritoInicicao);
           
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoId();
            var classe = await _classeRepository.ObterClasseId();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoInicicao)) return RedirectToAction("Create");
            #endregion

            alunoInscritoInicicao.FuncionarioCaixaId = emailFuncionario.Id; 
            alunoInscritoInicicao.AreaDeConhecimentoId = area.Id; 
            alunoInscritoInicicao.ClasseId = classe.Id;
            alunoInscritoInicicao.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoInicicao.Estado = true;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoInicicao.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoInicicao);
            }
            alunoInscritoInicicao.Imagem = imgPrefixo + alunoInscritoInicicao.ImagemUpload.FileName;
           
            DateTime datanascimento = alunoInscritoInicicao.Datanascimento;
            alunoInscritoInicicao.Idade = CalcularIdade(datanascimento); 

            if (!await ValidarIdadeIniciacao(classe.Nome, datanascimento)) return RedirectToAction("Create");
            alunoInscritoInicicao.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoInicicao.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoInicicao);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoInicicao);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito); 

            if (!OperacaoValida()) return View(alunoInscritoInicicao);

            return RedirectToAction("IndexIniciacao");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA ETAPAS
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-etapa-um")]
        public async Task<IActionResult> CreateEtapaUm()
        {
            var alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(new AlunoInscritoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaUm(AlunoInscritoViewModel alunoInscritoInicicao)
        {
            alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(alunoInscritoInicicao);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEtapaUm();
            var classe = await _classeRepository.ObterClasseIdEtapaUm();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoInicicao)) return RedirectToAction("CreateEtapaUm");
            #endregion

            alunoInscritoInicicao.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoInicicao.AreaDeConhecimentoId = area.Id;
            alunoInscritoInicicao.ClasseId = classe.Id;
            alunoInscritoInicicao.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoInicicao.Estado = true;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoInicicao.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoInicicao);
            }
            alunoInscritoInicicao.Imagem = imgPrefixo + alunoInscritoInicicao.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoInicicao.Datanascimento;
            alunoInscritoInicicao.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeEtapaUm(classe.Nome, datanascimento)) return RedirectToAction("CreateEtapaUm");
            alunoInscritoInicicao.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoInicicao.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoInicicao);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoInicicao);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoInicicao);

            return RedirectToAction("IndexEtapaUm");
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-etapa-dois")]
        public async Task<IActionResult> CreateEtapaDois()
        {
            var alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(new AlunoInscritoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaDois(AlunoInscritoViewModel alunoInscritoInicicao)
        {
            alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(alunoInscritoInicicao);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEtapaDois();
            var classe = await _classeRepository.ObterClasseIdEtapaDois();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoInicicao)) return RedirectToAction("CreateEtapaDois");
            #endregion

            alunoInscritoInicicao.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoInicicao.AreaDeConhecimentoId = area.Id;
            alunoInscritoInicicao.ClasseId = classe.Id;
            alunoInscritoInicicao.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoInicicao.Estado = true;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoInicicao.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoInicicao);
            }
            alunoInscritoInicicao.Imagem = imgPrefixo + alunoInscritoInicicao.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoInicicao.Datanascimento;
            alunoInscritoInicicao.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeEtapaDois(classe.Nome, datanascimento)) return RedirectToAction("CreateEtapaDois");
            alunoInscritoInicicao.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoInicicao.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoInicicao);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoInicicao);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoInicicao);

            return RedirectToAction("IndexEtapaDois");
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-etapa-tres")]
        public async Task<IActionResult> CreateEtapaTres()
        {
            var alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(new AlunoInscritoViewModel());
            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEtapaTres(AlunoInscritoViewModel alunoInscritoInicicao)
        {
            alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(alunoInscritoInicicao);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEtapaTres();
            var classe = await _classeRepository.ObterClasseIdEtapaTres();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaTres();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoInicicao)) return RedirectToAction("CreateEtapaTres");
            #endregion

            alunoInscritoInicicao.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoInicicao.AreaDeConhecimentoId = area.Id;
            alunoInscritoInicicao.ClasseId = classe.Id;
            alunoInscritoInicicao.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoInicicao.Estado = true;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoInicicao.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoInicicao);
            }
            alunoInscritoInicicao.Imagem = imgPrefixo + alunoInscritoInicicao.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoInicicao.Datanascimento;
            alunoInscritoInicicao.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeEtapaTres(classe.Nome, datanascimento)) return RedirectToAction("CreateEtapaTres");
            alunoInscritoInicicao.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoInicicao.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoInicicao);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoInicicao);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoInicicao);

            return RedirectToAction("IndexEtapaTres");
        }

        #endregion

        #region MÉTODO PARA CADASTRAR PARA ENSINO PRIMARIO
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-ensino-primario")]
        public async Task<IActionResult> CreateEnsinoPrimario()
        {
            var alunoInscritoViewModel = await PopularAlunoInscritoEnsinoPrimario(new AlunoInscritoViewModel());
            return View(alunoInscritoViewModel);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnsinoPrimario(AlunoInscritoViewModel alunoInscritoEnsinoPrimario)
        {
            alunoInscritoEnsinoPrimario = await PopularAlunoInscritoEnsinoPrimario(alunoInscritoEnsinoPrimario);
          
                   var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEnsinoPrimario();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();
            
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoEnsinoPrimario)) return RedirectToAction("CreateEnsinoPrimario");
            #endregion

            alunoInscritoEnsinoPrimario.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoEnsinoPrimario.AreaDeConhecimentoId = area.Id;
            alunoInscritoEnsinoPrimario.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoEnsinoPrimario.Estado = true;

            alunoInscritoEnsinoPrimario.AnoLetivo = CalcularIAnoLetivo();
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoEnsinoPrimario);
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoEnsinoPrimario.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoEnsinoPrimario);
            }
            alunoInscritoEnsinoPrimario.Imagem = imgPrefixo + alunoInscritoEnsinoPrimario.ImagemUpload.FileName;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscritoEnsinoPrimario.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoInscritoEnsinoPrimario.Idade = idade - 1; }
            else { alunoInscritoEnsinoPrimario.Idade = idade; }
            #endregion
            if (!await ValidarIdadeEnsinoPrimario(alunoInscritoEnsinoPrimario)) return RedirectToAction("CreateEnsinoPrimario");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoEnsinoPrimario);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoEnsinoPrimario);

            return RedirectToAction("IndexEnsinoPrimario");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA I CILO
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-o-primeiro-ciclo")]
        public async Task<IActionResult> CreateICiclo()
        {
            var alunoInscritoICiclo = await PopularAlunoInscritoICiclo(new AlunoInscritoViewModel());
            return View(alunoInscritoICiclo);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-o-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateICiclo(AlunoInscritoViewModel alunoInscritoICiclo)
        {
            alunoInscritoICiclo = await PopularAlunoInscritoICiclo(alunoInscritoICiclo);
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdICiclo();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoICiclo)) return RedirectToAction("CreateICiclo");
            #endregion

            alunoInscritoICiclo.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoICiclo.AreaDeConhecimentoId = area.Id;
            alunoInscritoICiclo.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoICiclo.Estado = true;
            alunoInscritoICiclo.AnoLetivo = CalcularIAnoLetivo();
           
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("TipoDocumento");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoICiclo);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoICiclo.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoICiclo);
            }
            alunoInscritoICiclo.Imagem = imgPrefixo + alunoInscritoICiclo.ImagemUpload.FileName;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscritoICiclo.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoInscritoICiclo.Idade = idade - 1; }
            else { alunoInscritoICiclo.Idade = idade; }
            #endregion

            if (!await ValidarIdadeICiclo(alunoInscritoICiclo)) return RedirectToAction("CreateICiclo");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoICiclo);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoICiclo);

            return RedirectToAction("IndexICiclo");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA II CILO FISICAS BIOLOGICAS
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-o-segundo-ciclo-fb")]
        public async Task<IActionResult> CreateIICicloFisicasBiologica()
        {
            var alunoIICicloFb = await PopularAlunoInscritoIICicloFisicasBiologica(new AlunoInscritoViewModel());
            return View(alunoIICicloFb);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-o-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloFisicasBiologica(AlunoInscritoViewModel alunoIICicloFb)
        {
            alunoIICicloFb = await PopularAlunoInscritoIICicloFisicasBiologica(alunoIICicloFb);
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdIICicloFB();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoIICicloFb)) return RedirectToAction("CreateIICicloFisicasBiologica");
            #endregion

            alunoIICicloFb.FuncionarioCaixaId = emailFuncionario.Id;
            alunoIICicloFb.AreaDeConhecimentoId = area.Id;
            alunoIICicloFb.NiveisDeEnsinoId = nivel.Id;
            alunoIICicloFb.Estado = true;
            alunoIICicloFb.AnoLetivo = CalcularIAnoLetivo();
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("TipoDocumento");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoIICicloFb);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoIICicloFb.ImagemUpload, imgPrefixo))
            {
                return View(alunoIICicloFb);
            }
            alunoIICicloFb.Imagem = imgPrefixo + alunoIICicloFb.ImagemUpload.FileName;

            #region CALCULAR IDADE

            DateTime datanascimento = alunoIICicloFb.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoIICicloFb.Idade = idade - 1; }
            else { alunoIICicloFb.Idade = idade; }
            #endregion
            if (!await ValidarIdadeIICiclo(alunoIICicloFb)) return RedirectToAction("CreateIICicloFisicasBiologica");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoIICicloFb);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoIICicloFb);

            return RedirectToAction("IndexIICicloFisicasBiologica");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA II CILO ECONOMICAS E JURIDICAS
        
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("nova-inscricao-para-o-segundo-ciclo-ej")]
        public async Task<IActionResult> CreateIICicloEconomicaJuridica()
        {
            var alunoIICicloEj = await PopularAlunoInscritoIICicloEconomicaJuridica(new AlunoInscritoViewModel());
            return View(alunoIICicloEj);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [Route("nova-inscricao-para-o-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIICicloEconomicaJuridica(AlunoInscritoViewModel alunoIICicloEj)
        {
            alunoIICicloEj = await PopularAlunoInscritoIICicloEconomicaJuridica(alunoIICicloEj);
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdIICicloEJ();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();

            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoIICicloEj)) return RedirectToAction("CreateIICicloEconomicaJuridica");
            #endregion

            alunoIICicloEj.FuncionarioCaixaId = emailFuncionario.Id;
            alunoIICicloEj.AreaDeConhecimentoId = area.Id;
            alunoIICicloEj.NiveisDeEnsinoId = nivel.Id;
            alunoIICicloEj.Estado = true;
            alunoIICicloEj.AnoLetivo = CalcularIAnoLetivo();
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("TipoDocumento");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoIICicloEj);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoIICicloEj.ImagemUpload, imgPrefixo))
            {
                return View(alunoIICicloEj);
            }
            alunoIICicloEj.Imagem = imgPrefixo + alunoIICicloEj.ImagemUpload.FileName;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoIICicloEj.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoIICicloEj.Idade = idade - 1; }
            else { alunoIICicloEj.Idade = idade; }
            #endregion
            if (!await ValidarIdadeIICiclo(alunoIICicloEj)) return RedirectToAction("CreateIICicloEconomicaJuridica");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoIICicloEj);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoIICicloEj);

            return RedirectToAction("IndexIICicloEconomicaJuridica");
        }

        #endregion

        #endregion

        #region NOVO CADASTRO ENCARREGADO

        #region MÉTODO PARA CADASTRAR PARA A INICIAÇÃO
        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-iniciacao")]
        public async Task<IActionResult> CreateEncarregado()
        {
            var alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(new AlunoInscritoViewModel());
            return View(alunoInscritoInicicao);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-iniciacao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregado(AlunoInscritoViewModel alunoInscritoInicicao)
        {
            alunoInscritoInicicao = await PopularAlunoInscritoIniciacao(alunoInscritoInicicao);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoId();
            var classe = await _classeRepository.ObterClasseId();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoId();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoInscritoInicicao.FuncionarioCaixaId = encarregado;
            alunoInscritoInicicao.AreaDeConhecimentoId = area.Id;
            alunoInscritoInicicao.ClasseId = classe.Id;
            alunoInscritoInicicao.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoInicicao.Estado = false;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoInicicao.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoInicicao);
            }
            alunoInscritoInicicao.Imagem = imgPrefixo + alunoInscritoInicicao.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoInicicao.Datanascimento;
            alunoInscritoInicicao.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeIniciacao(classe.Nome, datanascimento)) return RedirectToAction("CreateEncarregado");
            alunoInscritoInicicao.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoInicicao.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoInicicao);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoInicicao);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);
            //await _alunoInscritoRepository.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoInicicao);

            return RedirectToAction("IndexEncarregadoIniciacao");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA ENSINO PRIMARIO
        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-ensino-primario")]
        public async Task<IActionResult> CreateEncarregadoEnsinoPrimario()
        {
            var alunoInscritoViewModel = await PopularAlunoInscritoEnsinoPrimario(new AlunoInscritoViewModel());
            return View(alunoInscritoViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-ensino-primario")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEnsinoPrimario(AlunoInscritoViewModel alunoInscritoEnsinoPrimario)
        {
            alunoInscritoEnsinoPrimario = await PopularAlunoInscritoEnsinoPrimario(alunoInscritoEnsinoPrimario);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEnsinoPrimario();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEnsinoPrimario();

            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoInscritoEnsinoPrimario.FuncionarioCaixaId = encarregado;
            alunoInscritoEnsinoPrimario.AreaDeConhecimentoId = area.Id;
            alunoInscritoEnsinoPrimario.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoEnsinoPrimario.Estado = false;
            alunoInscritoEnsinoPrimario.AnoLetivo = CalcularIAnoLetivo();
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoEnsinoPrimario);
            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoEnsinoPrimario.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoEnsinoPrimario);
            }
            alunoInscritoEnsinoPrimario.Imagem = imgPrefixo + alunoInscritoEnsinoPrimario.ImagemUpload.FileName;
            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscritoEnsinoPrimario.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoInscritoEnsinoPrimario.Idade = idade - 1; }
            else { alunoInscritoEnsinoPrimario.Idade = idade; }
            #endregion
            if (!await ValidarIdadeEnsinoPrimario(alunoInscritoEnsinoPrimario)) return RedirectToAction("CreateEncarregadoEnsinoPrimario");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoEnsinoPrimario);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoEnsinoPrimario);

            return RedirectToAction("IndexEncarregadoEnsinoPrimario");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA ETAPAS 
        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-etapa-um")]
        public async Task<IActionResult> CreateEncarregadoEtapaUm()
        {
            var alunoInscritoViewModel = await PopularAlunoInscritoEtapaUm(new AlunoInscritoViewModel());
            return View(alunoInscritoViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-etapa-um")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEtapaUm(AlunoInscritoViewModel alunoInscritoEtapaUm)
        {
            alunoInscritoEtapaUm = await PopularAlunoInscritoEtapaUm(alunoInscritoEtapaUm);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEtapaUm();
            var classe = await _classeRepository.ObterClasseIdEtapaUm();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaUm();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoInscritoEtapaUm.FuncionarioCaixaId = encarregado;
            alunoInscritoEtapaUm.AreaDeConhecimentoId = area.Id;
            alunoInscritoEtapaUm.ClasseId = classe.Id;
            alunoInscritoEtapaUm.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoEtapaUm.Estado = false;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoEtapaUm.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoEtapaUm);
            }
            alunoInscritoEtapaUm.Imagem = imgPrefixo + alunoInscritoEtapaUm.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoEtapaUm.Datanascimento;
            alunoInscritoEtapaUm.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeIniciacao(classe.Nome, datanascimento)) return RedirectToAction("CreateEncarregadoEtapaUm");
            alunoInscritoEtapaUm.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoEtapaUm.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoEtapaUm);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoEtapaUm);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoEtapaUm);

            return RedirectToAction("IndexEncarregadoEtapaUm");
        }

        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-etapa-dois")]
        public async Task<IActionResult> CreateEncarregadoEtapaDois()
        {
            var alunoInscritoViewModel = await PopularAlunoInscritoEtapaDois(new AlunoInscritoViewModel());
            return View(alunoInscritoViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-etapa-dois")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEtapaDois(AlunoInscritoViewModel alunoInscritoEtapaDois)
        {
            alunoInscritoEtapaDois = await PopularAlunoInscritoEtapaDois(alunoInscritoEtapaDois);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEtapaDois();
            var classe = await _classeRepository.ObterClasseIdEtapaDois();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaDois();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoInscritoEtapaDois.FuncionarioCaixaId = encarregado;
            alunoInscritoEtapaDois.AreaDeConhecimentoId = area.Id;
            alunoInscritoEtapaDois.ClasseId = classe.Id;
            alunoInscritoEtapaDois.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoEtapaDois.Estado = false;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoEtapaDois.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoEtapaDois);
            }
            alunoInscritoEtapaDois.Imagem = imgPrefixo + alunoInscritoEtapaDois.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoEtapaDois.Datanascimento;
            alunoInscritoEtapaDois.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeIniciacao(classe.Nome, datanascimento)) return RedirectToAction("CreateEncarregadoEtapaDois");
            alunoInscritoEtapaDois.EscolaDeOrgigem = "Sem Escola";
            alunoInscritoEtapaDois.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            //ModelState.Remove("NumPautaDaEscolaOrigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoEtapaDois);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoEtapaDois);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoEtapaDois);

            return RedirectToAction("IndexEncarregadoEtapaDois");
        }

        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-etapa-tres")]
        public async Task<IActionResult> CreateEncarregadoEtapaTres()
        {
            var alunoInscritoViewModel = await PopularAlunoInscritoEtapaTres(new AlunoInscritoViewModel());
            return View(alunoInscritoViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-etapa-tres")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoEtapaTres(AlunoInscritoViewModel alunoInscritoEtapaTres)
        {
            alunoInscritoEtapaTres = await PopularAlunoInscritoEtapaTres(alunoInscritoEtapaTres);

            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdEtapaTres();
            var classe = await _classeRepository.ObterClasseIdEtapaTres();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdEtapaTres();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoInscritoEtapaTres.FuncionarioCaixaId = encarregado;
            alunoInscritoEtapaTres.AreaDeConhecimentoId = area.Id;
            alunoInscritoEtapaTres.ClasseId = classe.Id;
            alunoInscritoEtapaTres.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoEtapaTres.Estado = false;

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoEtapaTres.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoEtapaTres);
            }
            alunoInscritoEtapaTres.Imagem = imgPrefixo + alunoInscritoEtapaTres.ImagemUpload.FileName;

            DateTime datanascimento = alunoInscritoEtapaTres.Datanascimento;
            alunoInscritoEtapaTres.Idade = CalcularIdade(datanascimento);

            if (!await ValidarIdadeIniciacao(classe.Nome, datanascimento)) return RedirectToAction("CreateEncarregadoEtapaTres");
            alunoInscritoEtapaTres.EscolaDeOrgigem = "Sem Escola";
            //alunoInscritoEtapaTres.NumPautaDaEscolaOrigem = "JI" + "-" + "000";
            alunoInscritoEtapaTres.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("EscolaDeOrgigem");
            //ModelState.Remove("NumPautaDaEscolaOrigem");
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("ClasseId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoEtapaTres);

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoEtapaTres);
            await _alunoInscritoIniciacaoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoEtapaTres);

            return RedirectToAction("IndexEncarregadoEtapaTres");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA I CILO
        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-o-primeiro-ciclo")]
        public async Task<IActionResult> CreateEncarregadoICiclo()
        {
            var alunoInscritoICiclo = await PopularAlunoInscritoICiclo(new AlunoInscritoViewModel());
            return View(alunoInscritoICiclo);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-o-primeiro-ciclo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoICiclo(AlunoInscritoViewModel alunoInscritoICiclo)
        {
            alunoInscritoICiclo = await PopularAlunoInscritoICiclo(alunoInscritoICiclo);
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdICiclo();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdICiclo();
           
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoInscritoICiclo.FuncionarioCaixaId = encarregado;
            alunoInscritoICiclo.AreaDeConhecimentoId = area.Id;
            alunoInscritoICiclo.NiveisDeEnsinoId = nivel.Id;
            alunoInscritoICiclo.Estado = false;
            alunoInscritoICiclo.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("TipoDocumento");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoInscritoICiclo);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoInscritoICiclo.ImagemUpload, imgPrefixo))
            {
                return View(alunoInscritoICiclo);
            }
            alunoInscritoICiclo.Imagem = imgPrefixo + alunoInscritoICiclo.ImagemUpload.FileName;

            #region CALCULAR IDADE

            DateTime datanascimento = alunoInscritoICiclo.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoInscritoICiclo.Idade = idade - 1; }
            else { alunoInscritoICiclo.Idade = idade; }
            #endregion

            if (!await ValidarIdadeICiclo(alunoInscritoICiclo)) return RedirectToAction("CreateEncarregadoICiclo");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoInscritoICiclo);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoInscritoICiclo);

            return RedirectToAction("IndexEncarregadoICiclo");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA II CILO FISICAS BIOLOGICAS
        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-o-segundo-ciclo-fb")]
        public async Task<IActionResult> CreateEncarregadoIICicloFisicasBiologica()
        {
            var alunoIICicloFb = await PopularAlunoInscritoIICicloFisicasBiologica(new AlunoInscritoViewModel());
            return View(alunoIICicloFb);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-o-segundo-ciclo-fb")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoIICicloFisicasBiologica(AlunoInscritoViewModel alunoIICicloFb)
        {
            alunoIICicloFb = await PopularAlunoInscritoIICicloFisicasBiologica(alunoIICicloFb);
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdIICicloFB();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoIICicloFb.FuncionarioCaixaId = encarregado;
            alunoIICicloFb.AreaDeConhecimentoId = area.Id;
            alunoIICicloFb.NiveisDeEnsinoId = nivel.Id;
            alunoIICicloFb.Estado = false;
            alunoIICicloFb.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("TipoDocumento");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoIICicloFb);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoIICicloFb.ImagemUpload, imgPrefixo))
            {
                return View(alunoIICicloFb);
            }
            alunoIICicloFb.Imagem = imgPrefixo + alunoIICicloFb.ImagemUpload.FileName;

            #region CALCULAR IDADE

            DateTime datanascimento = alunoIICicloFb.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoIICicloFb.Idade = idade - 1; }
            else { alunoIICicloFb.Idade = idade; }
            #endregion
            if (!await ValidarIdadeIICiclo(alunoIICicloFb)) return RedirectToAction("CreateEncarregadoIICicloFisicasBiologica");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoIICicloFb);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoIICicloFb);

            return RedirectToAction("IndexEncarregadoIICicloFisicasBiologica");
        }
        #endregion

        #region MÉTODO PARA CADASTRAR PARA II CILO ECONOMICAS E JURIDICAS

        [AllowAnonymous]
        [Route("nova-inscricao-feita-pelo-encarregado-para-o-segundo-ciclo-ej")]
        public async Task<IActionResult> CreateEncarregadoIICicloEconomicaJuridica()
        {
            var alunoIICicloEj = await PopularAlunoInscritoIICicloEconomicaJuridica(new AlunoInscritoViewModel());
            return View(alunoIICicloEj);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("nova-inscricao-feita-pelo-encarregado-para-o-segundo-ciclo-ej")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregadoIICicloEconomicaJuridica(AlunoInscritoViewModel alunoIICicloEj)
        {
            alunoIICicloEj = await PopularAlunoInscritoIICicloEconomicaJuridica(alunoIICicloEj);
            var area = await _areaDeConhecimentoRepository.ObterAreaDeConhecimentoIdIICicloEJ();
            var nivel = await _niveisDeEnsinoRepository.ObterNiveisDeEnsinoIdIICiclo();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");

            alunoIICicloEj.FuncionarioCaixaId = encarregado;
            alunoIICicloEj.AreaDeConhecimentoId = area.Id;
            alunoIICicloEj.NiveisDeEnsinoId = nivel.Id;
            alunoIICicloEj.Estado = false;

            alunoIICicloEj.AnoLetivo = CalcularIAnoLetivo();

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("AreaDeConhecimentoId");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NiveisDeEnsinoId");
            ModelState.Remove("Estado");
            ModelState.Remove("TipoDocumento");
            ModelState.Remove("AnoLetivo");
            #endregion

            if (!ModelState.IsValid) return View(alunoIICicloEj);

            var imgPrefixo = Guid.NewGuid() + "_";
            if (!await UploadArquivo(alunoIICicloEj.ImagemUpload, imgPrefixo))
            {
                return View(alunoIICicloEj);
            }
            alunoIICicloEj.Imagem = imgPrefixo + alunoIICicloEj.ImagemUpload.FileName;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoIICicloEj.Datanascimento;
            DateTime agora = DateTime.Today;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoIICicloEj.Idade = idade - 1; }
            else { alunoIICicloEj.Idade = idade; }
            #endregion
            if (!await ValidarIdadeIICiclo(alunoIICicloEj)) return RedirectToAction("CreateEncarregadoIICicloEconomicaJuridica");

            var alunoInscrito = _mapper.Map<AlunoInscrito>(alunoIICicloEj);
            await _alunoInscritoService.Adicionar(alunoInscrito);

            if (!OperacaoValida()) return View(alunoIICicloEj);

            return RedirectToAction("IndexEncarregadoIICicloEconomicaJuridica");
        }

        #endregion

        #endregion

        #region MÉTODO PARA EDITAR

        #region INICIAÇÂO
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-iniciacao/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var alunoInscritoInicicao = await ObterAlunoInscrito(id);

            if (alunoInscritoInicicao == null)
            {
                return NotFound();
            }

            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("editar-inscricao-para-iniciacao/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, AlunoInscritoViewModel alunoInscritoInicicao)
        {
            if (id != alunoInscritoInicicao.Id) return NotFound();
            var classe = await _classeRepository.ObterClasseId();
            var alunoInscritoInicicaoAtualizacao = await ObterAlunoInscrito(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoInicicao)) return RedirectToAction("Edit");
            #endregion

            alunoInscritoInicicao.AreaDeConhecimento = alunoInscritoInicicaoAtualizacao.AreaDeConhecimento;
            alunoInscritoInicicao.Classe = alunoInscritoInicicaoAtualizacao.Classe;
            alunoInscritoInicicao.NiveisDeEnsino = alunoInscritoInicicaoAtualizacao.NiveisDeEnsino;
            alunoInscritoInicicao.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoInicicaoAtualizacao.Encarregado = alunoInscritoInicicao.Encarregado;
            alunoInscritoInicicaoAtualizacao.GrauDeParentesco = alunoInscritoInicicao.GrauDeParentesco;

            if (!ModelState.IsValid) return View(alunoInscritoInicicao);

            if (alunoInscritoInicicao.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoInicicao.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoInicicao);
                }

                alunoInscritoInicicaoAtualizacao.Imagem = imgPrefixo + alunoInscritoInicicao.ImagemUpload.FileName;
            }

            alunoInscritoInicicaoAtualizacao.Nome = alunoInscritoInicicao.Nome;
            DateTime datanascimento = alunoInscritoInicicao.Datanascimento;

            alunoInscritoInicicaoAtualizacao.EncarregadoId = alunoInscritoInicicao.EncarregadoId;
            alunoInscritoInicicaoAtualizacao.GrauDeParentescoId = alunoInscritoInicicao.GrauDeParentescoId;
            alunoInscritoInicicaoAtualizacao.Endereco = alunoInscritoInicicao.Endereco;
            alunoInscritoInicicaoAtualizacao.NomeDoPai = alunoInscritoInicicao.NomeDoPai;
            alunoInscritoInicicaoAtualizacao.NomeDaMae = alunoInscritoInicicao.NomeDaMae;
            alunoInscritoInicicaoAtualizacao.TipoDocumento = alunoInscritoInicicao.TipoDocumento;
            alunoInscritoInicicaoAtualizacao.Datanascimento = datanascimento;
            alunoInscritoInicicaoAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoInicicaoAtualizacao.NumDocumento = alunoInscritoInicicao.NumDocumento;
            alunoInscritoInicicaoAtualizacao.Sexo = alunoInscritoInicicao.Sexo;
            alunoInscritoInicicaoAtualizacao.ValorDaInscricao = alunoInscritoInicicao.ValorDaInscricao;
            alunoInscritoInicicaoAtualizacao.Estado = alunoInscritoInicicao.Estado;
           
            //if (!await ValidarIdadeIniciacao(alunoInscritoInicicao)) return RedirectToAction("Edit");
            if (!await ValidarIdadeIniciacao(classe.Nome, datanascimento)) return RedirectToAction("Create");
            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoInicicaoAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoInicicao);

            return RedirectToAction("IndexIniciacao");
        }

        #endregion

        #region Ensino Primario
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-ensino-primario/{id:guid}")]
        public async Task<IActionResult> EditEnsinoPrimario(Guid id)
        {
            var alunoInscritoInicicao = await ObterAlunoEnsinoPrimario(id);

            if (alunoInscritoInicicao == null)
            {
                return NotFound();
            }

            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("editar-inscricao-para-ensino-primario/{id:guid}")]
        public async Task<IActionResult> EditEnsinoPrimario(Guid id, AlunoInscritoViewModel alunoInscritoEnsinoPrimario)
        {
            if (id != alunoInscritoEnsinoPrimario.Id) return NotFound();

            var alunoInscritoEnsinoPrimarioAtualizacao = await ObterAlunoEnsinoPrimario(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoEnsinoPrimario)) return RedirectToAction("EditEnsinoPrimario");
            #endregion

            alunoInscritoEnsinoPrimario.AreaDeConhecimento = alunoInscritoEnsinoPrimarioAtualizacao.AreaDeConhecimento;
            alunoInscritoEnsinoPrimario.Classe = alunoInscritoEnsinoPrimarioAtualizacao.Classe;
            alunoInscritoEnsinoPrimario.NiveisDeEnsino = alunoInscritoEnsinoPrimarioAtualizacao.NiveisDeEnsino;
            alunoInscritoEnsinoPrimario.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoEnsinoPrimarioAtualizacao.Encarregado = alunoInscritoEnsinoPrimario.Encarregado;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentesco = alunoInscritoEnsinoPrimario.GrauDeParentesco;


            if (!ModelState.IsValid) return View(alunoInscritoEnsinoPrimario);

            if (alunoInscritoEnsinoPrimario.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoEnsinoPrimario.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoEnsinoPrimario);
                }

                alunoInscritoEnsinoPrimarioAtualizacao.Imagem = imgPrefixo + alunoInscritoEnsinoPrimario.ImagemUpload.FileName;
            }
            DateTime datanascimento = alunoInscritoEnsinoPrimario.Datanascimento;

            alunoInscritoEnsinoPrimarioAtualizacao.Nome = alunoInscritoEnsinoPrimario.Nome;
            alunoInscritoEnsinoPrimarioAtualizacao.EncarregadoId = alunoInscritoEnsinoPrimario.EncarregadoId;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentescoId = alunoInscritoEnsinoPrimario.GrauDeParentescoId;
            alunoInscritoEnsinoPrimarioAtualizacao.Endereco = alunoInscritoEnsinoPrimario.Endereco;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDoPai = alunoInscritoEnsinoPrimario.NomeDoPai;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDaMae = alunoInscritoEnsinoPrimario.NomeDaMae;
            alunoInscritoEnsinoPrimarioAtualizacao.TipoDocumento = alunoInscritoEnsinoPrimario.TipoDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Datanascimento = datanascimento;
            alunoInscritoEnsinoPrimarioAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoEnsinoPrimarioAtualizacao.NumDocumento = alunoInscritoEnsinoPrimario.NumDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Sexo = alunoInscritoEnsinoPrimario.Sexo;
            alunoInscritoEnsinoPrimarioAtualizacao.ValorDaInscricao = alunoInscritoEnsinoPrimario.ValorDaInscricao;
            alunoInscritoEnsinoPrimarioAtualizacao.Estado = alunoInscritoEnsinoPrimario.Estado;
            if (!await ValidarIdadeEnsinoPrimario(alunoInscritoEnsinoPrimarioAtualizacao)) return RedirectToAction("EditEnsinoPrimario");
            //(Guid classeId, string classeMatricula, DateTime datanascimento)
            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoEnsinoPrimarioAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoEnsinoPrimario);

            return RedirectToAction("IndexEnsinoPrimario");
        }

        #endregion

        #region I CICLO
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-o-primeiro-ciclo/{id:guid}")]
        public async Task<IActionResult> EditICiclo(Guid id)
        {
            var alunoIciclo = await ObterAlunoInscritoICiclo(id);

            if (alunoIciclo == null)
            {
                return NotFound();
            }

            return View(alunoIciclo);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("editar-inscricao-para-o-primeiro-ciclo/{id:guid}")]
        public async Task<IActionResult> EditICiclo(Guid id, AlunoInscritoViewModel alunoInscritoIciclo)
        {
            if (id != alunoInscritoIciclo.Id) return NotFound();

            var alunoInscritoIcicloAtualizacao = await ObterAlunoInscritoICiclo(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoIciclo)) return RedirectToAction("EditICiclo");
            #endregion

            alunoInscritoIciclo.AreaDeConhecimento = alunoInscritoIcicloAtualizacao.AreaDeConhecimento;
            alunoInscritoIciclo.Classe = alunoInscritoIcicloAtualizacao.Classe;
            alunoInscritoIciclo.NiveisDeEnsino = alunoInscritoIcicloAtualizacao.NiveisDeEnsino;
            alunoInscritoIciclo.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoIcicloAtualizacao.Encarregado = alunoInscritoIciclo.Encarregado;
            alunoInscritoIcicloAtualizacao.GrauDeParentesco = alunoInscritoIciclo.GrauDeParentesco;

            alunoInscritoIciclo.Imagem = alunoInscritoIcicloAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(alunoInscritoIciclo);

            if (alunoInscritoIciclo.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoIciclo.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoIciclo);
                }

                alunoInscritoIcicloAtualizacao.Imagem = imgPrefixo + alunoInscritoIciclo.ImagemUpload.FileName;
            }

            alunoInscritoIcicloAtualizacao.Nome = alunoInscritoIciclo.Nome;

            DateTime datanascimento = alunoInscritoIciclo.Datanascimento;

            alunoInscritoIcicloAtualizacao.EncarregadoId = alunoInscritoIciclo.EncarregadoId;
            alunoInscritoIcicloAtualizacao.GrauDeParentescoId = alunoInscritoIciclo.GrauDeParentescoId;
            alunoInscritoIcicloAtualizacao.Endereco = alunoInscritoIciclo.Endereco;
            alunoInscritoIcicloAtualizacao.NomeDoPai = alunoInscritoIciclo.NomeDoPai;
            alunoInscritoIcicloAtualizacao.NomeDaMae = alunoInscritoIciclo.NomeDaMae;
            alunoInscritoIcicloAtualizacao.TipoDocumento = alunoInscritoIciclo.TipoDocumento;
            alunoInscritoIcicloAtualizacao.Datanascimento = datanascimento;
            alunoInscritoIcicloAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoIcicloAtualizacao.NumDocumento = alunoInscritoIciclo.NumDocumento;
            alunoInscritoIcicloAtualizacao.Sexo = alunoInscritoIciclo.Sexo;
            alunoInscritoIcicloAtualizacao.ValorDaInscricao = alunoInscritoIciclo.ValorDaInscricao;
            alunoInscritoIcicloAtualizacao.Estado = alunoInscritoIciclo.Estado;
            if (!await ValidarIdadeICiclo(alunoInscritoIciclo)) return RedirectToAction("EditICiclo");

            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoIcicloAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoIciclo);

            return RedirectToAction("IndexICiclo");
        }

        #endregion

        #region II CICLO FB
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-o-segundo-ciclo-fb/{id:guid}")]
        public async Task<IActionResult> EditIICicloFisicasBiologica(Guid id)
        {
            var alunoIIcicloFb = await ObterAlunoInscritoIICicloFisicasBiologica(id);

            if (alunoIIcicloFb == null)
            {
                return NotFound();
            }

            return View(alunoIIcicloFb);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("editar-inscricao-para-o-segundo-ciclo-fb/{id:guid}")]
        public async Task<IActionResult> EditIICicloFisicasBiologica(Guid id, AlunoInscritoViewModel alunoInscritoIIcicloFb)
        {
            if (id != alunoInscritoIIcicloFb.Id) return NotFound();

            var alunoInscritoIIcicloFbAtualizacao = await ObterAlunoInscritoIICicloFisicasBiologica(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoIIcicloFb)) return RedirectToAction("EditIICicloFisicasBiologica");
            #endregion

            alunoInscritoIIcicloFb.AreaDeConhecimento = alunoInscritoIIcicloFbAtualizacao.AreaDeConhecimento;
            alunoInscritoIIcicloFb.Classe = alunoInscritoIIcicloFbAtualizacao.Classe;
            alunoInscritoIIcicloFb.NiveisDeEnsino = alunoInscritoIIcicloFbAtualizacao.NiveisDeEnsino;
            alunoInscritoIIcicloFb.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoIIcicloFbAtualizacao.Encarregado = alunoInscritoIIcicloFb.Encarregado;
            alunoInscritoIIcicloFbAtualizacao.GrauDeParentesco = alunoInscritoIIcicloFb.GrauDeParentesco;

            alunoInscritoIIcicloFb.Imagem = alunoInscritoIIcicloFbAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(alunoInscritoIIcicloFb);

            if (alunoInscritoIIcicloFb.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoIIcicloFb.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoIIcicloFb);
                }

                alunoInscritoIIcicloFbAtualizacao.Imagem = imgPrefixo + alunoInscritoIIcicloFb.ImagemUpload.FileName;
            }
            alunoInscritoIIcicloFbAtualizacao.Nome = alunoInscritoIIcicloFb.Nome;
           
            DateTime datanascimento = alunoInscritoIIcicloFb.Datanascimento;

            alunoInscritoIIcicloFbAtualizacao.EncarregadoId = alunoInscritoIIcicloFb.EncarregadoId;
            alunoInscritoIIcicloFbAtualizacao.GrauDeParentescoId = alunoInscritoIIcicloFb.GrauDeParentescoId;
            alunoInscritoIIcicloFbAtualizacao.Endereco = alunoInscritoIIcicloFb.Endereco;
            alunoInscritoIIcicloFbAtualizacao.NomeDoPai = alunoInscritoIIcicloFb.NomeDoPai;
            alunoInscritoIIcicloFbAtualizacao.NomeDaMae = alunoInscritoIIcicloFb.NomeDaMae;
            alunoInscritoIIcicloFbAtualizacao.TipoDocumento = alunoInscritoIIcicloFb.TipoDocumento;
            alunoInscritoIIcicloFbAtualizacao.Datanascimento = datanascimento;
            alunoInscritoIIcicloFbAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoIIcicloFbAtualizacao.NumDocumento = alunoInscritoIIcicloFb.NumDocumento;
            alunoInscritoIIcicloFbAtualizacao.Sexo = alunoInscritoIIcicloFb.Sexo;
            alunoInscritoIIcicloFbAtualizacao.ValorDaInscricao = alunoInscritoIIcicloFb.ValorDaInscricao;
            alunoInscritoIIcicloFbAtualizacao.Estado = alunoInscritoIIcicloFb.Estado;
            if (!await ValidarIdadeIICiclo(alunoInscritoIIcicloFb)) return RedirectToAction("EditIICicloFisicasBiologica");

            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoIIcicloFbAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoIIcicloFb);

            return RedirectToAction("IndexIICicloFisicasBiologica");
        }

        #endregion

        #region II CICLO EJ
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-o-segundo-ciclo-ej/{id:guid}")]
        public async Task<IActionResult> EditIICicloEconomicaJuridica(Guid id)
        {
            var alunoIIcicloEj = await ObterAlunoInscritoIICicloEconomicaJuridica(id);

            if (alunoIIcicloEj == null)
            {
                return NotFound();
            }

            return View(alunoIIcicloEj);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken] 
        [Route("editar-inscricao-para-o-segundo-ciclo-ej/{id:guid}")]
        public async Task<IActionResult> EditIICicloEconomicaJuridica(Guid id, AlunoInscritoViewModel alunoInscritoIIcicloEj)
        {
            if (id != alunoInscritoIIcicloEj.Id) return NotFound();

            var alunoInscritoIIcicloEjAtualizacao = await ObterAlunoInscritoIICicloEconomicaJuridica(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoIIcicloEj)) return RedirectToAction("EditIICicloEconomicaJuridica");
            #endregion

            alunoInscritoIIcicloEj.AreaDeConhecimento = alunoInscritoIIcicloEjAtualizacao.AreaDeConhecimento;
            alunoInscritoIIcicloEj.Classe = alunoInscritoIIcicloEjAtualizacao.Classe;
            alunoInscritoIIcicloEj.NiveisDeEnsino = alunoInscritoIIcicloEjAtualizacao.NiveisDeEnsino;
            alunoInscritoIIcicloEj.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoIIcicloEjAtualizacao.Encarregado = alunoInscritoIIcicloEj.Encarregado;
            alunoInscritoIIcicloEjAtualizacao.GrauDeParentesco = alunoInscritoIIcicloEj.GrauDeParentesco;

            alunoInscritoIIcicloEj.Imagem = alunoInscritoIIcicloEjAtualizacao.Imagem;

            if (!ModelState.IsValid) return View(alunoInscritoIIcicloEj);

            if (alunoInscritoIIcicloEj.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoIIcicloEj.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoIIcicloEj);
                }

                alunoInscritoIIcicloEjAtualizacao.Imagem = imgPrefixo + alunoInscritoIIcicloEj.ImagemUpload.FileName;
            }

            alunoInscritoIIcicloEjAtualizacao.Nome = alunoInscritoIIcicloEj.Nome;
           
            DateTime datanascimento = alunoInscritoIIcicloEj.Datanascimento;

            alunoInscritoIIcicloEjAtualizacao.EncarregadoId = alunoInscritoIIcicloEj.EncarregadoId;
            alunoInscritoIIcicloEjAtualizacao.GrauDeParentescoId = alunoInscritoIIcicloEj.GrauDeParentescoId;
            alunoInscritoIIcicloEjAtualizacao.Endereco = alunoInscritoIIcicloEj.Endereco;
            alunoInscritoIIcicloEjAtualizacao.NomeDoPai = alunoInscritoIIcicloEj.NomeDoPai;
            alunoInscritoIIcicloEjAtualizacao.NomeDaMae = alunoInscritoIIcicloEj.NomeDaMae;
            alunoInscritoIIcicloEjAtualizacao.TipoDocumento = alunoInscritoIIcicloEj.TipoDocumento;
            alunoInscritoIIcicloEjAtualizacao.Datanascimento = datanascimento;
            alunoInscritoIIcicloEjAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoIIcicloEjAtualizacao.NumDocumento = alunoInscritoIIcicloEj.NumDocumento;
            alunoInscritoIIcicloEjAtualizacao.Sexo = alunoInscritoIIcicloEj.Sexo;
            alunoInscritoIIcicloEjAtualizacao.ValorDaInscricao = alunoInscritoIIcicloEj.ValorDaInscricao;
            alunoInscritoIIcicloEjAtualizacao.Estado = alunoInscritoIIcicloEj.Estado;

            if (!await ValidarIdadeIICiclo(alunoInscritoIIcicloEj)) return RedirectToAction("EditIICicloEconomicaJuridica");

            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoIIcicloEjAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoIIcicloEj);

            return RedirectToAction("IndexIICicloEconomicaJuridica");
        }

        #endregion

        #region ETAPA UM
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-etapa-um/{id:guid}")]
        public async Task<IActionResult> EditEtapaUm(Guid id)
        {
            var alunoInscritoInicicao = await ObterAlunoEtapaUm(id);

            if (alunoInscritoInicicao == null)
            {
                return NotFound();
            }

            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-inscricao-para-etapa-um/{id:guid}")]
        public async Task<IActionResult> EditEtapaUm(Guid id, AlunoInscritoViewModel alunoInscritoEnsinoPrimario)
        {
            if (id != alunoInscritoEnsinoPrimario.Id) return NotFound();

            var alunoInscritoEnsinoPrimarioAtualizacao = await ObterAlunoEtapaUm(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoEnsinoPrimario)) return RedirectToAction("EditEtapaUm");
            #endregion

            alunoInscritoEnsinoPrimario.AreaDeConhecimento = alunoInscritoEnsinoPrimarioAtualizacao.AreaDeConhecimento;
            alunoInscritoEnsinoPrimario.Classe = alunoInscritoEnsinoPrimarioAtualizacao.Classe;
            alunoInscritoEnsinoPrimario.NiveisDeEnsino = alunoInscritoEnsinoPrimarioAtualizacao.NiveisDeEnsino;
            alunoInscritoEnsinoPrimario.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoEnsinoPrimarioAtualizacao.Encarregado = alunoInscritoEnsinoPrimario.Encarregado;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentesco = alunoInscritoEnsinoPrimario.GrauDeParentesco;


            if (!ModelState.IsValid) return View(alunoInscritoEnsinoPrimario);

            if (alunoInscritoEnsinoPrimario.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoEnsinoPrimario.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoEnsinoPrimario);
                }

                alunoInscritoEnsinoPrimarioAtualizacao.Imagem = imgPrefixo + alunoInscritoEnsinoPrimario.ImagemUpload.FileName;
            }
            DateTime datanascimento = alunoInscritoEnsinoPrimario.Datanascimento;

            alunoInscritoEnsinoPrimarioAtualizacao.Nome = alunoInscritoEnsinoPrimario.Nome;
            alunoInscritoEnsinoPrimarioAtualizacao.EncarregadoId = alunoInscritoEnsinoPrimario.EncarregadoId;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentescoId = alunoInscritoEnsinoPrimario.GrauDeParentescoId;
            alunoInscritoEnsinoPrimarioAtualizacao.Endereco = alunoInscritoEnsinoPrimario.Endereco;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDoPai = alunoInscritoEnsinoPrimario.NomeDoPai;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDaMae = alunoInscritoEnsinoPrimario.NomeDaMae;
            alunoInscritoEnsinoPrimarioAtualizacao.TipoDocumento = alunoInscritoEnsinoPrimario.TipoDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Datanascimento = datanascimento;
            alunoInscritoEnsinoPrimarioAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoEnsinoPrimarioAtualizacao.NumDocumento = alunoInscritoEnsinoPrimario.NumDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Sexo = alunoInscritoEnsinoPrimario.Sexo;
            alunoInscritoEnsinoPrimarioAtualizacao.ValorDaInscricao = alunoInscritoEnsinoPrimario.ValorDaInscricao;
            alunoInscritoEnsinoPrimarioAtualizacao.Estado = alunoInscritoEnsinoPrimario.Estado;
            if (!await ValidarIdadeEnsinoPrimario(alunoInscritoEnsinoPrimarioAtualizacao)) return RedirectToAction("EditEtapaUm");
            //(Guid classeId, string classeMatricula, DateTime datanascimento)
            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoEnsinoPrimarioAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoEnsinoPrimario);

            return RedirectToAction("IndexEtapaUm");
        }

        #endregion

        #region ETAPA DOIS
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-etapa-dois/{id:guid}")]
        public async Task<IActionResult> EditEtapaDois(Guid id)
        {
            var alunoInscritoInicicao = await ObterAlunoEtapaDois(id);

            if (alunoInscritoInicicao == null)
            {
                return NotFound();
            }

            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-inscricao-para-etapa-dois/{id:guid}")]
        public async Task<IActionResult> EditEtapaDois(Guid id, AlunoInscritoViewModel alunoInscritoEnsinoPrimario)
        {
            if (id != alunoInscritoEnsinoPrimario.Id) return NotFound();

            var alunoInscritoEnsinoPrimarioAtualizacao = await ObterAlunoEtapaDois(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoEnsinoPrimario)) return RedirectToAction("EditEtapaDois");
            #endregion

            alunoInscritoEnsinoPrimario.AreaDeConhecimento = alunoInscritoEnsinoPrimarioAtualizacao.AreaDeConhecimento;
            alunoInscritoEnsinoPrimario.Classe = alunoInscritoEnsinoPrimarioAtualizacao.Classe;
            alunoInscritoEnsinoPrimario.NiveisDeEnsino = alunoInscritoEnsinoPrimarioAtualizacao.NiveisDeEnsino;
            alunoInscritoEnsinoPrimario.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoEnsinoPrimarioAtualizacao.Encarregado = alunoInscritoEnsinoPrimario.Encarregado;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentesco = alunoInscritoEnsinoPrimario.GrauDeParentesco;


            if (!ModelState.IsValid) return View(alunoInscritoEnsinoPrimario);

            if (alunoInscritoEnsinoPrimario.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoEnsinoPrimario.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoEnsinoPrimario);
                }

                alunoInscritoEnsinoPrimarioAtualizacao.Imagem = imgPrefixo + alunoInscritoEnsinoPrimario.ImagemUpload.FileName;
            }
            DateTime datanascimento = alunoInscritoEnsinoPrimario.Datanascimento;

            alunoInscritoEnsinoPrimarioAtualizacao.Nome = alunoInscritoEnsinoPrimario.Nome;
            alunoInscritoEnsinoPrimarioAtualizacao.EncarregadoId = alunoInscritoEnsinoPrimario.EncarregadoId;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentescoId = alunoInscritoEnsinoPrimario.GrauDeParentescoId;
            alunoInscritoEnsinoPrimarioAtualizacao.Endereco = alunoInscritoEnsinoPrimario.Endereco;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDoPai = alunoInscritoEnsinoPrimario.NomeDoPai;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDaMae = alunoInscritoEnsinoPrimario.NomeDaMae;
            alunoInscritoEnsinoPrimarioAtualizacao.TipoDocumento = alunoInscritoEnsinoPrimario.TipoDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Datanascimento = datanascimento;
            alunoInscritoEnsinoPrimarioAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoEnsinoPrimarioAtualizacao.NumDocumento = alunoInscritoEnsinoPrimario.NumDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Sexo = alunoInscritoEnsinoPrimario.Sexo;
            alunoInscritoEnsinoPrimarioAtualizacao.ValorDaInscricao = alunoInscritoEnsinoPrimario.ValorDaInscricao;
            alunoInscritoEnsinoPrimarioAtualizacao.Estado = alunoInscritoEnsinoPrimario.Estado;
            if (!await ValidarIdadeEnsinoPrimario(alunoInscritoEnsinoPrimarioAtualizacao)) return RedirectToAction("EditEtapaDois");
            //(Guid classeId, string classeMatricula, DateTime datanascimento)
            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoEnsinoPrimarioAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoEnsinoPrimario);

            return RedirectToAction("IndexEtapaDois");
        }

        #endregion

        #region ETAPA TRES
        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [Route("editar-inscricao-para-etapa-tres/{id:guid}")]
        public async Task<IActionResult> EditEtapaTres(Guid id)
        {
            var alunoInscritoInicicao = await ObterAlunoEtapaTres(id);

            if (alunoInscritoInicicao == null)
            {
                return NotFound();
            }

            return View(alunoInscritoInicicao);
        }

        [ClaimsAuthorize("AlunoInscritos", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editar-inscricao-para-etapa-tres/{id:guid}")]
        public async Task<IActionResult> EditEtapaTres(Guid id, AlunoInscritoViewModel alunoInscritoEnsinoPrimario)
        {
            if (id != alunoInscritoEnsinoPrimario.Id) return NotFound();

            var alunoInscritoEnsinoPrimarioAtualizacao = await ObterAlunoEtapaTres(id);
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            if (!await ValidarDataFuncionario(alunoInscritoEnsinoPrimario)) return RedirectToAction("EditEtapaTres");
            #endregion

            alunoInscritoEnsinoPrimario.AreaDeConhecimento = alunoInscritoEnsinoPrimarioAtualizacao.AreaDeConhecimento;
            alunoInscritoEnsinoPrimario.Classe = alunoInscritoEnsinoPrimarioAtualizacao.Classe;
            alunoInscritoEnsinoPrimario.NiveisDeEnsino = alunoInscritoEnsinoPrimarioAtualizacao.NiveisDeEnsino;
            alunoInscritoEnsinoPrimario.FuncionarioCaixaId = emailFuncionario.Id;
            alunoInscritoEnsinoPrimarioAtualizacao.Encarregado = alunoInscritoEnsinoPrimario.Encarregado;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentesco = alunoInscritoEnsinoPrimario.GrauDeParentesco;


            if (!ModelState.IsValid) return View(alunoInscritoEnsinoPrimario);

            if (alunoInscritoEnsinoPrimario.ImagemUpload != null)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(alunoInscritoEnsinoPrimario.ImagemUpload, imgPrefixo))
                {
                    return View(alunoInscritoEnsinoPrimario);
                }

                alunoInscritoEnsinoPrimarioAtualizacao.Imagem = imgPrefixo + alunoInscritoEnsinoPrimario.ImagemUpload.FileName;
            }
            DateTime datanascimento = alunoInscritoEnsinoPrimario.Datanascimento;

            alunoInscritoEnsinoPrimarioAtualizacao.Nome = alunoInscritoEnsinoPrimario.Nome;
            alunoInscritoEnsinoPrimarioAtualizacao.EncarregadoId = alunoInscritoEnsinoPrimario.EncarregadoId;
            alunoInscritoEnsinoPrimarioAtualizacao.GrauDeParentescoId = alunoInscritoEnsinoPrimario.GrauDeParentescoId;
            alunoInscritoEnsinoPrimarioAtualizacao.Endereco = alunoInscritoEnsinoPrimario.Endereco;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDoPai = alunoInscritoEnsinoPrimario.NomeDoPai;
            alunoInscritoEnsinoPrimarioAtualizacao.NomeDaMae = alunoInscritoEnsinoPrimario.NomeDaMae;
            alunoInscritoEnsinoPrimarioAtualizacao.TipoDocumento = alunoInscritoEnsinoPrimario.TipoDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Datanascimento = datanascimento;
            alunoInscritoEnsinoPrimarioAtualizacao.Idade = CalcularIdade(datanascimento);
            alunoInscritoEnsinoPrimarioAtualizacao.NumDocumento = alunoInscritoEnsinoPrimario.NumDocumento;
            alunoInscritoEnsinoPrimarioAtualizacao.Sexo = alunoInscritoEnsinoPrimario.Sexo;
            alunoInscritoEnsinoPrimarioAtualizacao.ValorDaInscricao = alunoInscritoEnsinoPrimario.ValorDaInscricao;
            alunoInscritoEnsinoPrimarioAtualizacao.Estado = alunoInscritoEnsinoPrimario.Estado;
            if (!await ValidarIdadeEnsinoPrimario(alunoInscritoEnsinoPrimarioAtualizacao)) return RedirectToAction("EditEtapaTres");
            //(Guid classeId, string classeMatricula, DateTime datanascimento)
            await _alunoInscritoIniciacaoService.Atualizar(_mapper.Map<AlunoInscrito>(alunoInscritoEnsinoPrimarioAtualizacao));

            if (!OperacaoValida()) return View(alunoInscritoEnsinoPrimario);

            return RedirectToAction("IndexEtapaTres");
        }

        #endregion

        #endregion

        #region MÉTODO PARA EXCLUIR

        #region EXCLUIR INICIAÇÂO
        [ClaimsAuthorize("AlunoInscritos", "DG")]
        [Route("excluir-aluno-inscrito-para-iniciacao/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var aluno = await ObterAlunoInscrito(id);

            if (aluno == null)
            {
                return NotFound();
            }

            return View(aluno);

        }

        [ClaimsAuthorize("AlunoInscritos", "DG")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("excluir-aluno-inscrito-para-iniciacao/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var aluno = await ObterAlunoInscrito(id);
            var curso = await _cursoRepository.ObterCursoPrimaroICiclo();
            var alunoCurso = curso.Nome;
            var alunoNivel = aluno.NiveisDeEnsino;
            //await _alunoInscritoRepository.Remover(id);
       
            //if (aluno == null) return NotFound();
            await _alunoInscritoService.Remover(id);
            if (!OperacaoValida()) return View(aluno);
            //if (!OperacaoValida())
            //{
            //    TempData["Erro"] = "Este aluno já está matriculado.!";

            //    if (alunoCurso.Equals("Iniciação"))
            //    {
            //        return RedirectToAction("IndexIniciacao");
            //    }
            //}

            TempData["Sucesso"] = "Candidadto excluido com sucesso!";

            //if (alunoNivel.NomeNiveisDeEnsino.Equals("Iniciação"))
            //{
            //    return RedirectToAction("IndexIniciacao");
            //}
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Primário"))
            {
                return RedirectToAction("IndexEnsinoPrimario");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("I Ciclo"))
            {
                return RedirectToAction("IndexICiclo");
            }

            if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa I"))
            {
                return RedirectToAction("IndexEtapaUm");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa II"))
            {
                return RedirectToAction("IndexEtapaDois");
            }
            if (alunoNivel.NomeNiveisDeEnsino.Equals("Etapa III"))
            {
                return RedirectToAction("IndexEtapaTres");
            }

            if (alunoCurso.Equals("Ciências Fisicas e Biologicas"))
            {
                return RedirectToAction("IndexIICicloFisicasBiologica");
            }
            if (alunoCurso.Equals("Ciências Economicas e Jurídicas"))
            {
                return RedirectToAction("IndexIICicloEconomicaJuridica");
            }

            return RedirectToAction("IndexIniciacao");
        }

        #endregion

        //#region EXCLUIR INICIAÇÂO
        ////[ClaimsAuthorize("AlunoInscritos", "DG")]
        ////[Route("excluir-aluno-inscrito-para-iniciacao/{id:guid}")]
        ////public async Task<IActionResult> Delete(Guid id)
        ////{
        ////    var aluno = await ObterAlunoInscrito(id);

        ////    if (aluno == null)
        ////    {
        ////        return NotFound();
        ////    }

        ////    return View(aluno);

        ////}

        ////[ClaimsAuthorize("AlunoInscritos", "DG")]
        ////[HttpPost, ActionName("Delete")]
        ////[ValidateAntiForgeryToken]
        ////[Route("excluir-aluno-inscrito-para-iniciacao/{id:guid}")]
        ////public async Task<IActionResult> DeleteConfirmed(Guid id)
        ////{
        ////    var aluno = await ObterAlunoInscrito(id);

        ////    if (aluno == null) return NotFound();

        ////    await _alunoInscritoIniciacaoService.Remover(id);
        ////    if (!OperacaoValida()) return View(aluno);

        ////    TempData["Sucesso"] = "Candidadto excluido com sucesso!";
        ////    return RedirectToAction("IndexIniciacao");
        ////}

        //#endregion

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

        #region METODOS PRIVADOS DA CONTROLLER

        #region Popular
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoIniciacao(AlunoInscritoViewModel aluno)
        {
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoEnsinoPrimario(AlunoInscritoViewModel aluno)
        {
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoPrimario());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoEtapaUm(AlunoInscritoViewModel aluno)
        {
            //aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaUm());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoEtapaDois(AlunoInscritoViewModel aluno)
        {
            //aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaDois());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoEtapaTres(AlunoInscritoViewModel aluno)
        {
            //aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaTres());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoICiclo(AlunoInscritoViewModel aluno)
        {
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoICiclo());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoIICicloFisicasBiologica(AlunoInscritoViewModel aluno)
        {
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloFb());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        private async Task<AlunoInscritoViewModel> PopularAlunoInscritoIICicloEconomicaJuridica(AlunoInscritoViewModel aluno)
        {
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloEj());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodosEncarregados());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());

            return aluno;
        }
        #endregion

        #region Obter
        private async Task<AlunoInscritoViewModel> ObterAlunoInscrito(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoInscritoViewModel> ObterAlunoEnsinoPrimario(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoPrimario());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoInscritoViewModel> ObterAlunoInscritoICiclo(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoICiclo());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoInscritoViewModel> ObterAlunoInscritoIICicloEconomicaJuridica(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloEj());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoInscritoViewModel> ObterAlunoInscritoIICicloFisicasBiologica(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEnsinoIICicloFb());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }

        private async Task<AlunoInscritoViewModel> ObterAlunoEtapaUm(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaUm());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }

        private async Task<AlunoInscritoViewModel> ObterAlunoEtapaDois(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaDois());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        private async Task<AlunoInscritoViewModel> ObterAlunoEtapaTres(Guid id)
        {
            var aluno = _mapper.Map<AlunoInscritoViewModel>(await _alunoInscritoRepository.ObterAlunoInscrito(id));
            aluno.Classes = _mapper.Map<IEnumerable<ClasseViewModel>>(await _classeRepository.ObterClassesNiveisDeEtapaTres());
            aluno.Encarregados = _mapper.Map<IEnumerable<EncarregadoViewModel>>(await _encarregadoRepository.ObterTodos());
            aluno.GrauDeParentescos = _mapper.Map<IEnumerable<GrauDeParentescoViewModel>>(await _grauDeParentescoRepository.ObterTodos());
            return aluno;
        }
        #endregion

        #region Calcular
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
        /*
        private static string CalcularIAnoLetivo()
        {
            string anoLetivo;
            DateTime agora = DateTime.Today;
            //DateTime agora = new DateTime(2024, 08, 31);
            int ano = agora.Year;
            int mes = agora.Month;
            int dia = agora.Day;

            if (mes < 8 && dia <= 31)
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
        */


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

        #endregion

        #region Validar
        private async Task<bool> ValidarDataFuncionario(AlunoInscritoViewModel alunoInscrito)
        {
            var datanula = new DateTime(0001, 01, 01);
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscrito.Datanascimento;
            DateTime agora = DateTime.Today;

            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade)) { alunoInscrito.Idade = idade - 1; }
            else { alunoInscrito.Idade = idade; }
            #endregion
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
            if (alunoInscrito.Datanascimento == datanula)
            {
                TempData["Erro"] = "Opa! Data de Nascimento Inválida :(";
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarIdadeIniciacao(string classeMatricula, DateTime datanascimento)
        {
            var classe = await _classeRepository.ObterClasseId();
            #region CALCULAR IDADE
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
            #endregion

            if ((idadeAluno > 5 || idadeAluno < 4) && classe.Nome == classeMatricula)
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 5 anos a completar até Dezembro deste ano :(";
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
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter no mínimo 8 anos a completar até Dezembro deste ano :(";
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
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter no mínimo 10 anos a completar até Dezembro deste ano :(";
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
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter no mínimo 12 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeEnsinoPrimario(AlunoInscritoViewModel alunoInscrito)
        {
            alunoInscrito = await PopularAlunoInscritoEnsinoPrimario(alunoInscrito);
            var classeId = alunoInscrito.ClasseId;
            var classeObitida = await _classeRepository.ObterClasse(classeId);
            var classe = classeObitida.Nome;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscrito.Datanascimento;
            DateTime agora = DateTime.Today;
            int idadeAluno;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade))
            {
                idadeAluno = idade - 1;
            }
            else
            {
                idadeAluno = idade;
            }
            int t = idadeAluno; 
            #endregion


            //if (idade < 7 && classe == "1ª")
            if ((idadeAluno > 8 || idadeAluno < 6) && classe == "1ª") //6, 7 e 8 
            //if ((idade > 7 || idade < 6) && classe == "1ª")
            {
                TempData["Erro"] = $"O aluno tem {idadeAluno} anos de idade! Deve ter entre 6 á 7 anos de idade :(";
                return false;
            }
        
            //if (idade < 8 && classe == "2ª")
            if ((idadeAluno > 9 || idadeAluno < 7) && classe == "2ª") //7, 8 e 9 
            //if ((idade > 8 || idade < 7) && classe == "2ª")
            {
                TempData["Erro"] = $"O aluno tem {idadeAluno} anos de idade! Deve ter entre 7 á 8 anos de idade :(";
                return false;
            }

            //if (idade < 9 && classe == "3ª")
            if ((idadeAluno > 10 || idadeAluno < 8) && classe == "3ª") //8, 9 e 10 
            //if ((idade > 9 || idade < 8) && classe == "3ª")
            {
                TempData["Erro"] = $"O aluno tem {idadeAluno} anos de idade! Deve ter entre 8 á 9 anos de idade :(";
                return false;
            }

          
            if ((idadeAluno > 11 || idadeAluno < 8) && classe == "4ª") //9, 10 e 11 
            //if ((idade > 11 || idade < 8) && classe == "4ª") //9, 10 e 11 
            {
                TempData["Erro"] = $"O aluno tem {idadeAluno} anos de idade! Deve ter entre 9 á 10 anos de idade :(";
                return false;
            }

            if ((idadeAluno > 12 || idadeAluno < 10) && classe == "5ª") //10, 11 e 12 
            //if ((idade > 11 || idade < 10) && classe == "5ª")
            {
                TempData["Erro"] = $"O aluno tem {idadeAluno} anos de idade! Deve ter entre 10 á 11 anos de idade :(";
                return false;
            }

            if ((idadeAluno > 13 || idadeAluno < 11) && classe == "6ª") //11, 12 e 13 
                //if ((idade > 12 || idade < 11) && classe == "6ª")
            {
                TempData["Erro"] = $"O aluno tem {idadeAluno} anos de idade! Deve ter entre 11 á 12 anos de idade :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeICiclo(AlunoInscritoViewModel alunoInscrito)
        {
            alunoInscrito = await PopularAlunoInscritoICiclo(alunoInscrito);
            var classeId = alunoInscrito.ClasseId;
            var classeObitida = await _classeRepository.ObterClasse(classeId);
            var classe = classeObitida.Nome;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscrito.Datanascimento;
            DateTime agora = DateTime.Today;
            int idadeAluno;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade))
            {
                idadeAluno = idade - 1;
            }
            else
            {
                idadeAluno = idade;
            }
            #endregion

            if (idade < 12 && classe == "7ª")
            //if ((idadeAluno > 13 || idadeAluno < 11) && classe == "7ª") //11, 12 e 13 
            //if ((idadeAluno > 12 || idadeAluno < 11) && classe == "7ª")
            {
            TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 12 anos a completar até Dezembro deste ano :(";
            return false;
            }

            if (idade < 13 && classe == "8ª")
            //if ((idadeAluno > 13 || idadeAluno < 12) && classe == "8ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 13 anos a completar até Dezembro deste ano :(";
                return false;
            }

            if (idade < 14 && classe == "9ª")
            //if ((idadeAluno > 14 || idadeAluno < 13) && classe == "9ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 14 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        private async Task<bool> ValidarIdadeIICiclo(AlunoInscritoViewModel alunoInscrito)
        {
            alunoInscrito = await PopularAlunoInscritoIICicloFisicasBiologica(alunoInscrito);
            var classeId = alunoInscrito.ClasseId;
            var classeObitida = await _classeRepository.ObterClasse(classeId);
            var classe = classeObitida.Nome;

            #region CALCULAR IDADE
            DateTime datanascimento = alunoInscrito.Datanascimento;
            DateTime agora = DateTime.Today;
            int idadeAluno;
            int idade = agora.Year - datanascimento.Year;
            if (datanascimento > agora.AddYears(-idade))
            {
                idadeAluno = idade - 1;
            }
            else
            {
                idadeAluno = idade;
            }
            #endregion
            if (idadeAluno < 14 && classe == "10ª")
                //if ((idadeAluno > 15 || idadeAluno < 14) && classe == "10ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 15 anos a completar até Dezembro deste ano :(";
                return false;
            }
            if (idadeAluno < 15 && classe == "11ª")
            //if ((idadeAluno > 16 || idadeAluno < 15) && classe == "11ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 16 anos a completar até Dezembro deste ano :(";
                return false;
            }
            if (idadeAluno < 16 && classe == "12ª")
               // if ((idadeAluno > 17 || idadeAluno < 16) && classe == "12ª")
            {
                TempData["Erro"] = $"O aluno tem {idade} anos de idade! Deve ter 17 anos a completar até Dezembro deste ano :(";
                return false;
            }

            return true;
        }
        #endregion

        #region UploadImagem
        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            /*
              using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }
             */
            using var stream = new FileStream(path, FileMode.Create);
            await arquivo.CopyToAsync(stream);

            return true;
        }
        #endregion

        #endregion
    }
}

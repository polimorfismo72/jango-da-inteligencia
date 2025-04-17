using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using DevJANGO.Business.Models;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using DevJANGO.Data.Repository;
using DevJANGO.Business.Services;
using DevJANGO.App.Extensions;
using DevJANGO.App.Queries;

namespace DevJANGO.App.Controllers
{
    public class PagamentoPropinasController : BaseController
    {
        #region INJEÇÃO DE DEPENDENCIA
       
        #region DECLARAR AS DEPENDENCIA
        private readonly JangoDbContext _context;
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        private readonly IPagamentoPropinaRepository _pagamentoPropinaRepository;
        private readonly IPagamentoPropinaService _pagamentoPropinaService;
        private readonly IPropinaRepository _propinaRepository;
        private readonly IMesRepository _mesRepository;
        private readonly IClasseRepository _classeRepository;
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;
        private readonly IPagamentoQueries _pagamentoQueries;

        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PagamentoPropinasController(
              JangoDbContext context,
            IAlunoMatriculadoRepository alunoMatriculadoRepository,
            IPagamentoPropinaRepository pagamentoPropinaRepository,
            IPagamentoPropinaService pagamentoPropinaService,
            IPropinaRepository propinaRepository,
            IMesRepository mesRepository,
            IClasseRepository classeRepository,
            IFuncionarioCaixaRepository funcionarioCaixaRepository,
            IPagamentoQueries pagamentoQueries,
            IMapper mapper,
            INotificador notificador) : base(notificador)
        {
            _context = context;
            _alunoMatriculadoRepository = alunoMatriculadoRepository;
            _pagamentoPropinaRepository = pagamentoPropinaRepository;
            _pagamentoPropinaService = pagamentoPropinaService;
            _propinaRepository = propinaRepository;
            _mesRepository = mesRepository;
            _classeRepository = classeRepository;
            _pagamentoQueries = pagamentoQueries;
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
            _mapper = mapper;
        }
        #endregion

        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("PagamentoPropinas", "VI")]
        [Route("lista-de-aluno-para-o-pagamento")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterPagamentoPropinasAlunoMatriculados()));
        }

        [ClaimsAuthorize("PagamentoPropinas", "VI")]
        [Route("lista-de-pagamento-finalizado")]
        public async Task<IActionResult> IndexFinalizado()
        {
            return View(_mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterPagamentoPropinasAlunoMatriculados()));
        }
        [AllowAnonymous]
        [Route("lista-do-educando-para-o-pagamento")]
        public async Task<IActionResult> IndexEncarregado()
        {
            return View(_mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterPagamentoPropinasAlunoMatriculados()));
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        [ClaimsAuthorize("PagamentoPropinas", "AD")]
        [Route("seleciona-o-aluno-para-o-pagamento")]
        public IActionResult Create()
        {
           return View();
        }

        [HttpPost]
        [ClaimsAuthorize("PagamentoPropinas", "AD")]
        [Route("seleciona-o-aluno-para-o-pagamento")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PagamentoPropinaViewModel pagamento)
        {
            
            if (pagamento.AlunoMatriculado.Id == Guid.Empty)
            //if (pagamento.AlunoMatriculado.NumDocumento == null)
            {
               TempData["Erro"] = $"Deve inserir o número do BI/Cédula!";
               return RedirectToAction("Create");
            }
            var alunoId = pagamento.AlunoMatriculado.Id;
            var alunoInscrito = await _alunoMatriculadoRepository.ObterAlunoMatriculado(alunoId);
            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Matrícula pendente): Este aluno não pode fazer pagamentos!";
                return RedirectToAction("Create");
            }
            #region PEGAR O USUARIO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion
            if (!await ValidarDataFuncionario()) return RedirectToAction("Create");
            var contador = _context.Propinas.Where(c => ((int)c.Situacao) == 4)
             .Select(c => c.PagamentoPropinaId).Count();
            if (contador == 0) 
            {
                TempData["Erro"] = $"Opa ): Este aluno não tem dívida!";
                return RedirectToAction("Create");
            }
            PagamentoPropinaViewModel Pagar = new()
            {
                AlunoMatriculadoId = pagamento.AlunoMatriculado.Id,
                ValorDesconto = 0,
                TotalPago = 0,
                TipoPagamento = 1,
                Ativo = false,
                PagamentoMaticula = false,
                Descricao = "Nenhum mes",
                FuncionarioCaixaId = emailFuncionario.Id,
                NumeroDeMeses = 0,
                PrecoPropina = 0,
            };

            //if (Pagar.NumDocumento == null)
            //if ((Pagar.AlunoMatriculadoId).Equals(null))
            //if (pagamento.AlunoMatriculado.Id == Guid.Empty) return NotFound();
       
            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            ModelState.Remove("ValorDesconto");
            ModelState.Remove("TotalPago");
            ModelState.Remove("Descricao");
            ModelState.Remove("TipoPagamento");
            ModelState.Remove("Ativo");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NumeroDeMeses");
            ModelState.Remove("PrecoPropina");

            ModelState.Remove("AlunoMatriculado.Id");
            ModelState.Remove("AlunoMatriculado.Nome");
            ModelState.Remove("AlunoMatriculado.Descricao");
            ModelState.Remove("AlunoMatriculado.NumDocumento"); 
            #endregion

            if (!ModelState.IsValid) return View(pagamento);

            //await _pagamentoPropinaRepository.Adicionar(_mapper.Map<PagamentoPropina>(Pagar));
            await _pagamentoPropinaService.Adicionar(_mapper.Map<PagamentoPropina>(Pagar));
            if (!OperacaoValida()) return View(pagamento);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> BuscarAluno(AlunoMatriculadoViewModel aluno)
        {
            if (!await ValidarDocumentoMatriculado(aluno)) { return RedirectToAction("Create"); }
            var alunoMatriculado = await _alunoMatriculadoRepository.ObterDocumentoDoAluno(aluno.NumDocumento);
            var classe = await _classeRepository.ObterClassePorId(alunoMatriculado.ClasseId);
            TempData["Id"] = alunoMatriculado.Id;
            TempData["Nome"] = alunoMatriculado.Nome;
            //TempData["Idade"] = alunoMatriculado.Idade;
            TempData["Classe"] = classe.Nome;

            return RedirectToAction("Create");
        }
        #endregion

        #region MÉTODO PARA EFETUAR PAGAMENTO
        [ClaimsAuthorize("PagamentoPropinas", "AD")]
        [Route("efetuar-pagamento-de-propina/{id:guid}")]
        public async Task<IActionResult> EfetuarPagamento(Guid id)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaMeses = (from p in _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse())
                              //select new SelectListItem() { Text = p.Mes.NomeMes, Value = p.MesId.ToString() }).ToList();
            select new SelectListItem() { Text = p.DescricaoPropina, Value = p.Id.ToString() }).ToList();
            listaMeses.Insert(0, new SelectListItem() { Text = "-- Selecione a referência da mensalidade --", Value = string.Empty });

            ViewBag.Mes = listaMeses;

            #endregion

            var pagamento = await ObterPagamentoPropinaMesAlunoMatriculadoClasse(id);
            ViewBag.Id = pagamento.Id;
            //await _propinaRepository.ObterPropinasPorPagamento(id);
            //await ObterPropinaItems(id);
            ViewBag.Nome = pagamento.AlunoMatriculado.Nome;
            if (pagamento == null)
            {
                return NotFound();
            }
            return View(pagamento);
        }

        [HttpPost]
        [ClaimsAuthorize("PagamentoPropinas", "AD")]
        [ValidateAntiForgeryToken]
        [Route("efetuar-pagamento-de-propina/{id:guid}")]
        public async Task<IActionResult> EfetuarPagamento(Guid id, PagamentoPropinaViewModel pagamentoPropinaViewModel)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaMeses = (from p in _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse())
                              select new SelectListItem() { Text = p.DescricaoPropina, Value = p.Id.ToString() }).ToList();
            listaMeses.Insert(0, new SelectListItem() { Text = "-- Selecione a referência da mensalidade --", Value = string.Empty });

            ViewBag.Mes = listaMeses;

            #endregion

            if (id != pagamentoPropinaViewModel.Id) return NotFound();
            if (pagamentoPropinaViewModel.Descricao == "Nenhum mes")
            {
                TempData["Erro"] = "Opa ): Escreve a descrição do(s) mês(ou meses) !";
                return RedirectToAction("EfetuarPagamento");
            }
            #region VARIAVEIS PARA BUSCA DE VALORES
            var propinaId = pagamentoPropinaViewModel.Propina.Id;
            var propina = await _propinaRepository.ObterPropinaAlunoMatriculado(propinaId);
            var preco = await ObterClassePeloPreco(propina.ClasseId);
            var pagamentoPropina = await _pagamentoPropinaRepository.ObterPagamentoPropina(id);
            #endregion

            #region PEGAR O USUARIO LOGADO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            Guid guidFuncionario = emailFuncionario.Id;
            var estadoFuncionario = emailFuncionario.Ativo;
            var email = emailFuncionario.Email;

            if (email != nomeUsuarioLogado || estadoFuncionario == false)
            {
                TempData["Erro"] = "Opa! Este Funcionário não Existe, deve solicitar ao Administrador :(";
                return RedirectToAction("EfetuarPagamento");
            }
            #endregion

            PropinaViewModel AtualizarPropina = new()
            {
                AlunoMatriculadoId = propina.AlunoMatriculadoId,
                MesId = propina.MesId,
                TurmaId = propina.TurmaId,
                ClasseId = propina.ClasseId,
                DescricaoPropina = propina.DescricaoPropina,
                PrecoPropina = preco.PrecoPropina,
                AnoLetivo = propina.AnoLetivo,
                Situacao = 1,
                PagamentoPropinaId = pagamentoPropinaViewModel.Id
            };
            await _propinaRepository.Remover(propina.Id);
            await _propinaRepository.Adicionar(_mapper.Map<Propina>(AtualizarPropina));
            

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Propina.AnoLetivo");
            ModelState.Remove("Propina.DescricaoPropina");
            ModelState.Remove("NumDocumento");

            #endregion

            if (!ModelState.IsValid) return View(pagamentoPropinaViewModel);

            #region ACTUALIZAR pagamentoPropina
            var propinaPago = "Propina de ";
            if (pagamentoPropina.Descricao.Contains(propinaPago))
            {
                PagamentoPropinaViewModel PagaPropina = new()
                {
                    Id = pagamentoPropinaViewModel.Id,
                    Codigo = pagamentoPropina.Codigo,
                    AlunoMatriculadoId = pagamentoPropinaViewModel.AlunoMatriculadoId,
                    FuncionarioCaixaId = guidFuncionario,
                    ValorDesconto = pagamentoPropinaViewModel.ValorDesconto,
                    Descricao = pagamentoPropinaViewModel.Descricao,
                    NumeroDeMeses = ((int)((pagamentoPropina.TotalPago + preco.PrecoPropina) / preco.PrecoPropina)),
                    PrecoPropina = preco.PrecoPropina,
                    TotalPago = pagamentoPropina.TotalPago + preco.PrecoPropina - pagamentoPropina.ValorDesconto,
                    TipoPagamento = pagamentoPropinaViewModel.TipoPagamento,
                    Ativo = false,
                    PagamentoMaticula = false,
                };
                await _pagamentoPropinaRepository.Atualizar(_mapper.Map<PagamentoPropina>(PagaPropina));
            }
            else
            {
                PagamentoPropinaViewModel PagamentoPropina = new()
                {
                    Id = pagamentoPropinaViewModel.Id,
                    Codigo = pagamentoPropina.Codigo,
                    AlunoMatriculadoId = pagamentoPropinaViewModel.AlunoMatriculadoId,
                    FuncionarioCaixaId = guidFuncionario,
                    ValorDesconto = pagamentoPropinaViewModel.ValorDesconto,
                    Descricao = propinaPago + pagamentoPropinaViewModel.Descricao,
                    NumeroDeMeses = ((int)((pagamentoPropina.TotalPago + preco.PrecoPropina) / preco.PrecoPropina)),
                    PrecoPropina = preco.PrecoPropina,
                    TotalPago = pagamentoPropina.TotalPago + preco.PrecoPropina - pagamentoPropina.ValorDesconto,
                    TipoPagamento = pagamentoPropinaViewModel.TipoPagamento,
                    Ativo = false,
                    PagamentoMaticula = false,
                };
                await _pagamentoPropinaRepository.Atualizar(_mapper.Map<PagamentoPropina>(PagamentoPropina));
            }

            #endregion

            var propinaAluno = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinasPorPagamento(pagamentoPropinaViewModel.Id));
            return RedirectToAction("EfetuarPagamento",propinaAluno);
        }

       /* private async Task<bool> PagarPropinaAoMatricular(Guid alunoMatriculadoId, AlunoMatriculadoViewModel alunoMatriculado, int mes)
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
                PagamentoMaticula = false,
                FuncionarioCaixaId = alunoId.FuncionarioCaixaId,
                PrecoPropina = preco,
                NumeroDeMeses = mes,
            };

            int primeiroMes = (mes / mes);
            int ultimoMes = (mes);
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
                //var pagamento = _mapper.Map<PagamentoPropina>(PagamentoPropinas);

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
        }*/
        #endregion

        #region ENCARREGADO
        #region  CADASTRAR
        [AllowAnonymous]
        [Route("pagamento-de-propinas-do-seu-educando")]
        public IActionResult CreateEncarregado()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("pagamento-de-propinas-do-seu-educando")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEncarregado(PagamentoPropinaViewModel pagamento)
        {
            AlunoMatriculadoViewModel aluno = new();
            var encarregado = Guid.Parse("E225FFBA-77DC-4172-8019-08C4324B3DB6");
            if (pagamento.AlunoMatriculado.Id == Guid.Empty)
            {
                TempData["Erro"] = $"Deve inserir o número do BI/Cédula!";
                return RedirectToAction("CreateEncarregado");
            }

            var alunoId = pagamento.AlunoMatriculado.Id;
            var alunoInscrito = await _alunoMatriculadoRepository.ObterAlunoMatriculado(alunoId);

            if (alunoInscrito.Estado == false)
            {
                TempData["Erro"] = $"Matrícula pendente): Este aluno pode fazer pagamentos!";
                return RedirectToAction("CreateEncarregado");
            }
        
            var contador = _context.Propinas.Where(c => ((int)c.Situacao) == 4)
             .Select(c => c.PagamentoPropinaId).Count();
            if (contador == 0)
            {
                TempData["Erro"] = $"Opa ): Este aluno não tem dívida!";
                return RedirectToAction("CreateEncarregado");
            }
            PagamentoPropinaViewModel Pagar = new()
            {
                AlunoMatriculadoId = pagamento.AlunoMatriculado.Id,
                //PercentualDesconto = 0,
                ValorDesconto = 0,
                TotalPago = 0,
                TipoPagamento = 1,
                //NumeroDeTransacaoDePagamento = "123456",
                Ativo = false,
                PagamentoMaticula = false,
                Descricao = "Nenhum mes",
                FuncionarioCaixaId = encarregado,
                NumeroDeMeses = 0,
                PrecoPropina = 0,
            };


            #region REMOVER DO MODEL STATE 
            ModelState.Remove("NumDocumento");
            //ModelState.Remove("PercentualDesconto");
            ModelState.Remove("ValorDesconto");
            ModelState.Remove("TotalPago");
            ModelState.Remove("Descricao");
            ModelState.Remove("TipoPagamento");
            //ModelState.Remove("NumeroDeTransacaoDePagamento");
            ModelState.Remove("Ativo");
            ModelState.Remove("FuncionarioCaixaId");
            ModelState.Remove("NumeroDeMeses");
            ModelState.Remove("PrecoPropina");

            ModelState.Remove("AlunoMatriculado.Id");
            ModelState.Remove("AlunoMatriculado.Nome");
            ModelState.Remove("AlunoMatriculado.Descricao");
            //ModelState.Remove("AlunoMatriculado.NumeroDeTransacaoDePagamento");
            ModelState.Remove("AlunoMatriculado.NumDocumento");
            #endregion

            if (!ModelState.IsValid) return View(pagamento);

        
            await _pagamentoPropinaService.Adicionar(_mapper.Map<PagamentoPropina>(Pagar));
            if (!OperacaoValida()) return View("CreateEncarregado", pagamento);
            //{
            //    //TempData["Erro"] = $"Este aluno já possui um pagamento pendente. Deve regularizar antes o pagamento!";
            //    TempData["Erros"] = OperacaoValida();
            //    return RedirectToAction("CreateEncarregado", aluno);
            //}

            return RedirectToAction("IndexEncarregado");
        }

        [AllowAnonymous]
        public async Task<IActionResult> BuscarAlunoEncarregado(AlunoMatriculadoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("CreateEncarregado"); }
            var alunoMatriculado = await _alunoMatriculadoRepository.ObterDocumentoDoAluno(aluno.NumDocumento);
            var classe = await _classeRepository.ObterClassePorId(alunoMatriculado.ClasseId);
            TempData["Id"] = alunoMatriculado.Id;
            TempData["Nome"] = alunoMatriculado.Nome;
            TempData["Classe"] = classe.Nome;

            return RedirectToAction("CreateEncarregado");
        }
        #endregion

        #region MÉTODO PARA EFETUAR PAGAMENTO
        [AllowAnonymous]
        [Route("efetuar-pagamento-do-educando/{id:guid}")]
        public async Task<IActionResult> EfetuarPagamentoEncarregado(Guid id)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaMeses = (from p in _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse())
                                  //select new SelectListItem() { Text = p.Mes.NomeMes, Value = p.MesId.ToString() }).ToList();
                              select new SelectListItem() { Text = p.DescricaoPropina, Value = p.Id.ToString() }).ToList();
            listaMeses.Insert(0, new SelectListItem() { Text = "-- Selecione a referência da mensalidade --", Value = string.Empty });

            ViewBag.Mes = listaMeses;

            #endregion

            var pagamento = await ObterPagamentoPropinaMesAlunoMatriculadoClasse(id);
            ViewBag.Id = pagamento.Id;
            //await _propinaRepository.ObterPropinasPorPagamento(id);
            //await ObterPropinaItems(id);
            ViewBag.Nome = pagamento.AlunoMatriculado.Nome;
            if (pagamento == null)
            {
                return NotFound();
            }
            return View(pagamento);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("efetuar-pagamento-do-educando/{id:guid}")]
        public async Task<IActionResult> EfetuarPagamentoEncarregado(Guid id, PagamentoPropinaViewModel pagamentoPropinaViewModel)
        {
            #region LISTAR PRODUTOS E VENDEDORES
            var listaMeses = (from p in _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse())
                              select new SelectListItem() { Text = p.DescricaoPropina, Value = p.Id.ToString() }).ToList();
            listaMeses.Insert(0, new SelectListItem() { Text = "-- Selecione a referência da mensalidade --", Value = string.Empty });

            ViewBag.Mes = listaMeses;

            #endregion
            if (id != pagamentoPropinaViewModel.Id) return NotFound();

            #region VARIAVEIS PARA BUSCA DE VALORES
            var propinaId = pagamentoPropinaViewModel.Propina.Id;
            var propina = await _propinaRepository.ObterPropinaAlunoMatriculado(propinaId);
            var preco = await ObterClassePeloPreco(propina.ClasseId);
            var pagamentoPropina = await _pagamentoPropinaRepository.ObterPagamentoPropina(id);
            #endregion

            PropinaViewModel AtualizarPropina = new()
            {
                AlunoMatriculadoId = propina.AlunoMatriculadoId,
                MesId = propina.MesId,
                TurmaId = propina.TurmaId,
                ClasseId = propina.ClasseId,
                DescricaoPropina = propina.DescricaoPropina,
                PrecoPropina = preco.PrecoPropina,
                AnoLetivo = propina.AnoLetivo,
                Situacao = 1,
                PagamentoPropinaId = pagamentoPropinaViewModel.Id
            };

            #region REMOVER DO MODEL STATE 
            ModelState.Remove("Descricao");
            ModelState.Remove("Propina.AnoLetivo");
            ModelState.Remove("Propina.DescricaoPropina");
            ModelState.Remove("NumDocumento");

            #endregion

            if (!ModelState.IsValid) return View(pagamentoPropinaViewModel);
            await _propinaRepository.Remover(propina.Id);
            await _propinaRepository.Adicionar(_mapper.Map<Propina>(AtualizarPropina));

            var propinaAluno = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinasPorPagamento(pagamentoPropinaViewModel.Id));
            return RedirectToAction("EfetuarPagamentoEncarregado", propinaAluno);
        }
        #endregion
        #endregion

        #region MÉTODO PARA EXCLUIR PAGAMENTO

        [ClaimsAuthorize("PagamentoPropinas", "EX")]
        [Route("excluir-item-de-pagamento/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await ObterPropinaPorId(id);
            var idPagamentoPropina = item.PagamentoPropinaId;
            var mes =  await ObterNomeMes(item.MesId);
            var pagamentoPropinaAtualizacao = await ObterPagamentoPropina(idPagamentoPropina);
            var preco = await ObterClassePeloPreco(pagamentoPropinaAtualizacao.AlunoMatriculado.ClasseId);
            if (item == null) return NotFound();
            //await _produtoService.Remover(id);
            await _propinaRepository.Remover(id);
            
            #region PEGAR O USUARIO LOGADO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            Guid guidFuncionario = emailFuncionario.Id;
            var estadoFuncionario = emailFuncionario.Ativo;
            var email = emailFuncionario.Email;

            if (email != nomeUsuarioLogado || estadoFuncionario == false)
            {
                TempData["Erro"] = "Opa! Este Funcionário não Existe, deve solicitar ao Administrador :(";
                return RedirectToAction("EfetuarPagamento");
            }
            #endregion

            #region REPOR PROPINA
            if (!ModelState.IsValid) return View(pagamentoPropinaAtualizacao);
          
            PropinaViewModel AdicionarPropina = new()
            {
                //Id = propina.Id,
                AlunoMatriculadoId = item.AlunoMatriculadoId,
                MesId = item.MesId,
                TurmaId = item.TurmaId,
                ClasseId = item.ClasseId,
                DescricaoPropina = $"Referente ao mês de {mes.NomeMes}",
                PrecoPropina = 0,
                AnoLetivo = item.AnoLetivo,
                Situacao = 4,
                PagamentoPropinaId = item.PagamentoPropinaId
            };
            await _propinaRepository.Adicionar(_mapper.Map<Propina>(AdicionarPropina));

            //await _propinaRepository.Atualizar(_mapper.Map<Propina>(AtualizarPropina));
            //pedidoAtualizacao.ValorTotal = (pedidoAtualizacao.ValorTotal - (item.Quantidade * item.ValorUnitario));
            //await _pagamentoPropinaRepository.Atualizar(_mapper.Map<PagamentoPropina>(pedidoAtualizacao));


            PagamentoPropinaViewModel PagamentoPropinaAtualizacao = new()
            {
                Id = pagamentoPropinaAtualizacao.Id,
                Codigo = pagamentoPropinaAtualizacao.Codigo,
                AlunoMatriculadoId = pagamentoPropinaAtualizacao.AlunoMatriculadoId,
                FuncionarioCaixaId = guidFuncionario,
                ValorDesconto = pagamentoPropinaAtualizacao.ValorDesconto,
                Descricao = pagamentoPropinaAtualizacao.Descricao,
                //NumeroDeMeses = (int(pagamentoPropinaAtualizacao.TotalPago / preco.PrecoPropina)),
                NumeroDeMeses = ((int)((pagamentoPropinaAtualizacao.TotalPago - preco.PrecoPropina) / preco.PrecoPropina)),
                PrecoPropina = preco.PrecoPropina,
                //TotalPago = pagamentoPropinaAtualizacao.TotalPago - preco.PrecoPropina - pagamentoPropinaAtualizacao.ValorDesconto,
                TotalPago = pagamentoPropinaAtualizacao.TotalPago - preco.PrecoPropina,
                TipoPagamento = pagamentoPropinaAtualizacao.TipoPagamento,
                Ativo = false,
                PagamentoMaticula = false,
            };
            _context.ChangeTracker.Clear();
            await _pagamentoPropinaRepository.Atualizar(_mapper.Map<PagamentoPropina>(PagamentoPropinaAtualizacao));
            #endregion
            //if (!OperacaoValida()) return View(produto);
            TempData["Sucesso"] = "Item excluido com sucesso!";

            return RedirectToAction("ItemDeleted");
        }

        [ClaimsAuthorize("PagamentoPropinas", "VI")]
        [Route("item-de-pagamento-excluido")]
        public async Task<IActionResult> ItemDeleted()
        {
            return View(_mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterPagamentoPropinasAlunoMatriculados()));
        }
        [ClaimsAuthorize("PagamentoPropinas", "EX")]
        [Route("excluir-pagamento/{id:guid}")]
        public async Task<IActionResult> Apagar(Guid id)
        {
            var propina = await ObterPagamentoPropina(id);

            if (propina == null)
            {
                return NotFound();
            }
            return View(propina);
            //return RedirectToAction("IndexFinalizado");
        }

        [ClaimsAuthorize("PagamentoPropinas", "EX")]
        public async Task<IActionResult> ApagarPagamentoPropina(Guid id, PagamentoPropinaViewModel pagamentoPropinaViewModel)
        {
            var index = _mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterPagamentoPropinasAlunoMatriculados());
          
            if (pagamentoPropinaViewModel == null) return NotFound();

            Guid alunoId = _context.Propinas.Where(c => c.PagamentoPropinaId == pagamentoPropinaViewModel.Id)
                .Select(c => c.AlunoMatriculadoId).FirstOrDefault();
            Guid pagamentoId = _context.PagamentoPropinas.Where(c => c.AlunoMatriculadoId == alunoId)
               .Select(c => c.Id).FirstOrDefault();
            if (alunoId == Guid.Empty || pagamentoId == Guid.Empty)
            {
                await _pagamentoPropinaRepository.Remover(id);
                return RedirectToAction("Index", index);
            }
            

            var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == pagamentoPropinaViewModel.Id)
              .Select(c => c.PagamentoPropinaId).Count();

            for (int i = 0; i < contador; i++)
            {
                var propina = await ObterPropinasPeloPagamento(pagamentoPropinaViewModel.Id);
                var mes = await ObterNomeMes(propina.MesId);
                await _propinaRepository.Remover(propina.Id);
               
                #region REPOR A QUANTIDADE NO STOQUE DO PRODUTO
                _context.ChangeTracker.Clear(); 
                if (ModelState.IsValid) return View(pagamentoPropinaViewModel); //await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
                await _context.SaveChangesAsync();
               
                #region REPOR PROPINA
                //if (!ModelState.IsValid) return View(produtoAtualizacao);
                PropinaViewModel ReporPropina = new()
                {
                    AlunoMatriculadoId = alunoId,
                    MesId = propina.MesId,
                    TurmaId = propina.TurmaId,
                    ClasseId = propina.ClasseId,
                    DescricaoPropina = $"Referente ao mês de {mes.NomeMes}",
                    PrecoPropina = 0,
                    AnoLetivo = propina.AnoLetivo,
                    Situacao = 4,
                    PagamentoPropinaId = pagamentoId
                };
                await _propinaRepository.Adicionar(_mapper.Map<Propina>(ReporPropina));
                #endregion

                #endregion
            }
            //await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();
            await _pagamentoPropinaRepository.Remover(id);

            //if (!OperacaoValida()) return View(produto);
            return RedirectToAction("Index", index);
        }

        #endregion

        #region FINALIZAR A PAGAMENTO
        [ClaimsAuthorize("PagamentoPropinas", "AD")]
        public async Task<IActionResult> FinalizarPagamento(Guid id, PagamentoPropinaViewModel pagamentoPropinaViewModel)
        {
           if (id != pagamentoPropinaViewModel.Id) return NotFound();
            var pagamentoPropinaAtualizacao = await ObterPagamentoPropinaMesAlunoMatriculadoClasse(id);
            var itemPagamentoPropinaId = pagamentoPropinaAtualizacao.Id;
            var pagamentoPropina = await ObterPagamentoPropinaPropinasAlunoMatriculado(id);
         
            var contador = _context.Propinas.Where(c => c.PagamentoPropinaId == itemPagamentoPropinaId)
            .Select(c => c.PagamentoPropinaId).Count();
           
            #region PEGAR O USUARIO LOGADO
            var usuario = HttpContext.User.Identity;
            var nomeUsuarioLogado = usuario.Name;
            var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            Guid guidFuncionario = emailFuncionario.Id;
            var estadoFuncionario = emailFuncionario.Ativo;
            var email = emailFuncionario.Email;

            if (email != nomeUsuarioLogado || estadoFuncionario == false)
            {
                TempData["Erro"] = "Opa! Este Funcionário não Existe, deve solicitar ao Administrador :(";
                return RedirectToAction("EfetuarPagamento");
            }
            #endregion
            if (ModelState.IsValid) return View(pagamentoPropinaViewModel);
            if (contador == 0)
            {
                TempData["Erro"] = "Opa ): Não é possível terminar a venda. Item Vazio!";
                return RedirectToAction("EfetuarPagamento", pagamentoPropina);
            }

            PagamentoPropinaViewModel PagamentoPropina = new()
            {
                Id = pagamentoPropinaAtualizacao.Id,
                Codigo = pagamentoPropinaAtualizacao.Codigo,
                AlunoMatriculadoId = pagamentoPropinaAtualizacao.AlunoMatriculadoId,
                FuncionarioCaixaId = guidFuncionario,
                ValorDesconto = pagamentoPropinaAtualizacao.ValorDesconto,
                Descricao = pagamentoPropinaAtualizacao.Descricao,
                NumeroDeMeses = pagamentoPropinaAtualizacao.NumeroDeMeses,
                PrecoPropina = pagamentoPropinaAtualizacao.PrecoPropina,
                TotalPago = pagamentoPropinaAtualizacao.TotalPago ,
                TipoPagamento = pagamentoPropinaAtualizacao.TipoPagamento,
                Ativo = true,
                PagamentoMaticula = false,
            };
            await _pagamentoPropinaRepository.Atualizar(_mapper.Map<PagamentoPropina>(PagamentoPropina));

            //if (!OperacaoValida()) return View(produto);
            var index = _mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterPagamentoPropinasAlunoMatriculados());
            
            return RedirectToAction("IndexFinalizado", index);
        }
        private async Task<PagamentoPropinaViewModel> ObterPagamentoPropinaPropinasAlunoMatriculado(Guid id)
        {
            var propina = _mapper.Map<PagamentoPropinaViewModel>(await _pagamentoPropinaRepository.ObterPagamentoPropinaPropinasAlunoMatriculado(id));
            propina.Propinas = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinasPagamentoPropina(id));
            return propina;
        }
        private async Task<PagamentoPropinaViewModel> ObterPagamentoPropina(Guid id)
        {
            var propina = _mapper.Map<PagamentoPropinaViewModel>(await _pagamentoPropinaRepository.ObterPagamentoPropinaPropinasAlunoMatriculado(id));
            propina.Propinas = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinasPagamentoPropina(id));
            propina.AlunoMatriculados = _mapper.Map<IEnumerable<AlunoMatriculadoViewModel>>(await _alunoMatriculadoRepository.ObterTodos());
            return propina;
        }

        #endregion

        #region ESTATISTICA
        
        [ClaimsAuthorize("PagamentoPropinas", "DG")]
        [Route("estatistica-financeira-de-propinas")]
        public async Task<IActionResult> Estatistica()
        {
            return View(_mapper.Map<IEnumerable<PagamentoPropinaViewModel>>(await _pagamentoPropinaRepository.ObterEstatisticaDePagamentos()));
        }

        #endregion
       
        public IActionResult ImprimirPagamentoPropina(PagamentoPropinaViewModel pagamentoPropinaViewModel)
        {
            PagamentoPropinaViewModel modelo = _context.PagamentoPropinas.Include(dv => dv.Propinas).Where(v => v.Id == pagamentoPropinaViewModel.Id && v.PagamentoMaticula == false)
              .Select(v => new PagamentoPropinaViewModel()
              {
                  Id = v.Id,
                  Codigo = v.Codigo,
                  AlunoMatriculadoId = v.AlunoMatriculadoId,
                  Nome = v.AlunoMatriculado.Nome,
                  Documento = v.AlunoMatriculado.NumDocumento,
                  ValorDesconto = v.ValorDesconto,
                  TotalPago = v.TotalPago,
                  DataCodigo = "JI_" + v.DataCadastro.ToString("dd/MM/yyyy") + '_' + v.Codigo,
                  PropinaDetalhe = v.Propinas.Where(v => ((int)v.Situacao) == 1 && v.PagamentoPropina.PagamentoMaticula == false).Select(dv => new PropinaViewModel()
                  {
                      DescricaoPropina = dv.DescricaoPropina,
                      PrecoPropina = dv.PrecoPropina,
                  }).ToList()
              }).FirstOrDefault();
            
                return new ViewAsPdf("ImprimirPagamentoPropina", modelo)
                {
                    FileName = $"Propina {modelo.DataCodigo}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize = Rotativa.AspNetCore.Options.Size.A4
                };
          
        }

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<PagamentoPropinaViewModel> PopularPropinas(PagamentoPropinaViewModel pagamento)
        {
            pagamento.Propinas =  _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse());
            return pagamento;
        }
        private async Task<ClasseViewModel> ObterClassePeloPreco(Guid id)
        {
            var classe = _mapper.Map<ClasseViewModel>(await _classeRepository.ObterClassePeloPreco(id));
            return classe;
        }

        private async Task<PropinaViewModel> ObterPropinasPeloPagamento(Guid id)
        {
            return _mapper.Map<PropinaViewModel>(await _propinaRepository.ObterPropinasPeloPagamento(id));
        }
        private async Task<PropinaViewModel> ObterPropinaPeloAluno(Guid id)
        {
            return _mapper.Map<PropinaViewModel>(await _propinaRepository.ObterPropinaPeloAluno(id));
        }
        private async Task<MesViewModel> ObterNomeMes(Guid id)
        {
            var mes = _mapper.Map<MesViewModel>(await _mesRepository.ObterNomeMes(id));
            return mes;
        }
        private async Task<PagamentoPropinaViewModel> ObterPropinaItems(Guid id)
        {
            var pagamento = _mapper.Map<PagamentoPropinaViewModel>(await _pagamentoPropinaRepository.ObterPorId(id));
            pagamento.Propinas = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinasPorPagamento(id));
            return pagamento;
        }
        private async Task<PagamentoPropinaViewModel> ObterPagamentoPropinaMesAlunoMatriculadoClasse(Guid id)
        {
            var pagamento = _mapper.Map<PagamentoPropinaViewModel>(await _pagamentoPropinaRepository.ObterPagamentoPropina(id));
            //pagamento.Propinas = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinaMesAlunoMatriculadoClasse());
            pagamento.Propinas = _mapper.Map<IEnumerable<PropinaViewModel>>(await _propinaRepository.ObterPropinasPorPagamento(id));
            return pagamento;
        }
        private async Task<PagamentoPropinaViewModel> ObterPagamentoPropinaAlunoMatriculado(Guid id)
        {
            var pagamento = _mapper.Map<PagamentoPropinaViewModel>(await _pagamentoPropinaRepository.ObterPagamentoPropinaAlunoMatriculado(id));
            return pagamento;
        }
        private async Task<ClasseViewModel> ObterNomeClasse(Guid id)
        {
            var mes = _mapper.Map<ClasseViewModel>(await _classeRepository.ObterNomeClasse(id));
            return mes;
        }
        
        private async Task<AlunoMatriculadoViewModel> ObterAlunoMatriculado(Guid id)
        {
            var aluno = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterAlunoMatriculado(id));
            return aluno;
        }
        private async Task<ClasseViewModel> ObterClassePorId(Guid id)
        {
            var classe = _mapper.Map<ClasseViewModel>(await _classeRepository.ObterClassePorId(id));
            return classe;
        }
        private async Task<PropinaViewModel> ObterPropinaPorId(Guid id)
        {
            var item = _mapper.Map<PropinaViewModel>(await _propinaRepository.ObterPorId(id));
            return item;
        }
        private async Task<bool> PagarPropina(Guid id)
        {
            var alunoId = await ObterAlunoMatriculado(id);
            //var classeObitida = await _classeRepository.ObterClasse(alunoId.ClasseId);
            //var preco = classeObitida.PrecoPropina;
            var nomeMes = await _mesRepository.ObterMesPeloCodigo(1);

            PagamentoPropinaViewModel PagamentoPropinas = new()
            {
                AlunoMatriculadoId = alunoId.Id,
                //PercentualDesconto = 0,
                ValorDesconto = 0,
                TotalPago = 0,
                TipoPagamento = 1,
                //NumeroDeTransacaoDePagamento = "0001",
                Ativo = false,
                PagamentoMaticula = false,
                FuncionarioCaixaId = alunoId.FuncionarioCaixaId,
                PrecoPropina = 0,
                NumeroDeMeses = 1,
                Descricao = $"Pago o mês de {nomeMes.NomeMes}",
            };

            //int primeiroMes = (mes / mes);
            //int ultimoMes = (mes);
            //var nomeMesPrimeiro = await _mesRepository.ObterMesPeloCodigo(primeiroMes);
            //var nomeMesUltimo = await _mesRepository.ObterMesPeloCodigo(ultimoMes);

            var pagamentoUmMes = _mapper.Map<PagamentoPropina>(PagamentoPropinas);
            await _pagamentoPropinaRepository.Adicionar(pagamentoUmMes);

            return true;
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
        private async Task<bool> ValidarDocumento(AlunoMatriculadoViewModel aluno)
        {
            var alunoMatriculado = await _alunoMatriculadoRepository.ObterDocumentoDoAluno(aluno.NumDocumento);
            if (alunoMatriculado == null)
            {
                TempData["Erro"] = $"Opa ): Este documento com o número '{aluno.NumDocumento}', não existe!";
                //AdicionarErroProcessamento($"Opa ): Este documento com o número '{aluno.NumDocumento}', não existe!");
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarDocumentoMatriculado(AlunoMatriculadoViewModel aluno)
        {
            var alunoMatriculado = await _alunoMatriculadoRepository.ObterDocumentoDoAlunoMatriculado(aluno.NumDocumento);
            if (alunoMatriculado == null)
            {
                TempData["Erro"] = $"Opa ): Este documento com o número '{aluno.NumDocumento}', não existe!";
                return false;
            }
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
     
        #endregion
    }
}

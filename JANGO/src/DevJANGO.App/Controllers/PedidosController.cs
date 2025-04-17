using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using DevJANGO.Data.Repository;
using DevJANGO.App.Extensions;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore;


namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class PedidosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        //private readonly ApplicationDbContext _user;
        private readonly UserManager<IdentityUser> _user;
        private readonly JangoDbContext _context;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoItemsRepository _pedidoItemsRepository;
        private readonly IAlunoMatriculadoRepository _alunoMatriculadoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;
        private readonly IFuncionarioCaixaRepository _funcionarioCaixaRepository;
        private readonly IClasseRepository _classeRepository;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public PedidosController(
            UserManager<IdentityUser> user,
            JangoDbContext context,
            IPedidoRepository pedidoRepository,
            IPedidoItemsRepository pedidoItemsRepository,
            IMapper mapper,
            INotificador notificador,
            IAlunoMatriculadoRepository alunoMatriculadoRepository,
            IFuncionarioCaixaRepository funcionarioCaixaRepository,
            IProdutoRepository produtoRepository,
            IClasseRepository classeRepository) : base(notificador)
        {
            _user = user;
            _context = context;
            _pedidoRepository = pedidoRepository;
            _pedidoItemsRepository = pedidoItemsRepository;
            _mapper = mapper;
            _alunoMatriculadoRepository = alunoMatriculadoRepository;
            _funcionarioCaixaRepository = funcionarioCaixaRepository;
            _produtoRepository = produtoRepository;
            _classeRepository = classeRepository;
        }
        #endregion

        #region MÉTODO PARA LISTAR GERAL

        //[ClaimsAuthorize("Pedidos", "VI")]
        [Route("lista-de-alunos-para-o-pedido")]
        public async Task<IActionResult> Index()
        {
            //return _context.Pedidos != null ?
            //             View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados())) :
            //             Problem("Entity set 'JangoDbContext.Pedidos'  is null.");

            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados()));
        }
        #endregion

        #region MÉTODO PARA CADASTRAR NOVO
        //[AllowAnonymous]
        //[ClaimsAuthorize("Pedidos", "AD")]
        [Route("seleciona-o-aluno-para-o-pedido")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ClaimsAuthorize("Pedidos", "AD")]
        [Route("seleciona-o-aluno-para-o-pedido")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PedidoViewModel pedidoViewModel)
        {
            if (pedidoViewModel.AlunoMatriculado.Id == Guid.Empty)
            {
                TempData["Erro"] = $"Deve inserir o número do BI/Cédula!";
                return RedirectToAction("Create");
            }
            //var alunoId = pedidoViewModel.AlunoMatriculado.Id;
            #region PEGAR O USUARIO
            //var usuario = HttpContext.User.Identity;
            //var nomeUsuarioLogado = usuario.Name;
            //var emailFuncionario = await _funcionarioCaixaRepository.ObterFuncionario(nomeUsuarioLogado);
            #endregion

            if (!await ValidarDataFuncionario()) return RedirectToAction("Create");
            //pedidoViewModel.ValorDesconto = 0;
            //pedidoViewModel.ValorTotal = 0;
            //pedidoViewModel.OperacaoPedidos = 1;
            //pedidoViewModel.Situacao = 1;
            //pedidoViewModel.TipoPagamento = 2;
            //pedidoViewModel.NumeroDeTransacaoDePagamento = "123456";

            PedidoViewModel Pedido = new()
            {
                AlunoMatriculadoId = pedidoViewModel.AlunoMatriculado.Id,
                ValorDesconto = 0,
                ValorTotal = 0,
                OperacaoPedidos = 1,
                Situacao = 1,
                TipoPagamento = 2,
                //NumeroDeTransacaoDePagamento = "123456",
              //FuncionarioCaixaId = emailFuncionario.Id,
            };

            //pedidoViewModel = await PopularAlunoMatriculados(pedidoViewModel);
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

            if (!ModelState.IsValid) return View(pedidoViewModel);
          
            await _pedidoRepository.Adicionar(_mapper.Map<Pedido>(Pedido));
            //if (!OperacaoValida()) return View(pedidoViewModel);
            
            //var pedido = await ObterPedidoPedidoItemsCliente(id) ;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> BuscarAluno(AlunoMatriculadoViewModel aluno)
        {
            if (!await ValidarDocumento(aluno)) { return RedirectToAction("Create"); }
            var alunoMatriculado = await _alunoMatriculadoRepository.ObterDocumentoDoAluno(aluno.NumDocumento);
            var classe = await _classeRepository.ObterClassePorId(alunoMatriculado.ClasseId);
            TempData["Id"] = alunoMatriculado.Id;
            TempData["Nome"] = alunoMatriculado.Nome;
            TempData["Classe"] = classe.Nome;

            return RedirectToAction("Create");
        }

    
        #endregion

        #region MÉTODO PARA EDITAR

        //[ClaimsAuthorize("Pedidos", "AD")]
        [Route("carrinho-do-aluno-e-pedido/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var pedido = await ObterPedidoPedidoItemsAlunoMatriculado(id);

            ViewBag.Id = pedido.Id;
            
            var cliente = await ObterPedidoPorAlunoMatriculado(id);
            ViewBag.Nome = cliente.AlunoMatriculado.Nome;

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        [HttpPost]
        //[ClaimsAuthorize("Pedidos", "AD")]
        [ValidateAntiForgeryToken]
        [Route("carrinho-do-aluno-e-pedido/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, PedidoViewModel pedidoViewModel)
        {
            #region VARIAVEIS PARA BUSCA DE VALORES
            var idproduto = pedidoViewModel.Produto.Id;
            var produto = await ObterNomeProduto(idproduto);
            var valorVenda = produto.ValorVenda;

            var produtoAtualizacao = await ObterProdutoPorId(idproduto);
            var pedido = await ObterPedido(id);
            #endregion

            #region BUSCA PRODUTO
            ViewBag.ProdutoId = idproduto;
            var quantidadeProduto = produto.QuantidadeEstoque;
            var nome = produto.Nome;

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
                return RedirectToAction("Edit");
            }
            #endregion

            var quantidadeEstoque = (quantidadeProduto - pedidoViewModel.PedidoItem.Quantidade);
            ViewBag.QuantidadeEstoque = quantidadeEstoque;

            #region ACTUALIZAR STOQUE DO PRODUTO
            pedidoViewModel.PedidoItem.NomeProduto = nome;
            ProdutoViewModel Produto = new()
            {
                Id = produtoAtualizacao.Id,
                Nome = produtoAtualizacao.Nome,
                Descricao = produtoAtualizacao.Descricao,
                ValorVenda = produtoAtualizacao.ValorVenda,
                QuantidadeEstoque = quantidadeEstoque,
                Ativo = produtoAtualizacao.Ativo,

            };
            PedidoItemViewModel PedidoItem = new()
            {
                PedidoId = pedido.Id,
                ProdutoId = idproduto,
                NomeProduto = produtoAtualizacao.Nome,
                Quantidade = pedidoViewModel.PedidoItem.Quantidade,
                ValorUnitario = produtoAtualizacao.ValorVenda,
                FuncionarioCaixaId = guidFuncionario,
            };
            if (quantidadeEstoque > 0)
            {
                await _pedidoItemsRepository.Adicionar(_mapper.Map<PedidoItem>(PedidoItem));
                await _produtoRepository.Atualizar(_mapper.Map<Produto>(Produto));
            }
            else
            {
                TempData["Erro"] = "Opa! Estoque Esgotado :(";
            }
            #endregion

            #region ACTUALIZAR PEDIDO
            _context.ChangeTracker.Clear();
            if (id != pedidoViewModel.Id) return NotFound();
          var pedidoAtualizacao = await ObterPedido(id);
            
            //var p3 = pedidoAtualizacao.ValorDesconto;

            pedidoViewModel.AlunoMatriculado = pedidoAtualizacao.AlunoMatriculado;
            var aluno = pedidoAtualizacao.AlunoMatriculado.Id;

            PedidoViewModel Pedido = new()
            {
                Id = pedidoViewModel.Id,
                AlunoMatriculadoId= aluno,
               ValorDesconto = pedidoViewModel.ValorDesconto,
               ValorTotal = ((pedidoAtualizacao.ValorTotal +
               (pedidoViewModel.PedidoItem.Quantidade * valorVenda)) - pedidoViewModel.ValorDesconto),

             OperacaoPedidos = pedidoViewModel.OperacaoPedidos,
             Situacao = pedidoViewModel.Situacao,
             TipoPagamento = pedidoViewModel.TipoPagamento,
             //NumeroDeTransacaoDePagamento = pedidoViewModel.NumeroDeTransacaoDePagamento,
             Ativo = pedidoViewModel.Ativo,
            };
             _context.ChangeTracker.Clear();
            await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(Pedido));
            #endregion

            #region REMOVER DO MODEL STATE
            ModelState.Remove("Nome");
            ModelState.Remove("Filial");
            ModelState.Remove("Descricao");
            ModelState.Remove("Produto.Id");
            ModelState.Remove("Produto.Nome");
            ModelState.Remove("Produto.Ativo");
            ModelState.Remove("Produto.Filial");
            ModelState.Remove("Produto.Descricao");
            ModelState.Remove("Produto.ValorVenda");
            ModelState.Remove("Produto.ValorVenda");
            ModelState.Remove("Produto.CategoriaId");
            ModelState.Remove("Produto.ValorCompra");
            ModelState.Remove("Produto.FabricanteId");
            ModelState.Remove("Produto.FornecedorId");
            ModelState.Remove("Produto.QuantidadeEstoque");
            ModelState.Remove("PedidoItem.VendedorId");
            ModelState.Remove("PedidoItem.NumeroDeSerie");
            ModelState.Remove("PedidoItem.ValorUnitario");
            ModelState.Remove("PedidoItem.FuncionarioCaixaId");
            #endregion

            if (!ModelState.IsValid) return View("Edit");
          
            //await _pedidoService.Atualizar(pedido);
            // if (!OperacaoValida()) return View(await ObterPedidoClientes(id));

            var pedidoItems = _mapper.Map<IEnumerable<PedidoItemViewModel>>(await _pedidoItemsRepository.ObterPedidoItemsPedido(pedido.Id));
          
            return RedirectToAction("Edit", pedidoItems);
        }

        public async Task<IActionResult> BuscarProduto(ProdutoViewModel produto)
        {
           if (!await ValidarProduto(produto)) { return RedirectToAction("ProdutoNaoEncontrado"); }
            var produtoVenda = await _produtoRepository.ObterProdutoPeloCodigo(produto.Codigo);
            TempData["Id"] = produtoVenda.Id;
            TempData["Nome"] = produtoVenda.Nome;
            TempData["Stoque"] = produtoVenda.QuantidadeEstoque;
            return RedirectToAction("ProdutoEncontrado");
        }
        #endregion

        #region FINALIZAR A VENDA
        public async Task<IActionResult> FinalizarVenda(Guid id, PedidoViewModel pedidoViewModel)
        {
            #region ACTUALIZAR PEDIDO
            if (id != pedidoViewModel.Id) return NotFound();
            var pedidoAtualizacao = await ObterPedido(id);
            var itemDoPedidoId = pedidoAtualizacao.Id;
            var pedido = await ObterPedidoPedidoItemsAlunoMatriculado(id);
            var contador = _context.PedidoItems.Where(c => c.PedidoId == itemDoPedidoId)
              .Select(c => c.PedidoId).Count();

            if (!ModelState.IsValid) return View(pedidoViewModel);
           
            if (contador == 0)
            {
                TempData["Erro"] = "Opa ): Não é possível terminar a venda. Item Vazio!";
                return RedirectToAction("Edit", pedido);
            }
            PedidoViewModel Pedido = new()
            {
                Id = pedidoAtualizacao.Id,
                AlunoMatriculadoId = pedidoAtualizacao.AlunoMatriculadoId,
                ValorDesconto = pedidoViewModel.ValorDesconto,
                ValorTotal = (pedidoAtualizacao.ValorTotal - pedidoAtualizacao.ValorDesconto),

                OperacaoPedidos = pedidoAtualizacao.OperacaoPedidos,
                Situacao = 2,
                TipoPagamento = pedidoAtualizacao.TipoPagamento,
                Ativo = true,
            };
            _context.ChangeTracker.Clear();
            await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(Pedido));
            //await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(pedidoAtualizacao));

            #endregion

            //if (!OperacaoValida()) return View(produto);
            var index = _mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados());
            return RedirectToAction("Index", index);
        }

        public IActionResult ImprimirPagamento(PedidoViewModel pedidoViewModel)
        {
            PedidoViewModel modelo = _context.Pedidos.Include(dv => dv.PedidoItems).Where(v => v.Id == pedidoViewModel.Id &&  ((int)v.Situacao) == 1)
            //PagamentoPropinaViewModel modelo = _context.PagamentoPropinas.Include(dv => dv.Propinas).Where(v => v.Id == pagamentoPropinaViewModel.Id)
               .Select(v => new PedidoViewModel()
               {
                   Id = v.Id,
                   Codigo = v.Codigo,
                   AlunoMatriculadoId = v.AlunoMatriculadoId,
                   Nome = v.AlunoMatriculado.Nome,
                   Documento = v.AlunoMatriculado.NumDocumento,
                   ValorDesconto = v.ValorDesconto,
                   ValorTotal = v.ValorTotal,

                   //PedidoItemDetalhe = v.PedidoItems.Where(v => ((int)v.Situacao) == 1).Select(dv => new PedidoItemViewModel()
                   PedidoItemDetalhe = v.PedidoItems.Select(dv => new PedidoItemViewModel()
                   {
                       NomeProduto = dv.NomeProduto,
                       Quantidade = dv.Quantidade,
                       ValorUnitario = dv.ValorUnitario,
                   }).ToList()
               }).FirstOrDefault();

            return new ViewAsPdf("ImprimirPagamento", modelo)
            {
                //FileName = $"Pagamento {modelo.Codigo}-{modelo.Nome}.pdf",
                FileName = $"Pagamento {modelo.Codigo}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
        #endregion

        #region MÉTODO PARA EXCLUIR
        //[ClaimsAuthorize("Pedidos", "DG")]
        [Route("excluir-pedidoItem/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await ObterPedidoItemPorId(id);
            var idproduto = item.ProdutoId;
            var idpedido = item.PedidoId;
            var produtoAtualizacao = await ObterProdutoPorId(idproduto);
            var pedidoAtualizacao = await ObterPedido(idpedido);
 
            if (item == null) return NotFound();

            await _pedidoItemsRepository.Remover(id);

            #region REPOR A QUANTIDADE NO STOQUE DO PRODUTO

            if (!ModelState.IsValid) return View(produtoAtualizacao);
            //produtoAtualizacao.QuantidadeEstoque = (produtoAtualizacao.QuantidadeEstoque + item.Quantidade);
            produtoAtualizacao.QuantidadeEstoque += item.Quantidade;
            
             await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
      
            PedidoViewModel Pedido = new()
            {
                Id = pedidoAtualizacao.Id,
                AlunoMatriculadoId= pedidoAtualizacao.AlunoMatriculadoId,
                ValorDesconto = pedidoAtualizacao.ValorDesconto,
                ValorTotal = (pedidoAtualizacao.ValorTotal - (item.Quantidade * item.ValorUnitario)),

                OperacaoPedidos = pedidoAtualizacao.OperacaoPedidos,
                Situacao = pedidoAtualizacao.Situacao,
                TipoPagamento = pedidoAtualizacao.TipoPagamento,
                //NumeroDeTransacaoDePagamento = pedidoAtualizacao.NumeroDeTransacaoDePagamento,
                Ativo = pedidoAtualizacao.Ativo,
            };
            _context.ChangeTracker.Clear();
            await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(Pedido));

            //await _pedidoRepository.Atualizar(_mapper.Map<Pedido>(pedidoAtualizacao));

            #endregion

            //if (!OperacaoValida()) return View(produto);
            TempData["Sucesso"] = "Item excluido com sucesso!";
           
            return RedirectToAction("ItemDeleted");
        }

        [Route("item-excluido")]
        public async Task<IActionResult> ItemDeleted()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados()));
        }
        
        [Route("produto-encontrado")]
        public async Task<IActionResult> ProdutoEncontrado()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados()));
        }
        
        [Route("produto-nao-encontrado")]
        public async Task<IActionResult> ProdutoNaoEncontrado()
        {
            return View(_mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados()));
        }

        public async Task<IActionResult> ApagarPedido(Guid id)
        {
           var pedido = await ObterPedidoPorId(id);
            var idpedido = pedido.Id;

            var produto = await ObterPedidoItemsPeloPedido(idpedido);
            var index = _mapper.Map<IEnumerable<PedidoViewModel>>(await _pedidoRepository.ObterPedidosAlunoMatriculados());

            if (produto == null)
            {
                await _pedidoRepository.Remover(id);
                return RedirectToAction("Index", index);
            }

            var idproduto = produto.Produto.Id;
           
            var contador = _context.PedidoItems.Where(c => c.PedidoId == idpedido)
              .Select(c => c.PedidoId).Count();
            for (int i = 0; i < contador; i++)
            {
                var produto1 = await ObterPedidoItemsPeloPedido(idpedido);
                var idpedidoItem = produto1.Id;
                var idproduto1 = produto1.ProdutoId;
                await _pedidoItemsRepository.Remover(idpedidoItem);

                #region REPOR A QUANTIDADE NO STOQUE DO PRODUTO
                _context.ChangeTracker.Clear();
                var produtoAtualizacao = await ObterProdutoPorId(idproduto1);
                if (ModelState.IsValid) return View(produtoAtualizacao);

                //produtoAtualizacao.QuantidadeEstoque = (produtoAtualizacao.QuantidadeEstoque + produto.Quantidade);
                produtoAtualizacao.QuantidadeEstoque += produto.Quantidade;

                await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
                await _context.SaveChangesAsync();
                #endregion
            }

            if (pedido == null) return NotFound();
            //await _produtoService.Remover(id);
            await _pedidoRepository.Remover(id);

            //if (!OperacaoValida()) return View(produto);
            return RedirectToAction("Index", index);
        }
        #endregion

        #region MÉTODO PARA SERVIÇOS

        private async Task<PedidoViewModel> ObterPedidoPedidoItems(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoPedidoItems(id));
        }

        private async Task<PedidoViewModel> ObterPedidoPorAlunoMatriculado(Guid id)
        {
            var pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoAlunoMatriculados(id));
            return pedido;
        }
        private async Task<PedidoViewModel> ObterPedido(Guid id)
        {
            var pedido = _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoAlunoMatriculados(id));
            pedido.AlunoMatriculados = _mapper.Map<IEnumerable<AlunoMatriculadoViewModel>>(await _alunoMatriculadoRepository.ObterTodos());
            return pedido;
        }
        private async Task<PedidoViewModel> ObterPedidoPeloItems(Guid id)
        {
           return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoPeloItems(id));
        }
        private async Task<PedidoViewModel> PopularAlunoMatriculados(PedidoViewModel pedido)
        {
            pedido.AlunoMatriculados = _mapper.Map<IEnumerable<AlunoMatriculadoViewModel>>(await _alunoMatriculadoRepository.ObterTodos());
            return pedido;
        }
      
        private async Task<PedidoViewModel> PopularProdutos(PedidoViewModel pedido)
        {
            pedido.Produtos = _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterTodos());
            return pedido;
        }
        
        private async Task<PedidoItemViewModel> ObterPedidoItemsPeloPedido(Guid id)
        {
            return _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPedidoItemsPeloPedido(id));
        }
        private async Task<PedidoItemViewModel> ObterPedidosItemsProdutos()
        {
            return _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPedidosItemsProdutos());
        }
        private async Task<PedidoViewModel> ObterPedidoAlunoMatriculados(Guid id)
        {
            return _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoAlunoMatriculados(id));
        }

        private async Task<AlunoMatriculadoViewModel> ObterCarrinhoAlunoMatriculado(Guid id)
        {
            var cliente = _mapper.Map<AlunoMatriculadoViewModel>(await _alunoMatriculadoRepository.ObterPorId(id));
            return cliente;
        }
        private async Task<ProdutoViewModel> ObterNomeProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterNomeProduto(id));
            return produto;
        }
        private async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoPorId(id));
            return produto;
        }
        private async Task<PedidoItemViewModel> ObterPedidoItemPorId(Guid id)
        {
            var item = _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPorId(id));
            return item;
        }
        private async Task<PedidoViewModel> ObterPedidoPorId(Guid id)
        {
            var item = _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPorId(id));
            return item;
        }
        private async Task<PedidoItemViewModel> ObterPedidoItemsPorPedido(Guid id)
        {
            return _mapper.Map<PedidoItemViewModel>(await _pedidoItemsRepository.ObterPedidoItemsPorPedido(id));
        }
        private async Task<PedidoViewModel> ObterPedidoPedidoItemsAlunoMatriculado(Guid id)
        {
            var pedido= _mapper.Map<PedidoViewModel>(await _pedidoRepository.ObterPedidoPedidoItemsAlunoMatriculado(id));
            pedido.PedidoItems = _mapper.Map<IEnumerable<PedidoItemViewModel>>(await _pedidoItemsRepository.ObterPedidoItemsPedido(id));
            return pedido;
        }
     

        [HttpGet]
        public JsonResult getPrecoProdutoUnitario(Guid produtoId)
        {
            //var produto = new Produto();
            decimal PrecoUnit = 0;
            PrecoUnit = _context.Produtos.Single(model => model.Id == produtoId).ValorVenda;

            return Json(PrecoUnit);
        }
        public IActionResult GetPrecoProdutoUnitario(Guid produtoId)
        {
            decimal PrecoUnit = _context.Produtos.Single(model => model.Id == produtoId).ValorVenda;
            return Json(PrecoUnit);
        }
        //public IActionResult GetNifCliente(Guid clienteId)
        //{
        //    string Nif = _context.AlunoMatriculados.Single(model => model.Id == clienteId).NumDocumento;
        //    return Json(Nif);
        //}


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
                return false;
            }
            return true;
        }
        private async Task<bool> ValidarProduto(ProdutoViewModel produto)
        {
            var alunoMatriculado = await _produtoRepository.ObterProdutoPeloCodigo(produto.Codigo);
            if (alunoMatriculado == null)
            {
                TempData["Erro"] = $"Este produto com o código '{produto.Codigo}', não existe!";
                return false;
            }
            return true;
        }
        #endregion

    }
}

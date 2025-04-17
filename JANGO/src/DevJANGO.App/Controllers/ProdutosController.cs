using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.ViewModels;
using AutoMapper;
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Context;
using DevJANGO.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using DevJANGO.App.Extensions;
using DevJANGO.App.Queries;


namespace DevJANGO.App.Controllers
{
    [Authorize]
    public class ProdutosController : BaseController
    {
        #region DECLARAR AS DEPENDENCIA
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoQueries _produtoQueries;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        #endregion

        #region INJECTAR AS DEPENDENCIA VI CONSTRUTOR
        public ProdutosController(IProdutoRepository produtoRepository,
                                  IProdutoQueries produtoQueries, 
                                   IMapper mapper
         ,IProdutoService produtoService
                                ,INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoQueries = produtoQueries;
            _mapper = mapper;
            _produtoService = produtoService;
        }

        #endregion

        #region MÉTODO PARA LISTAR GERAL
        [ClaimsAuthorize("Produtos", "VI")]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index([FromQuery] int ps = 8, [FromQuery] int page = 1, [FromQuery] string q = null)
        {
            var produtos = await _produtoQueries.ObterTodos(ps, page, q);
            ViewBag.Pesquisa = q;
            produtos.ReferenceAction = "lista-de-produtos";

            return View(produtos);
        }
        #endregion

        #region MÉTODO PARA LISTAR INDIVIDUAL

        [ClaimsAuthorize("Produtos", "VI")]
        [Route("dados-do-produto/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        #endregion

        #region MÉTODO PARA CADASTRAR NOVO

        [ClaimsAuthorize("Produtos", "AD")]
        [Route("criar-novo")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Produtos", "AD")]
        [HttpPost]
        [Route("criar-novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {

            if (!ModelState.IsValid) return View(produtoViewModel);

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));
            //await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }
        #endregion

        #region MÉTODO PARA EDITAR
        [ClaimsAuthorize("Produtos", "AD")]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Produtos", "AD")]
        [Route("editar/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);
      

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
 
            produtoAtualizacao.ValorVenda = produtoViewModel.ValorVenda;
            produtoAtualizacao.QuantidadeEstoque = produtoViewModel.QuantidadeEstoque;

            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));
            //await _produtoService.Atualizar(cliente);

            // if (!OperacaoValida()) return View(await ObterFornecedorProdutos(id));

            return RedirectToAction("Index");
        }

        #endregion

        #region MÉTODO PARA EXCLUIR

        [ClaimsAuthorize("Produtos", "DG")]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [ClaimsAuthorize("Produtos", "DG")]
        [Route("excluir/{id:guid}")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);

            if (produto == null) return NotFound();

            //await _produtoService.Remover(id);
            await _produtoRepository.Remover(id);

            //if (!OperacaoValida()) return View(produto);
            TempData["Sucesso"] = "Produto excluido com sucesso!";
            return RedirectToAction("Index");
        }

        #endregion

        #region METODOS PRIVADOS DA CONTROLLER
        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));

            return produto;
        }





        #endregion

    }
}

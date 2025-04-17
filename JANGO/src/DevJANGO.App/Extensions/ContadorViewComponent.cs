using DevJANGO.App.Queries;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Extensions
{
    public class ContadorViewComponent : ViewComponent
    {
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;
        private readonly IAlunoInscritoQueries _alunoInscritoQueries;

        public ContadorViewComponent(IAlunoInscritoRepository alunoInscritoRepository,
            IAlunoInscritoQueries alunoInscritoQueries)
        {
            _alunoInscritoRepository = alunoInscritoRepository;
            _alunoInscritoQueries = alunoInscritoQueries;
        }

        public async Task<IViewComponentResult> InvokeAsync(int modeloContador)
        //public IViewComponentResult Invoke(IPagedList modeloPaginado)
        {


            return View(await _alunoInscritoRepository.ObterAlunoInscrito());
        }
    }
}
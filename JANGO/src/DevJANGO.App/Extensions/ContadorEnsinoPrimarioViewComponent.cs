using DevJANGO.App.Queries;
using DevJANGO.App.ViewModels;
using DevJANGO.Business.Intefaces;
using DevJANGO.Data.Context;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Extensions
{
    public class ContadorEnsinoPrimarioViewComponent : ViewComponent
    {
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;

        public ContadorEnsinoPrimarioViewComponent(
            IAlunoInscritoRepository alunoInscritoRepository)
        {
            _alunoInscritoRepository = alunoInscritoRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync(int modeloContador)
        {
            return View(await _alunoInscritoRepository.ObterAlunoInscrito());
        }
    }
}
using DevJANGO.Business.Intefaces;
using DevJANGO.Business.Models;
using DevJANGO.Data.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevJANGO.App.Areas.Admin.Extensions
{
    public class ContadorIniciacaoViewComponent : ViewComponent
    {
        private readonly JangoDbContext _context;
        private readonly IAlunoInscritoRepository _alunoInscritoRepository;

        public ContadorIniciacaoViewComponent(
            JangoDbContext context,
            IAlunoInscritoRepository alunoInscritoRepository)
        {
            _alunoInscritoRepository = alunoInscritoRepository;
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            //var modeloIniciacao1 = await _context.AlunoInscritos.FirstOrDefaultAsync(c => c.Classe.Nome == nomeClasse && c.Sexo == sexo);
            var modeloIniciacao = await _alunoInscritoRepository.ObterAlunoInscrito();
            return View(modeloIniciacao);
        }
    }
}
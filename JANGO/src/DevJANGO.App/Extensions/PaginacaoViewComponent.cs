using DevJANGO.App.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Extensions
{
    public class PaginacaoViewComponent : ViewComponent
    {

        //public async Task<IViewComponentResult>InvokeAsync(IPagedList modeloPaginado)
        public IViewComponentResult Invoke(IPagedList modeloPaginado)
        {
            return View(modeloPaginado);
        }
    }
}
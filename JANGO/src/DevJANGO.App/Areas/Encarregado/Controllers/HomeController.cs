using Microsoft.AspNetCore.Mvc;


namespace DevJANGO.App.Areas.Encarregado.Controllers
{
    [Area("Encarregado")]
    public class HomeController : Controller
    {
        public HomeController()
        {
          
        }

        [Route("area-do-encarregado")]
        public IActionResult Index()
        {
            return View();
        }


    }
}



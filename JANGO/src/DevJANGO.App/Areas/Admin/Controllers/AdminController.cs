using DevJANGO.App.Controllers;
using DevJANGO.App.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevJANGO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminController : Controller
    //public class AdminController : BaseController
    {
        [ClaimsAuthorize("Administrador", "ADM")]
        public IActionResult Index()
        {
            return View();
        }
    }
}

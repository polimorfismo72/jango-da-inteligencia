using DevJANGO.App.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace DevJANGO.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly UserManager<IdentityUser> _userManager;

        public HomeController(RoleManager<IdentityRole> roleManager
            //UserManager<IdentityUser> userManager
            )
        {
            _roleManager = roleManager;
            //_userManager = userManager;
        }

        [ClaimsAuthorize("Administrador", "ADM")]
        public ViewResult Index() => View(_roleManager.Roles);

        [ClaimsAuthorize("Administrador", "ADM")]
        public IActionResult Create() => View();
                                                                                                                                                                                                                                                   
        [HttpPost]
        [ClaimsAuthorize("Administrador", "ADM")]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using DevJANGO.App.Areas.Admin.Models;
using System.Security.Claims;
using DevJANGO.App.Extensions;

namespace DevJANGO.App.Areas.Admin.Controllers;

[Area("Admin")]
//[Authorize(Roles = "Admin")]
[Authorize]
public class AdminClaimsController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminClaimsController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }
    [ClaimsAuthorize("Administrador", "ADM")]
    [Route("lista-de-usuario")]
    public IActionResult Index()
    {
        var users = _userManager.Users;
        return View(users);
    }

    [HttpGet]
    [ClaimsAuthorize("Administrador", "ADM")]
    [Route("editar-usuario/{id:guid}")]
    public async Task<IActionResult> EditUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            ModelState.AddModelError("", "Usuário não encontrado");
            return View();
        }

        var userClaims = await _userManager.GetClaimsAsync(user);

        var model = new EditUserViewModel
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            Claims = userClaims.ToList(),
        };
        return View(model);
    }

    [HttpPost]
    [ClaimsAuthorize("Administrador", "ADM")]
    [Route("editar-usuario/{id:guid}")]
    public async Task<IActionResult> EditUser(EditUserViewModel model)
    {
        var user = await _userManager.FindByIdAsync(model.Id);

        if (user is null)
        {
            ModelState.AddModelError("", "Usuário não encontrado");
            return View(model);
        }
        else
        {
            user.Email = model.Email;
            user.UserName = model.UserName;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }
    }

    [HttpGet]
    [ClaimsAuthorize("Administrador", "ADM")]
    [Route("novo-usuario")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Create")]
    [ClaimsAuthorize("Administrador", "ADM")]
    [Route("novo-usuario")]
    public async Task<IActionResult> Create_Post(string claimType, string claimValue,
                                                  string userId)

    {
        if (claimType is null || claimValue is null)
        {
            ModelState.AddModelError("", "Tipo/Valor da Claim deve ser informado");
            return View();
        }

        IdentityUser user = await _userManager.FindByIdAsync(userId);

        if (user is not null)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);

            Claim claim = userClaims.FirstOrDefault(x => x.Type.Equals(claimType)
                          && x.Value.Equals(claimValue));

            if (claim is null)
            {
                Claim newClaim = new(claimType, claimValue);
                //Claim newClaim = new Claim(claimType, claimValue);

                IdentityResult result = await _userManager.AddClaimAsync(user, newClaim);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
            {
                ModelState.AddModelError("", "Claim já existente");
            }
        }
        else
        {
            ModelState.AddModelError("", "Usuário não encontrado");
        }
        return View();
    }

    [HttpPost]
    [ClaimsAuthorize("Administrador", "ADM")]
    //[Route("excluir-usuario/{id:guid}")]
    public async Task<IActionResult> DeleteClaim(string claimValues)
    {
        string[] claimValuesArray = claimValues.Split(";");
        string claimType = claimValuesArray[0].ToString();
        string claimValue = claimValuesArray[1].ToString();
        string userId = claimValuesArray[2].ToString();

        IdentityUser user = await _userManager.FindByIdAsync(userId);

        if (user is not null)
        {

            var userClaims = await _userManager.GetClaimsAsync(user);

            Claim claim = userClaims.FirstOrDefault(x => x.Type.Equals(claimType)
                          && x.Value.Equals(claimValue));

            IdentityResult result = await _userManager.RemoveClaimAsync(user, claim);

            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
        }
        else
        {
            ModelState.AddModelError("", "Usuário não encontrado");
        }

        return View("Index");
    }

    void Errors(IdentityResult result)
    {
        foreach (IdentityError error in result.Errors)
            ModelState.AddModelError("", error.Description);
    }
}

using System.Security.Claims;
using FamilyTree.ViewModels;
using FTEntities.IdentityModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FamilyTree.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        User user = new User { Email = model.Email, UserName = model.Email};

        var result = await userManager.CreateAsync(user, model.Password);
        await userManager.AddClaimAsync(user, new Claim("UserId", user.Id));
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        return View(model);
    }
    
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        return View(new LoginVM { ReturnUrl = returnUrl });
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Start", "Home");
    }
}
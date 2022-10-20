using System.Security.Claims;
using FamilyTree.ViewModels;
using FTEntities.IdentityModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        if (result.Succeeded)
        {
            await userManager.AddClaimAsync(user, new Claim("UserId", user.Id));
            await signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Login()
    {
        LoginVM model = new LoginVM()
        {
            ExternalLogins = await signInManager.GetExternalAuthenticationSchemesAsync()
        };
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        
        if (result.Succeeded)
            return RedirectToAction("Index", "Home");

        model.ExternalLogins = await signInManager.GetExternalAuthenticationSchemesAsync();
        return View(model);
    }
    
    [HttpPost]
    public IActionResult ExternalLogin(string provider)
    {
        var redirectUrl = Url.Action("ExternalLoginCallback", "Account");

        var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);

        return new ChallengeResult(provider, properties);
    }

    public async Task<IActionResult> ExternalLoginCallback()
    {

        var info = await signInManager.GetExternalLoginInfoAsync();

        var signInResult = await signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
            return RedirectToAction("Index", "Home");
        
        
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);

        if (email != null)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user == null)
            {
                user = new User
                {
                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                };

                await userManager.CreateAsync(user);
            }

            await userManager.AddClaimAsync(user, new Claim("UserId", user.Id));
            await userManager.AddLoginAsync(user, info);
            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        return NotFound();    
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return RedirectToAction("Start", "Home");
    }
}
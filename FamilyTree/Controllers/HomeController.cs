using System.Net;
using System.Security.Claims;
using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
using FTEntities.IdentityModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly SignInManager<User> signInManager;
    public HomeController( SignInManager<User> signInManager) => this.signInManager = signInManager;
    
    [AllowAnonymous] 
    [HttpGet]
    public IActionResult Start()
    {
        if (signInManager.IsSignedIn(User))
            return RedirectToAction("Index");
        return View();
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
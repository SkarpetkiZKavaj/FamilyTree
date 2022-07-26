using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}
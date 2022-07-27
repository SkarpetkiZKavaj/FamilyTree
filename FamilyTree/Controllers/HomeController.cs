using Microsoft.AspNetCore.Mvc;
using FamilyTree.Models;

namespace FamilyTree.Controllers;

public class HomeController : Controller
{
    private PersonContext db;

    public HomeController(PersonContext context)
    {
        db = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(db.Persons.ToList());
    }

    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
        db.Persons.Add(person);
        db.SaveChanges();

        return RedirectToAction("Index");
    }
}
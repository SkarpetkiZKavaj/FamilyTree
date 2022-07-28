using Microsoft.AspNetCore.Mvc;
using FamilyTree.Models;

namespace FamilyTree.Controllers;

public class HomeController : Controller
{
    private PersonContext db;

    public HomeController(PersonContext context) => db = context;

    [HttpGet]
    public IActionResult Index()
    {
        return View(db.Persons.ToList());
    }

    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
        Description desc = new Description();
        person.Description = desc;
        desc.Person = person;
        db.Persons.Add(person);
        db.Descriptions.Add(desc);
        db.SaveChanges();

        return RedirectToAction("Index");
    }
}
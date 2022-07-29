using FamilyTree.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree.Controllers;

public class DescriptionController : Controller
{
    private PersonContext db;

    public DescriptionController(PersonContext context) => db = context;

    [HttpGet]
    public IActionResult Index(int personId)
    {
        var persons = db.Persons.Include(p => p.Description).ToList();
        Person person = persons.Find(p => p.Id == personId);
        return View(person);
    }

    [HttpPost]
    public IActionResult AddHistory(int personId, Description description)
    {
        var persons = db.Persons.Include(p => p.Description).ToList();
        Person person = persons.Find(p => p.Id == personId);
        person.Description.History = description.History;
        db.SaveChanges();
        return RedirectToAction("Index", new {personId});
    }
}
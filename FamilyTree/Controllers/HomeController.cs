using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interfaces;
using FamilyTree_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

public class HomeController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<Person, PersonVM>();
        cfg.CreateMap<Description, DescriptionVM>();
    }).CreateMapper();
    
    private IPersonService service;
    
    public HomeController(IPersonService service) => this.service = service;

    [HttpGet]
    public IActionResult Index()
    {
        var personDTO = service.GetPersons();
        var persons = mapper.Map<IEnumerable<PersonDTO>, IEnumerable<PersonVM>>(personDTO);
        return View(persons.ToList());
    }

    [HttpPost]
    public IActionResult AddPerson(Person person)
    {
        Description desc = new Description();
        person.Description = desc;
        desc.Person = person;

        var personDTO = mapper.Map<Person, PersonDTO>(person);
        service.AddPerson(personDTO);
        
        return RedirectToAction("Index");
    }
}
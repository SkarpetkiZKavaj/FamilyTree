using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

public class HomeController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, DescriptionVM>().ReverseMap();
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
    public IActionResult AddPerson(PersonVM person)
    {
        DescriptionVM desc = new DescriptionVM();
        person.Description = desc;

        var personDTO = mapper.Map<PersonVM, PersonDTO>(person);
        service.AddPerson(personDTO);
        
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int personId)
    {
        service.DeletePerson(personId);
        
        return RedirectToAction("Index");
    }
}
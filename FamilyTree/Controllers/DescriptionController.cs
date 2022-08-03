using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interfaces;
using FamilyTree_DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

public class DescriptionController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<Person, PersonVM>();
        cfg.CreateMap<Description, DescriptionVM>();
    }).CreateMapper();
    
    private IPersonService service;
    
    public DescriptionController(IPersonService service) => this.service = service;

    [HttpGet]
    public IActionResult Index(int personId)
    {
        var person = mapper.Map<PersonDTO, PersonVM>(service.GetPerson(personId));

        if (person is null)
            return NotFound();
        
        return View(person);
    }

    [HttpPost]
    public IActionResult AddHistory(int personId, Description description)
    {
        var person = mapper.Map<PersonDTO, PersonVM>(service.GetPerson(personId));
        person.Description.History = description.History;
        service.UpdatePerson(mapper.Map<PersonVM, PersonDTO>(person));
        return RedirectToAction("Index", new {personId});
    }
}
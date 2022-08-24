using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

[Authorize]
public class PersonController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, DescriptionVM>().ReverseMap();
        cfg.CreateMap<TreeDTO, TreeVM>().ReverseMap();
    }).CreateMapper();
    
    private IServiceHub hub;

    public PersonController(IServiceHub hub) => this.hub = hub;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddPerson(PersonVM person)
    {
        string? ownerId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;

        if (ownerId is null)
            return Unauthorized();

        person.OwnerId = ownerId;
        person.Description = new DescriptionVM() { OwnerId = ownerId };
            
        var personDTO = mapper.Map<PersonVM, PersonDTO>(person);
        hub.PersonService.Add(personDTO);
        hub.Save();
        
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int personId)
    {
        hub.PersonService.Delete(personId);
        hub.Save();
        
        return RedirectToAction("Index", "Home");
    }
}
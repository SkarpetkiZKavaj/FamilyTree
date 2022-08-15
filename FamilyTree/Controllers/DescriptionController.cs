using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

[Authorize]
public class DescriptionController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, DescriptionVM>().ReverseMap();
        cfg.CreateMap<TreeDTO, TreeVM>().ReverseMap();
    }).CreateMapper();
    
    private IServiceHub hub;

    public DescriptionController(IServiceHub hub) => this.hub = hub;
    

    [HttpGet]
    public IActionResult Index(int personId)
    {
        var person = mapper.
            Map<PersonDTO, PersonVM>(hub.PersonService.
            Get((p => p.Id == personId), null, "Description").
            FirstOrDefault());
    
        if (person is null)
            return NotFound();
        
        return View(person);
    }
    
    [HttpPost]
    public IActionResult ChangeInformation(int personId, PersonVM person)
    {
        var personDTO = hub.PersonService.Get((p => p.Id == personId), null, "Description").FirstOrDefault();

        if (personDTO is null)
            return NotFound();
        
        personDTO.Age = person.Age;
        personDTO.FirstName = person.FirstName;
        personDTO.LastName = person.LastName;
        personDTO.Description.History = person.Description.History;
        
        hub.PersonService.Update(personDTO);
        hub.Save();
        return RedirectToAction("Index", new {personId});
    }
}
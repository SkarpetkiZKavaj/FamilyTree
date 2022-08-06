using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

public class DescriptionController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, DescriptionVM>().ReverseMap();
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
    public IActionResult ChangeInformation(int personId, int descId, PersonVM person)
    {
        var personDTO = mapper.Map<PersonVM, PersonDTO>(person);
        personDTO.Id = personId;
        personDTO.Description.Id = descId;
        service.UpdatePerson(personDTO);
        return RedirectToAction("Index", new {personId});
    }
}
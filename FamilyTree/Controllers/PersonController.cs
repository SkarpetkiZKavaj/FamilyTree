using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.Helper.Validation;
using FamilyTree.ViewModels;
using FluentValidation;
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
    private IValidator<PersonDTO> validator; 

    public PersonController(IServiceHub hub, IValidator<PersonDTO> validator)
    {
        this.validator = validator;
        this.hub = hub;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddPerson(PersonVM person)
    {
        string? ownerId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;

        person.OwnerId = ownerId;
        person.Description = new DescriptionVM() { OwnerId = ownerId };
            
        var personDTO = mapper.Map<PersonVM, PersonDTO>(person);

        var validationResult = validator.Validate(personDTO);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);

            return View("Index", person);
        }
        
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
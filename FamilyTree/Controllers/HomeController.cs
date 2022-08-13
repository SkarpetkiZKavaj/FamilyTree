using System.Security.Claims;
using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interfaces;
using FTEntities.IdentityModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, DescriptionVM>().ReverseMap();
    }).CreateMapper();
    
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private IPersonService service;
    
    public HomeController(IPersonService service, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.service = service;
    }

    [AllowAnonymous] 
    [HttpGet]
    public IActionResult Start()
    {
        if (signInManager.IsSignedIn(User))
            return RedirectToAction("Index");
        return View();
    }

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

    [HttpPost]
    public IActionResult Delete(int personId)
    {
        service.DeletePerson(personId);
        
        return RedirectToAction("Index");
    }
}
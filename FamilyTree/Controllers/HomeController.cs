using System.Net;
using System.Security.Claims;
using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
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
        cfg.CreateMap<TreeDTO, TreeVM>().ReverseMap();
    }).CreateMapper();
    
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;
    private IServiceHub hub;

    public HomeController(IServiceHub hub, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.hub = hub;
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
        var personsDTO = hub.PersonService.Get();
        var persons = mapper.Map<IEnumerable<PersonDTO>, IEnumerable<PersonVM>>(personsDTO);
        return View(persons.ToList());
    }

    [HttpPost]
    public IActionResult AddPerson(PersonVM person)
    {
        DescriptionVM desc = new DescriptionVM();
        person.Description = desc;
        person.Description.OwnerId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
        person.OwnerId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;

        var personDTO = mapper.Map<PersonVM, PersonDTO>(person);
        hub.PersonService.Add(personDTO);
        hub.Save();
        
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Delete(int personId)
    {
        hub.PersonService.Delete(personId);
        hub.Save();
        
        return RedirectToAction("Index");
    }
}
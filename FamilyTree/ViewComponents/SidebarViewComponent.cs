using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
using FTEntities.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

[Authorize]
public class SidebarViewComponent : ViewComponent
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
    }).CreateMapper();
    
    private IServiceHub hub;

    public SidebarViewComponent(IServiceHub hub) => this.hub = hub;

    public IViewComponentResult Invoke()
    {
        var persons = mapper.Map<IEnumerable<PersonDTO>, IEnumerable<PersonVM>>(hub.PersonService.Get()).ToList();
        
        return View(persons);
    }
}
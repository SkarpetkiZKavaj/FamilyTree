using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

public class SlidebarViewComponent : ViewComponent
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<TreeDTO, TreeVM>().ReverseMap();
    }).CreateMapper();
    
    private IServiceHub hub;

    public SlidebarViewComponent(IServiceHub hub) => this.hub = hub;    
    
    public IViewComponentResult Invoke()
    {
        var trees = mapper.Map<IEnumerable<TreeDTO>, IEnumerable<TreeVM>>(hub.TreeService.Get()).ToList();
        
        return View(trees);
    }
}
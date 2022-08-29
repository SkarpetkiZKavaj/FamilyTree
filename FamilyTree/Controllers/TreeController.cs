using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FamilyTree.Controllers;

[Authorize]
public class TreeController : Controller
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, PersonVM>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, DescriptionVM>().ReverseMap();
        cfg.CreateMap<TreeDTO, TreeVM>().ReverseMap();
    }).CreateMapper();
    
    private IServiceHub hub;

    public TreeController(IServiceHub hub)
    {
        this.hub = hub;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult GetTree(int treeId)
    {
        var treeDTO = hub.TreeService.Get((t => t.Id == treeId), null, "Members").FirstOrDefault();
        
        if (treeDTO is null)
            return NotFound();
        
        var treeVM = mapper.Map<TreeDTO, TreeVM>(treeDTO);
        
        return View("Tree", treeVM);
    }

    [HttpPost]
    public IActionResult AddTree(TreeVM tree)
    {
        string? ownerId = User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
        
        if (ownerId is null)
            return Unauthorized();

        tree.OwnerId = ownerId;

        var treeDTO = mapper.Map<TreeVM, TreeDTO>(tree);
        hub.TreeService.Add(treeDTO);
        hub.Save();
        
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult Update(int treeId, TreeVM treeVM)
    {
        var treeDTO = hub.TreeService.Get((p => p.Id == treeId), null, "Members").FirstOrDefault();

        if (treeDTO is null)
            return NotFound();

        treeDTO.Name = treeVM.Name;
        treeDTO.Members = mapper.Map<ICollection<PersonVM>, ICollection<PersonDTO>>(treeVM.Members);
        
        hub.TreeService.Update(treeDTO);
        hub.Save();
        return RedirectToAction("GetTree",  new{treeId});
    }

    [HttpGet]
    public IActionResult Delete(int treeId)
    {
        hub.TreeService.Delete(treeId);
        hub.Save();
        
        return RedirectToAction("Index", "Home");
    }
}
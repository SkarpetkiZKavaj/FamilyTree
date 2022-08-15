using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Services;
using FamilyTree_DAL.Models;
using FTEntities.Models.Tree;

namespace FamilyTree_BAL.Interface;

public interface IServiceHub
{
    public GenericService<Person, PersonDTO> PersonService { get; }
    
    public GenericService<Tree, TreeDTO> TreeService { get; }
    
    public GenericService<Description, DescriptionDTO> DescriptionService { get; }
    
    public void Save();
    public void Dispose();
}
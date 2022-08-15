using System.Xml.Linq;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interface;
using FamilyTree_DAL.Models;
using FTEntities.Interface;
using FTEntities.Models.Tree;

namespace FamilyTree_BAL.Services;

public class ServiceHub : IDisposable, IServiceHub
{
    private IUnitOfWork unitOfWork;
    private GenericService<Person, PersonDTO> personService;
    private GenericService<Tree, TreeDTO> treeService;
    private GenericService<Description, DescriptionDTO> descriptionService;

    public ServiceHub(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;
    
    public GenericService<Person, PersonDTO> PersonService 
    {
        get
        {
            if (personService is null)
                personService = new GenericService<Person, PersonDTO>(this.unitOfWork.PersonRepository);

            return personService;
        }
    }
    
    public GenericService<Tree, TreeDTO> TreeService 
    {
        get
        {
            if(treeService is null)
                treeService = new GenericService<Tree, TreeDTO>(this.unitOfWork.TreeRepository);

            return treeService;
        }
    }

    public GenericService<Description, DescriptionDTO> DescriptionService
    {
        get
        {
            if(descriptionService is null)
                descriptionService = new GenericService<Description, DescriptionDTO>(this.unitOfWork.DescriptionRepository);

            return descriptionService;
        }
    }
    
    public void Save() => unitOfWork.Save();

    public void Dispose() => unitOfWork.Dispose();
}
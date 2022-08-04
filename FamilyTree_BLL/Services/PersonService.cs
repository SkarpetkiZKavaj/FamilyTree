using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_BAL.Interfaces;
using FamilyTree_DAL.Models;
using FTEntities.Interfaces;

namespace FamilyTree_BAL.Services;

public class PersonService : IPersonService
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<Person, PersonDTO>().ReverseMap();
        cfg.CreateMap<Description, DescriptionDTO>().ReverseMap();
    }).CreateMapper();
    
    private IRepository Database { get; set; }

    public PersonService(IRepository repository) => Database = repository;

    public IEnumerable<PersonDTO> GetPersons() => mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(Database.GetAll());

    public PersonDTO GetPerson(int personId) => mapper.Map<Person, PersonDTO>(Database.GetById(personId));

    public void UpdatePerson(PersonDTO person)
    {
        Database.Update(mapper.Map<PersonDTO, Person>(person));
        Database.Save();
    }

    public void DeletePerson(int personId)
    {
        Database.Delete(personId);
        Database.Save();
    }

    public void AddPerson(PersonDTO person)
    {
        Database.Create(mapper.Map<PersonDTO, Person>(person));
        Database.Save();
    }
}
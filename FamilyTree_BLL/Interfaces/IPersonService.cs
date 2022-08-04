using FamilyTree_BAL.DTO;

namespace FamilyTree_BAL.Interfaces;

public interface IPersonService
{
    public IEnumerable<PersonDTO> GetPersons();
    public PersonDTO GetPerson(int personId);
    public void UpdatePerson(PersonDTO person);
    public void DeletePerson(int peronId);
    public void AddPerson(PersonDTO person);
}
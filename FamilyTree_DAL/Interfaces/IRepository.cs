using FamilyTree_DAL.Models;

namespace FTEntities.Interfaces;

public interface IRepository
{
    IEnumerable<Person> GetAll();
    Person GetById(int id);
    void Create(Person item);
    void Update(Person item);
    void Delete(int id);

    void Save();
}
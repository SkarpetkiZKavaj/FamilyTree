using FamilyTree_DAL.EF;
using FTEntities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree_DAL.Models;

public class PersonRepository : IRepository
{
    private PersonContext context;

    public PersonRepository(PersonContext context)
    {
        this.context = context;
    }

    public IEnumerable<Person> GetAll() => context.Persons.Include(p => p.Description).AsNoTracking();

    public Person GetById(int id) => context.Persons.Include(p => p.Description).AsNoTracking().FirstOrDefault(p => p.Id == id);

    public void Create(Person person)
    {
        context.Persons.Add(person);
        context.Descriptions.Add(person.Description);
    }
    
    public void Update(Person person)
    {
        context.Update(person);
    }

    public void Delete(int id)
    {
        Person person = context.Persons.Find(id);
        if (person is not null)
            context.Persons.Remove(person);
    }

    public void Save() => context.SaveChanges();
}
using System.Security.Claims;
using FamilyTree_DAL.EF;
using FTEntities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree_DAL.Models;

public class PersonRepository : IRepository
{
    private PersonContext context;
    private string userId;

    public PersonRepository(PersonContext context, IHttpContextAccessor accessor)
    {
        
        this.context = context;
        userId = accessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value ;
    }

    public IEnumerable<Person> GetAll() => context.Persons.Include(p => p.Description).Where(p => p.OwnerId == userId).AsNoTracking();

    public Person GetById(int id) => context.Persons.Include(p => p.Description).Where(p => p.OwnerId == userId).AsNoTracking().FirstOrDefault(p => p.Id == id);

    public void Create(Person person)
    {
        person.OwnerId = userId;
        context.Persons.Add(person);
        context.Descriptions.Add(person.Description);
    }
    
    public void Update(Person person)
    {
        person.OwnerId = userId;
        context.Update(person);
    }

    public void Delete(int id)
    {
        Person person = context.Persons.Where(p => p.OwnerId == userId).FirstOrDefault(p => p.Id == id);
        if (person is not null)
            context.Persons.Remove(person);
    }

    public void Save() => context.SaveChanges();
}
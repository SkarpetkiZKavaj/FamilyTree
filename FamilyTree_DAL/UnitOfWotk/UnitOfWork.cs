using FamilyTree_DAL.EF;
using FamilyTree_DAL.Models;
using FTEntities.Models.Tree;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace FTEntities.UnitOfWotk;

public class UnitOfWork : IDisposable
{
    private PersonContext context;
    private string ownerId;
    private bool disposed = false;
    private GenericRepository<Tree> treeRepository;
    private GenericRepository<Person> personRepository;
    private GenericRepository<Description> descriptionRepository;

    public UnitOfWork(PersonContext context, IHttpContextAccessor accessor)
    {
        this.context = context;
        ownerId = accessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == "UserId")?.Value;
    }

    public GenericRepository<Tree> TreeRepository
    {
        get
        {
            if (treeRepository == null)
                treeRepository = new GenericRepository<Tree>(context.Set<Tree>().Where(t => t.OwnerId == ownerId) as DbSet<Tree>);
 
            return treeRepository;
        }
    }
    
    public GenericRepository<Person> PersonRepository
    {
        get
        {
            if (personRepository == null)
                personRepository = new GenericRepository<Person>(context.Set<Person>().Where(p => p.OwnerId == ownerId) as DbSet<Person>);
            
            return personRepository;
        }
    }
    
    public GenericRepository<Description> DescriptionRepository
    {
        get
        {
            if (descriptionRepository == null)
                descriptionRepository = new GenericRepository<Description>(context.Set<Description>().Where(d => d.OwnerId == ownerId) as DbSet<Description>);
            
            return descriptionRepository;
        }
    }
    
    public void Save() => context.SaveChanges();
    
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
            if (disposing)
                context.Dispose();
        
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
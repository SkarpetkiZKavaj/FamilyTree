using FamilyTree_DAL.EF;
using FamilyTree_DAL.Models;
using FTEntities.Interface;
using FTEntities.Models.Tree;
using Microsoft.EntityFrameworkCore;

namespace FTEntities.UnitOfWotk;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private PersonContext context;
    private string ownerId;
    private bool disposed = false;
    private GenericRepository<Tree> treeRepository;
    private GenericRepository<Person> personRepository;
    private GenericRepository<Description> descriptionRepository;

    public UnitOfWork(PersonContext context) => this.context = context;
    

    public GenericRepository<Tree> TreeRepository
    {
        get
        {
            if (treeRepository is null)
                treeRepository = new GenericRepository<Tree>(context.Set<Tree>());
 
            return treeRepository;
        }
    }
    
    public GenericRepository<Person> PersonRepository
    {
        get
        {
            if (personRepository is null)
                personRepository = new GenericRepository<Person>(context.Set<Person>());
            
            return personRepository;
        }
    }
    
    public GenericRepository<Description> DescriptionRepository
    {
        get
        {
            if (descriptionRepository is null)
                descriptionRepository = new GenericRepository<Description>(context.Set<Description>());
            
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
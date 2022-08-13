using FamilyTree_DAL.EF;
using FamilyTree_DAL.Models;
using FTEntities.Models.Tree;

namespace FTEntities.UnitOfWotk;

public class UnitOfWork : IDisposable
{
    private PersonContext context;
    private bool disposed = false;
    private GenericRepository<Tree> treeRepository;
    private GenericRepository<Person> personRepository;
    private GenericRepository<Description> descriptionRepository;

    public UnitOfWork(PersonContext context) => this.context = context;
    
    public GenericRepository<Tree> TreeRepository
    {
        get
        {
            if (treeRepository == null)
                treeRepository = new GenericRepository<Tree>(context);
            
            return treeRepository;
        }
    }
    
    public GenericRepository<Person> PersonRepository
    {
        get
        {
            if (personRepository == null)
                personRepository = new GenericRepository<Person>(context);
            
            return personRepository;
        }
    }
    
    public GenericRepository<Description> DescriptionRepository
    {
        get
        {
            if (descriptionRepository == null)
                descriptionRepository = new GenericRepository<Description>(context);
            
            return descriptionRepository;
        }
    }
    
    public void Save() => context.SaveChanges();
    
    protected virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
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
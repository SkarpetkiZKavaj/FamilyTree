using FamilyTree_DAL.Models;
using FTEntities.Models.Tree;
using Microsoft.EntityFrameworkCore;

namespace FTEntities.Interface;

public interface IUnitOfWork
{
    public GenericRepository<Tree> TreeRepository { get; }

    public GenericRepository<Person> PersonRepository { get; }
    
    public GenericRepository<Description> DescriptionRepository { get; }
    
    public void Save();

    public void Dispose();
}
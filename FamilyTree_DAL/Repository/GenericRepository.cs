using System.Linq.Expressions;
using System.Security.Claims;
using FamilyTree_DAL.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FamilyTree_DAL.Models;

public class GenericRepository<T> where T : class 
{
    private DbSet<T> dbSet;
    private bool disposed = false;

    public GenericRepository(DbSet<T> dbSet) => this.dbSet = dbSet;
    
    
    public virtual IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = dbSet;

        if (filter != null)
            query = query.Where(filter);

        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        if (orderBy != null)
            return orderBy(query).ToList();
        else
            return query.ToList();
    }

    public T GetById(object id) => dbSet.Find(id);
    

    public void Create(T entity) => dbSet.Add(entity);
    
    
    public void Update(T entity) => dbSet.Update(entity);
    

    public void Delete(object id)
    {
        T entity = dbSet.Find(id);
        if (entity is not null)
            dbSet.Remove(entity);
    }
}
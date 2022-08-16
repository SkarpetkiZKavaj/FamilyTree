using System.Linq.Expressions;
using AutoMapper;
using FamilyTree_BAL.DTO;
using FamilyTree_DAL.Models;
using FTEntities.Models.Tree;

namespace FamilyTree_BAL.Services;

public class GenericService<T, TDTO> where T : class
{
    private readonly IMapper mapper = new MapperConfiguration(cfg => {
        cfg.CreateMap<PersonDTO, Person>().ReverseMap();
        cfg.CreateMap<DescriptionDTO, Description>().ReverseMap();
        cfg.CreateMap<TreeDTO, Tree>().ReverseMap();
    }).CreateMapper();
    
    private GenericRepository<T> Database { get; set; }

    public GenericService(GenericRepository<T> repository) => Database = repository;

    public IEnumerable<TDTO> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "") => 
        mapper.Map<IEnumerable<T>, IEnumerable<TDTO>>(Database.Get(filter, orderBy, includeProperties));

    public TDTO GetById(int entityId) => mapper.Map<T, TDTO>(Database.GetById(entityId));

    public void Update(TDTO entity) => Database.Update(mapper.Map<TDTO, T>(entity));

    public void Delete(int entityId) => Database.Delete(entityId);

    public void Add(TDTO entity) => Database.Create(mapper.Map<TDTO, T>(entity));
    
}
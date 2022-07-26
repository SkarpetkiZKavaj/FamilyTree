using FamilyTree_DAL.Models;

namespace FTEntities.Models.Tree;

public class Tree 
{
    public int Id { get; set; }

    public string OwnerId { get; set; }
    
    public string? Name { get; set; }

    public ICollection<Person> Members { get; set; }
}
using FamilyTree_DAL.Models;

namespace FTEntities.Models.Tree;

public class Tree 
{
    public int Id { get; set; }

    public int Name { get; set; }

    public ICollection<Person> Members { get; set; }
}
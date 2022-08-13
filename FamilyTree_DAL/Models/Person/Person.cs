using System.ComponentModel.DataAnnotations;
using FTEntities.Models.Tree;

namespace FamilyTree_DAL.Models;

public class Person
{
    public int Id { get; set; }

    public string OwnerId { get; set; }

    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public int Age { get; set; }

    public Description? Description { get; set; }

    public ICollection<Tree> Trees { get; set; }
}

    
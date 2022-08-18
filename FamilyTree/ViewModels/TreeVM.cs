namespace FamilyTree.ViewModels;

public class TreeVM
{
    public int Id { get; set; }
    
    public string OwnerId { get; set; }

    public string Name { get; set; }

    public ICollection<PersonVM> Members { get; set; }
}
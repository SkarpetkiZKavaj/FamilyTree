using FamilyTree_BAL.DTO;
using FamilyTree.ViewModels;

public class PersonVM
{
    public int Id { get; set; }
    public string OwnerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DescriptionVM Description { get; set; } 
    public ICollection<TreeVM> Trees { get; set; }
}
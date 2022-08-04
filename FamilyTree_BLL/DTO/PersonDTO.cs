namespace FamilyTree_BAL.DTO;

public class PersonDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DescriptionDTO Description { get; set; } 
}
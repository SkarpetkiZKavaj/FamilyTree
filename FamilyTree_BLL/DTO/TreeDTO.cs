using FamilyTree_DAL.Models;

namespace FamilyTree_BAL.DTO;

public class TreeDTO
{
    public int Id { get; set; }
    
    public string OwnerId { get; set; }

    public int Name { get; set; }

    public ICollection<PersonDTO> Members { get; set; }
}
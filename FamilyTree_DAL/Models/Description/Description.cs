using System.ComponentModel.DataAnnotations;

namespace FamilyTree_DAL.Models;

public class Description
{
    public int Id { get; set; }
    
    public string OwnerId { get; set; }
    
    [DataType(DataType.Text)] 
    public string? History { get; set; }

    public int PersonId { get; set; }

    public Person? Person { get; set; }
}
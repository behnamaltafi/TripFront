using System.ComponentModel.DataAnnotations;

public class AddFamilyDTO
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }


    [StringLength(50)]
    public string AccountNumber { get; set; }
    [Required]
    public string UserId { get; set; }
}

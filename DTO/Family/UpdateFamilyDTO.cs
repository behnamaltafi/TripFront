using System.ComponentModel.DataAnnotations;

public class UpdateFamilyDTO
{
    [StringLength(100)]
    public string Name { get; set; }

    [Range(1, 20)]
    public int? MemberCount { get; set; }

    [StringLength(50)]
    public string AccountNumber { get; set; }
}
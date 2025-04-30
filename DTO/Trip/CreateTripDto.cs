
// مدل‌های پایه
using System.ComponentModel.DataAnnotations;

public class CreateTripDto
{

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    public int MemberCount{ get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

}


// مدل‌های پایه
using System.ComponentModel.DataAnnotations;

public class UpdateParticipantDto
{
    [Range(1, int.MaxValue)]
    public int ParticipantCount { get; set; }
}

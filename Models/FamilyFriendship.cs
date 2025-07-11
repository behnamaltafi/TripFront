// مدل‌های پایه
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TripFront.Models;


public class FamilyFriendship : BaseEntity<int>
{
    public int FamilyId { get; set; }
    public int FriendFamilyId { get; set; }

    // Optional: Additional properties
    public DateTime FriendshipDate { get; set; }
    public FriendshipStatus Status { get; set; } // Pending, Accepted, Blocked
    public string RequestMessage { get; set; }

    // Navigation properties
    public Family Family { get; set; }
    public Family FriendFamily { get; set; }
}

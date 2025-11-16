using System.ComponentModel.DataAnnotations;

namespace TripFront.Api.Models;

public class CreateFriendshipRequest
{
    [Range(1, int.MaxValue)]
    public int FamilyId { get; set; }

    [Range(1, int.MaxValue)]
    public int FriendFamilyId { get; set; }

    public FriendshipStatus Status { get; set; } = FriendshipStatus.Pending;

    public DateTime? FriendshipDate { get; set; }

    public string? RequestMessage { get; set; }
}

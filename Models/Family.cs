// مدل‌های پایه

public class Family : BaseEntity<int>
{
    public string Name { get; set; }

    public string AccountNumber { get; set; }
    public string UserId { get; set; }


    public List<TripFamily> TripFamilies { get; set; } = new();

    // Navigation properties
    public List<FriendRequest> SentFriendRequests { get; set; } = new();
    public List<FriendRequest> ReceivedFriendRequests { get; set; } = new();

    public List<FamilyFriendship> FriendshipsA { get; set; } = new();
    public List<FamilyFriendship> FriendshipsB { get; set; } = new();
}

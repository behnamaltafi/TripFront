namespace TripFront.Models
{
    public class Family : BaseEntity<int>
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string UserId { get; set; }
        public string ProfileImage { get; set; }
        public string Bio { get; set; }
        public List<FamilyInterest> FamilyInterests { get; set; } = new();
        public List<TripFamily> TripFamilies { get; set; } = new();
        public List<FamilyFriendship> SentFriendshipRequests { get; set; } = new();
        public List<FamilyFriendship> ReceivedFriendshipRequests { get; set; } = new();
    }

}

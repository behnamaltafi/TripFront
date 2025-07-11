namespace TripFront.Models
{
    public class FamilyInterest : BaseEntity<int>
    {
        public int FamilyId { get; set; }
        public int InterestId { get; set; }
        public Family Family { get; set; }
        public Interest Interest { get; set; }
    }

}

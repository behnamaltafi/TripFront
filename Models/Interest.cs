namespace TripFront.Models
{
    public class Interest : BaseEntity<int>
    {
        public string Title { get; set; }
        public List<FamilyInterest> FamilyInterests { get; set; } = new();
    }

}

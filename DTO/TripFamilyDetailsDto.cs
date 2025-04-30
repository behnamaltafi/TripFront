public class TripFamilyDetailsDto
{
    public int FamilyId { get; set; }
    public string FamilyName { get; set; }
    public int MemberCount { get; set; }
    public string AccountNumber { get; set; }

    // These will be calculated in the service
    public decimal TotalPaid { get; set; }
    public decimal TotalShare { get; set; }
    public decimal Balance { get; set; }
}

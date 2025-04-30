public class FamilyBalanceDto
{
    public int FamilyId { get; set; }
    public string FamilyName { get; set; }
    public int ParticipantCount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalShare { get; set; }
    public decimal Balance { get; set; }
    public string  AccountNumber  { get; set; }
}

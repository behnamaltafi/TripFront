// مدل‌های پایه
public class DebtRecordDto
{
    public int Id { get; set; }
    public int FromFamilyId { get; set; }
    public string FromFamilyName { get; set; }
    public int ToFamilyId { get; set; }
    public string ToFamilyName { get; set; }
    public int TripId { get; set; }
    public string TripName { get; set; }
    public decimal Amount { get; set; }
    public string AccountNumber { get; set; }
    public bool IsPaid { get; set; }
    public int ParticipantCount { get; set; }
    public string AmountWithToman => Amount.ToString() + " تومان";
    public string? PaymentReceiptUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}

// مدل‌های پایه
public class FriendRequestDTO 
{
    public int SenderFamilyId { get; set; }
    public int Id { get; set; }
    public string SenderFamilyTitle { get; set; }
    public int ReceiverFamilyId { get; set; }
    public string ReceiverFamilyTitle { get; set; }
    public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public DateTime? RespondedAt { get; set; }
}
// مدل‌های پایه
public class FriendRequest
{
    public int Id { get; set; }

    public int SenderFamilyId { get; set; }
    public Family SenderFamily { get; set; }

    public int ReceiverFamilyId { get; set; }
    public Family ReceiverFamily { get; set; }

    public FriendRequestStatus Status { get; set; } = FriendRequestStatus.Pending;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    public DateTime? RespondedAt { get; set; }
}

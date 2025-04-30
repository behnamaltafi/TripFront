public class FriendRequestDto
{
    public int Id { get; set; }
    public int SenderFamilyId { get; set; }
    public int ReceiverFamilyId { get; set; }
    public string Status { get; set; }
    public DateTime SentAt { get; set; }
    public DateTime? RespondedAt { get; set; }
}

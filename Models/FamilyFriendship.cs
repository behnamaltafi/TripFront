// مدل‌های پایه
public class FamilyFriendship
{
    public int Id { get; set; }

    public int FamilyId1 { get; set; }
    public Family Family1 { get; set; }

    public int FamilyId2 { get; set; }
    public Family Family2 { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

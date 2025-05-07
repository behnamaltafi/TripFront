// مدل‌های پایه
public class FamilyFriendship:BaseEntity<int>
{


    public int FamilyId1 { get; set; }
    public Family Family1 { get; set; }

    public int FamilyId2 { get; set; }
    public Family Family2 { get; set; }

}



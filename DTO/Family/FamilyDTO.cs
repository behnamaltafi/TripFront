public class FamilyDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string AccountNumber { get; set; }
}
public class FamilyInfo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public string AccountNumber { get; set; }
    public string UserId { get; set; }
    public List<string> Interests { get; set; }
    public decimal Budget { get; set; }
    public int Friends { get; set; }
    public int Trips { get; set; }

}

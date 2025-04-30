
public interface IFriendshipService
{
    Task<bool> AreFriendsAsync(int familyId1, int familyId2);
    Task CreateFriendshipAsync(int familyId1, int familyId2);
    int GetFamilyId();
    Task<List<FamilyDTO>> GetFriendsAsync();
    Task RemoveFriendshipAsync(int familyId1, int familyId2);
}
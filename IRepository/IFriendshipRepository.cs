public interface IFriendshipRepository
{
    Task<FamilyFriendship?> GetFriendshipAsync(int familyId1, int familyId2);
    Task AddFriendshipAsync(FamilyFriendship friendship);
    Task RemoveFriendshipAsync(FamilyFriendship friendship);
    Task<List<FamilyDTO>> GetFriendsAsync(int familyId);
}
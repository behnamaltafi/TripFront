public interface IFriendshipRepository: IGenericRepository<FamilyFriendship, int>
{
    Task<FamilyFriendship?> GetFriendshipAsync(int familyId1, int familyId2);

    Task<List<FamilyDTO>> GetFriendsAsync(int familyId);
}
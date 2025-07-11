using FilterPagingEfCore.Paging;

public interface IFriendshipRepository : IGenericRepository<FamilyFriendship, int>
{
    Task AddFriendshipAsync(FamilyFriendship friendship);
    Task<bool> AreFriendsAsync(int currentFamilyId, int targetFamilyId);
    Task<PagingResult<FamilyDTO>> GetFriendsAsync(PagingParam pagingParam, int familyId);
    Task<FamilyFriendship> GetFriendshipAsync(int currentFamilyId, int targetFamilyId);
    Task RemoveFriendshipAsync(FamilyFriendship friendship);
}
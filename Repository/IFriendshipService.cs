
using FilterPagingEfCore.Paging;

public interface IFriendshipService
{
    Task AddFriendshipAsync(FamilyFriendship friendship);
    Task<bool> AreFriendsAsync( int targetFamilyId);
    Task<PagingResult<FamilyDTO>> GetFriendsAsync(PagingParam pagingParam, int familyId);
    Task<FamilyFriendship> GetFriendshipAsync( int targetFamilyId);
    Task RemoveFriendshipAsync(FamilyFriendship friendship);

}
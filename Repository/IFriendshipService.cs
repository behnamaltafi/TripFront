
using FilterPagingEfCore.Paging;

public interface IFriendshipService
{
    Task<bool> AreFriendsAsync(int familyId1, int familyId2);
    Task CreateFriendshipAsync(int familyId1, int familyId2);
    int GetFamilyId();
    Task<PagingResult<FamilyDTO>> GetFriendsAsync(PagingParam pagingParam);
    Task RemoveFriendshipAsync(int familyId1, int familyId2);
}
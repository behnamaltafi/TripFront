using FilterPagingEfCore.Paging;

public interface IFriendshipRepository: IGenericRepository<FamilyFriendship, int>
{
    Task<FamilyFriendship?> GetFriendshipAsync(int familyId1, int familyId2);

    Task<PagingResult<FamilyDTO>> GetFriendsAsync(PagingParam pagingParam, int familyId);
}
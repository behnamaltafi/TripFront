using BusinessExceptionStructure;
using FilterPagingEfCore.Paging;

public class FriendshipService : IFriendshipService
{
    private readonly IFriendshipRepository _repository;
    private readonly IFamilyService _familyService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public FriendshipService(IFriendshipRepository repository, IHttpContextAccessor httpContextAccessor, IFamilyService familyService)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
        _familyService = familyService;
    }

    public async Task AddFriendshipAsync(FamilyFriendship friendship)
    {
        await _repository.AddFriendshipAsync(friendship);
    }

    public async Task<bool> AreFriendsAsync(int targetFamilyId)
    {
        var currentFamilyId = _familyService.GetFamilyId();
        return await _repository.AreFriendsAsync(currentFamilyId, targetFamilyId);
    }

    public async Task<PagingResult<FamilyDTO>> GetFriendsAsync(PagingParam pagingParam, int familyId)
    {
        return await _repository.GetFriendsAsync(pagingParam, familyId);

    }

    public async Task<FamilyFriendship> GetFriendshipAsync(int targetFamilyId)
    {
        var currentFamilyId = _familyService.GetFamilyId();

        return await _repository.GetFriendshipAsync(currentFamilyId, targetFamilyId);

    }

    public async Task RemoveFriendshipAsync(FamilyFriendship friendship)
    {
        await _repository.RemoveFriendshipAsync(friendship);

    }
}



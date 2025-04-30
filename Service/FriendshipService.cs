public class FriendshipService : IFriendshipService
{
    private readonly IFriendshipRepository _repository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public FriendshipService(IFriendshipRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        _repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<bool> AreFriendsAsync(int familyId1, int familyId2)
    {
        var friendship = await _repository.GetFriendshipAsync(familyId1, familyId2);
        return friendship != null;
    }

    public async Task CreateFriendshipAsync(int familyId1, int familyId2)
    {
        if (familyId1 == familyId2)
            throw new InvalidOperationException("A family cannot be friends with itself.");

        var existing = await _repository.GetFriendshipAsync(familyId1, familyId2);
        if (existing != null)
            throw new InvalidOperationException("Families are already friends.");

        var friendship = new FamilyFriendship
        {
            FamilyId1 = Math.Min(familyId1, familyId2),
            FamilyId2 = Math.Max(familyId1, familyId2),
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddFriendshipAsync(friendship);
    }

    public async Task RemoveFriendshipAsync(int familyId1, int familyId2)
    {
        var friendship = await _repository.GetFriendshipAsync(familyId1, familyId2);
        if (friendship == null)
            throw new InvalidOperationException("Friendship does not exist.");

        await _repository.RemoveFriendshipAsync(friendship);
    }

    public async Task<List<FamilyDTO>> GetFriendsAsync()
    {
        var familyId = GetFamilyId();
        return await _repository.GetFriendsAsync(familyId);
    }
    public int GetFamilyId()
    {
        var familyId = _httpContextAccessor.HttpContext?.User.FindFirst("familyId")?.Value;
        if (familyId == null)
            throw new UnauthorizedAccessException();
        return int.Parse(familyId);
    }
}



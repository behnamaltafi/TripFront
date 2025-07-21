using AutoMapper;
using FilterPagingEfCore.Extenstion;
using FilterPagingEfCore.Paging;
using Microsoft.EntityFrameworkCore;

public class FriendshipRepository : GenericRepository<FamilyFriendship, int>, IFriendShipRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FriendshipRepository(AppDbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> AreFriendsAsync(int currentFamilyId, int targetFamilyId)
    {
        return await _context.FamilyFriendships
            .AnyAsync(f =>
                (f.FamilyId == currentFamilyId && f.FriendFamilyId == targetFamilyId ||
                 f.FamilyId == targetFamilyId && f.FriendFamilyId == currentFamilyId) &&
                f.Status == FriendshipStatus.Accepted);
    }

    public async Task<FamilyFriendship> GetFriendshipAsync(int currentFamilyId, int targetFamilyId)
    {
        return await _context.FamilyFriendships
            .FirstOrDefaultAsync(f =>
                (f.FamilyId == currentFamilyId && f.FriendFamilyId == targetFamilyId ||
                 f.FamilyId == targetFamilyId && f.FriendFamilyId == currentFamilyId));
    }

    public async Task AddFriendshipAsync(FamilyFriendship friendship)
    {
        await _context.FamilyFriendships.AddAsync(friendship);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFriendshipAsync(FamilyFriendship friendship)
    {
        _context.FamilyFriendships.Remove(friendship);
        await _context.SaveChangesAsync();
    }

    public async Task<PagingResult<FamilyDTO>> GetFriendsAsync(PagingParam pagingParam, int familyId)
    {
        var query = _context.FamilyFriendships
            .Where(f => (f.FamilyId == familyId || f.FriendFamilyId == familyId) &&
                       f.Status == FriendshipStatus.Accepted);


        return await _mapper.ProjectTo<FamilyDTO>(query).FilterPaging(pagingParam);

    }
}




using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class FriendshipRepository : IFriendshipRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FriendshipRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FamilyFriendship?> GetFriendshipAsync(int familyId1, int familyId2)
    {
        int minId = Math.Min(familyId1, familyId2);
        int maxId = Math.Max(familyId1, familyId2);

        return await _context.FamilyFriendships
            .FirstOrDefaultAsync(f => f.FamilyId1 == minId && f.FamilyId2 == maxId);
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

    public async Task<List<FamilyDTO>> GetFriendsAsync(int familyId)
    {
       var qry= _context.FamilyFriendships
          .Where(f => f.FamilyId1 == familyId || f.FamilyId2 == familyId)
          .Select(f => f.FamilyId1 == familyId ? f.Family2 : f.Family1);
        return await _mapper.ProjectTo<FamilyDTO>(qry).ToListAsync();

    }
}




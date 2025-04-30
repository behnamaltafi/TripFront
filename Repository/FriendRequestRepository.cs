using Microsoft.EntityFrameworkCore;

public class FriendRequestRepository : IFriendRequestRepository
{
    private readonly AppDbContext _context;

    public FriendRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<FriendRequest?> GetPendingRequestAsync(int senderFamilyId, int receiverFamilyId)
    {
        return await _context.FriendRequests
            .FirstOrDefaultAsync(r => r.SenderFamilyId == senderFamilyId &&
                                      r.ReceiverFamilyId == receiverFamilyId &&
                                      r.Status == FriendRequestStatus.Pending);
    }

    public async Task<List<FriendRequest>> GetReceivedRequestsAsync(int familyId)
    {
        return await _context.FriendRequests
            .Where(r => r.ReceiverFamilyId == familyId && r.Status == FriendRequestStatus.Pending)
            .ToListAsync();
    }

    public async Task<List<FriendRequest>> GetSentRequestsAsync(int familyId)
    {
        return await _context.FriendRequests
            .Where(r => r.SenderFamilyId == familyId && r.Status == FriendRequestStatus.Pending)
            .ToListAsync();
    }

    public async Task AddRequestAsync(FriendRequest request)
    {
        await _context.FriendRequests.AddAsync(request);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRequestAsync(FriendRequest request)
    {
        _context.FriendRequests.Update(request);
        await _context.SaveChangesAsync();
    }
}

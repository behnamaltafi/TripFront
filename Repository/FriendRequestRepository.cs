using AutoMapper;

public class FriendRequestRepository : GenericRepository<FriendRequest, int>, IFriendRequestRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public FriendRequestRepository(AppDbContext context, IMapper mapper) : base(context,mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    //public async Task<FriendRequest?> GetPendingRequestAsync(int senderFamilyId, int receiverFamilyId)
    //{
    //    return await _context.FriendRequests
    //        .FirstOrDefaultAsync(r => r.SenderFamilyId == senderFamilyId &&
    //                                  r.ReceiverFamilyId == receiverFamilyId &&
    //                                  r.Status == FriendRequestStatus.Pending);
    //}

    //public async Task<List<FriendRequest>> GetReceivedRequestsAsync(int familyId)
    //{
    //    return await _context.FriendRequests
    //        .Where(r => r.ReceiverFamilyId == familyId && r.Status == FriendRequestStatus.Pending)
    //        .ToListAsync();
    //}

    //public async Task<List<FriendRequest>> GetSentRequestsAsync(int familyId)
    //{
    //    return await _context.FriendRequests
    //        .Where(r => r.SenderFamilyId == familyId && r.Status == FriendRequestStatus.Pending)
    //        .ToListAsync();
    //}
}

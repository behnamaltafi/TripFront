
public class FriendRequestService : IFriendRequestService
{
    private readonly IFriendRequestRepository _requestRepository;
    private readonly IFriendshipRepository _friendshipRepository;
    private readonly IFriendshipService _friendshipService;

    public FriendRequestService(IFriendRequestRepository requestRepository, IFriendshipRepository friendshipRepository, IFriendshipService friendshipService)
    {
        _requestRepository = requestRepository;
        _friendshipRepository = friendshipRepository;
        _friendshipService = friendshipService;
    }

    public async Task SendFriendRequestAsync( int receiverFamilyId)
    {
        int senderFamilyId = _friendshipService.GetFamilyId();
        if (senderFamilyId == receiverFamilyId)
            throw new InvalidOperationException("Cannot send friend request to yourself.");

        var existingFriendship = await _friendshipRepository.GetFriendshipAsync(senderFamilyId, receiverFamilyId);
        if (existingFriendship != null)
            throw new InvalidOperationException("Already friends.");

        var existingRequest = await _requestRepository.GetPendingRequestAsync(senderFamilyId, receiverFamilyId);
        if (existingRequest != null)
            throw new InvalidOperationException("Friend request already sent and pending.");

        var newRequest = new FriendRequest
        {
            SenderFamilyId = senderFamilyId,
            ReceiverFamilyId = receiverFamilyId,
            Status = FriendRequestStatus.Pending,
            SentAt = DateTime.UtcNow
        };

        await _requestRepository.AddRequestAsync(newRequest);
    }

    public async Task AcceptFriendRequestAsync( int requestId)
    {
        int receiverFamilyId = _friendshipService.GetFamilyId();
        var request = (await _requestRepository.GetReceivedRequestsAsync(receiverFamilyId))
            .FirstOrDefault(r => r.Id == requestId);

        if (request == null)
            throw new InvalidOperationException("Friend request not found or already handled.");

        request.Status = FriendRequestStatus.Accepted;
        request.RespondedAt = DateTime.UtcNow;
        await _requestRepository.UpdateRequestAsync(request);

        var friendship = new FamilyFriendship
        {
            FamilyId1 = Math.Min(request.SenderFamilyId, request.ReceiverFamilyId),
            FamilyId2 = Math.Max(request.SenderFamilyId, request.ReceiverFamilyId),
            CreatedAt = DateTime.UtcNow
        };

        await _friendshipRepository.AddFriendshipAsync(friendship);
    }

    public async Task RejectFriendRequestAsync( int requestId)
    {
        int receiverFamilyId = _friendshipService.GetFamilyId();
        var request = (await _requestRepository.GetReceivedRequestsAsync(receiverFamilyId))
            .FirstOrDefault(r => r.Id == requestId);

        if (request == null)
            throw new InvalidOperationException("Friend request not found or already handled.");

        request.Status = FriendRequestStatus.Rejected;
        request.RespondedAt = DateTime.UtcNow;
        await _requestRepository.UpdateRequestAsync(request);
    }

    public async Task<List<FriendRequest>> GetReceivedFriendRequestsAsync()
    {
        int familyId = _friendshipService.GetFamilyId();
        return await _requestRepository.GetReceivedRequestsAsync(familyId);
    }

    public async Task<List<FriendRequest>> GetSentFriendRequestsAsync()
    {
        int familyId = _friendshipService.GetFamilyId();
        return await _requestRepository.GetSentRequestsAsync(familyId);
    }
}


using BusinessExceptionStructure;
using FilterPagingEfCore.Paging;

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

    public async Task SendFriendRequestAsync(int receiverFamilyId)
    {
        int senderFamilyId = _friendshipService.GetFamilyId();
        if (senderFamilyId == receiverFamilyId)
            throw new BusinessException("Cannot send friend request to yourself.");

        var existingFriendship = await _friendshipRepository.GetFriendshipAsync(senderFamilyId, receiverFamilyId);
        if (existingFriendship != null)
            throw new BusinessException("Already friends.");

        var existingRequest = await _requestRepository.FindAll(r => r.SenderFamilyId == senderFamilyId &&
                                     r.ReceiverFamilyId == receiverFamilyId &&
                                      r.Status == FriendRequestStatus.Pending);
        if (existingRequest != null)
            throw new BusinessException("Friend request already sent and pending.");

        var newRequest = new FriendRequest
        {
            SenderFamilyId = senderFamilyId,
            ReceiverFamilyId = receiverFamilyId,
            Status = FriendRequestStatus.Pending,
            SentAt = DateTime.UtcNow
        };

        await _requestRepository.Add(newRequest);
        await _requestRepository.Save();
    }

    public async Task AcceptFriendRequestAsync(int requestId)
    {
        int receiverFamilyId = _friendshipService.GetFamilyId();
        var request = (await _requestRepository.FindAll(r => r.ReceiverFamilyId == receiverFamilyId && r.Status == FriendRequestStatus.Pending))
            .FirstOrDefault(r => r.Id == requestId);

        if (request == null)
            throw new BusinessException("Friend request not found or already handled.");

        request.Status = FriendRequestStatus.Accepted;
        request.RespondedAt = DateTime.UtcNow;
        await _requestRepository.Update(request);

        var friendship = new FamilyFriendship
        {
            FamilyId1 = Math.Min(request.SenderFamilyId, request.ReceiverFamilyId),
            FamilyId2 = Math.Max(request.SenderFamilyId, request.ReceiverFamilyId),
        };

        await _friendshipRepository.Add(friendship);
        await _friendshipRepository.Save();
    }

    public async Task RejectFriendRequestAsync(int requestId)
    {
        int receiverFamilyId = _friendshipService.GetFamilyId();
        var request = (await _requestRepository.FindAll(r => r.ReceiverFamilyId == receiverFamilyId && r.Status == FriendRequestStatus.Pending))
            .FirstOrDefault(r => r.Id == requestId);

        if (request == null)
            throw new BusinessException("Friend request not found or already handled.");

        request.Status = FriendRequestStatus.Rejected;
        request.RespondedAt = DateTime.UtcNow;
        await _requestRepository.Update(request);
        await _requestRepository.Save();
    }

    public async Task<PagingResult<FriendRequestDTO>> GetReceivedFriendRequestsAsync(PagingParam pagingParam)
    {
            int familyId = _friendshipService.GetFamilyId();
            return await _requestRepository.FindAllPaging<FriendRequestDTO>(pagingParam, r => r.ReceiverFamilyId == familyId && r.Status == FriendRequestStatus.Pending);
    }

    public async Task<PagingResult<FriendRequestDTO>> GetSentFriendRequestsAsync(PagingParam pagingParam)
    {
        int familyId = _friendshipService.GetFamilyId();
        var result = await _requestRepository.FindAllPaging<FriendRequestDTO>(pagingParam, r => r.SenderFamilyId == familyId && r.Status == FriendRequestStatus.Pending);
        return result;
    }
}

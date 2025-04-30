public interface IFriendRequestRepository
{
    Task<FriendRequest?> GetPendingRequestAsync(int senderFamilyId, int receiverFamilyId);
    Task<List<FriendRequest>> GetReceivedRequestsAsync(int familyId);
    Task<List<FriendRequest>> GetSentRequestsAsync(int familyId);
    Task AddRequestAsync(FriendRequest request);
    Task UpdateRequestAsync(FriendRequest request);
}
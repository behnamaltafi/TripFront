
public interface IFriendRequestService
{
    Task AcceptFriendRequestAsync( int requestId);
    Task<List<FriendRequest>> GetReceivedFriendRequestsAsync();
    Task<List<FriendRequest>> GetSentFriendRequestsAsync();
    Task RejectFriendRequestAsync( int requestId);
    Task SendFriendRequestAsync(int receiverFamilyId);
}
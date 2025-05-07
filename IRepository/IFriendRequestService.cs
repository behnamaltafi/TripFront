
using FilterPagingEfCore.Paging;

public interface IFriendRequestService
{
    Task AcceptFriendRequestAsync( int requestId);
    Task<PagingResult<FriendRequestDTO>> GetReceivedFriendRequestsAsync(PagingParam pagingParam);
    Task<PagingResult<FriendRequestDTO>> GetSentFriendRequestsAsync(PagingParam pagingParam);
    Task RejectFriendRequestAsync( int requestId);
    Task SendFriendRequestAsync(int receiverFamilyId);
}
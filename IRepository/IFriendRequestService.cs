
using FilterPagingEfCore.Paging;

public interface IFriendRequestService
{
    Task AcceptFriendRequestAsync( int requestId);
    Task<PagingResult<FriendRequestDTO>> GetFriendRequestsAsync(PagingParam pagingParam);
    Task RejectFriendRequestAsync( int requestId);
    Task SendFriendRequestAsync(int receiverFamilyId);
}

using System.ComponentModel.DataAnnotations;

public enum FriendRequestStatus
{
    [Display(Name = "در انتظار")]
    Pending,

    [Display(Name = "قبول شده")]
    Accepted,

    [Display(Name = "قبول نشده")]
    Rejected
}
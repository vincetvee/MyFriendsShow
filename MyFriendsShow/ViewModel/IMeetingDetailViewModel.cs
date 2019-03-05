using MyFriendsShow.Wrapper;

namespace MyFriendsShow.ViewModel
{
    public interface IMeetingDetailViewModel: IDetailViewModel
    {
        MeetingWrapper Meeting { get; }
    }
}
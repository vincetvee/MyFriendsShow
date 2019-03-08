using FriendShowDataAccess.Migrations;
using FriendsShow.Model;
using System.Threading.Tasks;

namespace MyFriendsShow.Data.Repositories
{
    public interface IFriendRepository: IGenericRepository<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber model);
        Task<bool> HasMeetingsAsync(int friendId);
    }
}
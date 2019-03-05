using FriendShowDataAccess.Migrations;
using FriendsShow.Model;

namespace MyFriendsShow.Data.Repositories
{
    public interface IFriendRepository: IGenericRepository<Friend>
    {
        void RemovePhoneNumber(FriendPhoneNumber model);
    }
}
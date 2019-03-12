using System.Collections.Generic;
using System.Threading.Tasks;
using FriendsShow.Model;

namespace MyFriendsShow.Data.Repositories
{
    public interface IMeetingRepository:IGenericRepository<Meeting>
    {
        Task<List<Friend>> GetAllFriendsAsync();
        Task ReloadFriendAsync(int friendId);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using FriendsShow.Model;

namespace MyFriendsShow.Data.LookUps
{
    public interface IFriendLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetFriendLookupAsync();
    }
}
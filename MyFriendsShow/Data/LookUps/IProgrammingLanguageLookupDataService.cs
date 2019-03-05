 using System.Collections.Generic;
using System.Threading.Tasks;
using FriendsShow.Model;

namespace MyFriendsShow.Data.LookUps
{
    public interface IProgrammingLanguageLookupDataService
    {
        Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync();
    }
}
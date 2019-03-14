using FriendsShow.Model;
using System.Threading.Tasks;

namespace MyFriendsShow.Data.Repositories
{
    public interface IProgrammingLanguageRepository
        : IGenericRepository<ProgrammingLanguage>
    {
        Task<bool> IsReferencedByFriendAsync(int programmingLanguageId);
    }
}
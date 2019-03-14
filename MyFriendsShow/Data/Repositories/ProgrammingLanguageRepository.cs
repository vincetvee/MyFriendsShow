using System.Data.Entity;
using System.Threading.Tasks;
using FriendShowDataAccess;
using FriendsShow.Model;

namespace MyFriendsShow.Data.Repositories
{
    public  class ProgrammingLanguageRepository
        : GenericRepository<ProgrammingLanguage, FriendsShowDbContext>,
         IProgrammingLanguageRepository
    {
        public ProgrammingLanguageRepository(FriendsShowDbContext context) 
            : base(context)
        {
        }

        public async Task<bool> IsReferencedByFriendAsync(int programmingLanguageId)
        {
            return await Context.Friends.AsNoTracking()
                .AnyAsync(f => f.FavoriteLanguageId == programmingLanguageId);
        }
    }
}

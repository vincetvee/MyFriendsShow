using FriendShowDataAccess;
using FriendsShow.Model;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MyFriendsShow.Data.Repositories
{
    public class MeetingRepository : GenericRepository<Meeting, FriendsShowDbContext>, IMeetingRepository
    {
        protected MeetingRepository(FriendsShowDbContext context) : base(context)
        {
        }

        public async  override Task<Meeting> GetByIdAsync(int id)
        {
          return await  Context.Meetings
                .Include(m => m.Friends)
                .SingleAsync(m => m.Id == id);
        }
    }
}
 
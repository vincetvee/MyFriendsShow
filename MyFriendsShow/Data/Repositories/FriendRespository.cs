using FriendShowDataAccess;
using FriendsShow.Model;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MyFriendsShow.Data.Repositories
{

    public class FriendRespository :GenericRepository<Friend,FriendsShowDbContext>,
                                       IFriendRepository

    {
       
        public FriendRespository(FriendsShowDbContext context): base(context)
        {
        }

 

        public override async Task<Friend> GetByIdAsync(int friendId)
        {

                return await Context.Friends
                .Include(f => f.PhoneNumbers)
                .SingleAsync(f =>f.Id == friendId);
         
        }

        public async Task<bool> HasMeetingsAsync(int friendId)
        {
            return await Context.Meetings.AsNoTracking()
                .Include(m => m.Friends)
                .AnyAsync(m => m.Friends.Any(f => f.Id == friendId));
        }
    

        public void RemovePhoneNumber(FriendPhoneNumber model)
        {
            Context.FriendPhoneNumbers.Remove(model);
        }

        //public  void AddPhoneNumber( FriendPhoneNumber friendPhoneNumber)
        //{
        //    _context.Friends.Add(friendPhoneNumber);
        //}
        //public  void RemovePhoneNumber(FriendPhoneNumber friendPhoneNumber)
        //{
        //    _context.Friends.Remove();
        //}

        //public IEnumerable<Friend> GeAll()
        //{
        //    yeild return new Friend { FirstName = "Thomas", LastName = " Huber" };
        //    yeild return new Friend { FirstName = "Victor", LastName = " James" };
        //    yeild return new Friend { FirstName = "Emmanuel", LastName = " Luke" };
        //    yeild return new Friend { FirstName = "Solomon ", LastName = " John" };
        //    yeild return new Friend { FirstName = "Samuel", LastName= "Longcross" };

        //}

    }
}


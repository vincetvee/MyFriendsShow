using System.Threading.Tasks;

namespace MyFriendsShow.ViewModel
{
    public interface IDetailViewModel
    {
        Task LoadAsync(int? id);
        bool HasChanges { get; }
        int Id { get; }

    }
}
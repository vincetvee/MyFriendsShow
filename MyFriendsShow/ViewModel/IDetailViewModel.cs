using System.Threading.Tasks;

namespace MyFriendsShow.ViewModel
{
    public interface IDetailViewModel
    {
        bool HasChanges { get; }
        Task LoadAsync(int? id);
    }
}
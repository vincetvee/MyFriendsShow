using System.Threading.Tasks;

namespace MyFriendsShow.View.Service
{
    public interface IMessageDialogService
    {
       Task<MessageDialogResult> ShowOkCancelDialogAsync(string text, string title);
        Task ShowInfoDialogAsync(string text);
    
    }
}
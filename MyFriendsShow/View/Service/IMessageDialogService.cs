namespace MyFriendsShow.View.Service
{
    public interface IMessageDialogService
    {
        MessageDialogResult ShowOkCancelDialog(string text, string title);
        void ShowInforDailog(string text);
    
    }
}
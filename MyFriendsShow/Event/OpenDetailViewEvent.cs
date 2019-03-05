using Prism.Events;


namespace MyFriendsShow.Event
{
   public class OpenDetailViewEvent: PubSubEvent<OpenDetailViewEventArgs>
    {
    }
    public class OpenDetailViewEventArgs
    {
        public int? Id { get; set; }
        public string ViewModelName { get; set; }
    }

}

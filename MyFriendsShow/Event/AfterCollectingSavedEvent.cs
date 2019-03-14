using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFriendsShow.Event
{
    public  class AfterCollectingSavedEvent: PubSubEvent<AfterCollectionSavedEventArgs>
    {
    }

    public class AfterCollectionSavedEventArgs
    {
        public string ViewModelName { get; set; }
    }
}

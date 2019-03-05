using MyFriendsShow.Event;
using Prism.Commands;
using Prism.Events;
using System.Windows.Input;

namespace MyFriendsShow.ViewModel
{
    public  class NavigationItemViewModel : ViewModelBase
    {
          private string _displayMember;
          public ICommand OpenDetailViewCommand { get;  }
          private IEventAggregator _eventAggregator;
        public NavigationItemViewModel(int id, string displayMember,
            string detailViewModelName,
            IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            Id = id;
            DisplayMember = displayMember;
            _detiaViewModelName = detailViewModelName;
            OpenDetailViewCommand = new DelegateCommand(OnOpenDetailView);
        }

        public int Id { get; }
        public string DisplayMember {
            get { return _displayMember; }
            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }

        private  string _detiaViewModelName;

        private void OnOpenDetailView()
        {
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                      .Publish(
                new OpenDetailViewEventArgs {
                Id = Id,
                ViewModelName =_detiaViewModelName
                });
        }
    }
}

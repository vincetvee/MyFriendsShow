using MyFriendsShow.Event;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyFriendsShow.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
       // private object _messageDialogService;

         public ICommand CreateNewDetailCommand { get; }
        public INavigationViewModel NavigationViewModel { get; }
        public Func<IFriendDetailViewModel> _friendDetailViewModelCreator { get; }
        public Func<IMeetingDetailViewModel> _meetingDetailViewModelCreator { get; }

        private IEventAggregator _eventAggregator;
        private IDetailViewModel _detailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
           Func<IFriendDetailViewModel> friendDetailViewModelCreator,
           Func<IMeetingDetailViewModel> meetingDetailViewModelCreator,
            IEventAggregator eventAggregator)
        {
            _friendDetailViewModelCreator = friendDetailViewModelCreator;
            _meetingDetailViewModelCreator = meetingDetailViewModelCreator;

            _eventAggregator = eventAggregator;
           // _messageDialogService = messageDailogService;
                
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);
             CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);

             NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            private set {
                _detailViewModel = value;
                OnPropertyChanged();

            }
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if (DetailViewModel !=null  && DetailViewModel.HasChanges)
            {
                var result = MessageBox.Show("you've made changes.Navigate away?", "Question", 
                    MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    return;
                }
            }

             switch(args.ViewModelName)
             {
                case nameof(FriendDetailViewModel):
                    DetailViewModel = _friendDetailViewModelCreator();
                    break;
                case nameof(MeetingDetailViewModel):
                    DetailViewModel = _meetingDetailViewModelCreator();
                    break;
                default:
                    throw new Exception($"ViewModel {args.ViewModelName} not mapped");

             }
            await DetailViewModel.LoadAsync(args.Id);
        }

        private void OnCreateNewDetailExecute( Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { ViewModelName = viewModelType.Name });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
           DetailViewModel = null;
        }

    }

}

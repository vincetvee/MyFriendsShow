using Autofac.Features.Indexed;
using MyFriendsShow.Event;
using MyFriendsShow.View.Service;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyFriendsShow.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand CreateNewDetailCommand { get; }

        public  ICommand OpenSingleDetailViewCommand { get; set; }

        public INavigationViewModel NavigationViewModel { get; }

        public ObservableCollection<IDetailViewModel> DetailViewModels { get;}

        private IIndex<string, IDetailViewModel> _detailViewModelCreator;

        private IEventAggregator _eventAggregator;

        private IMessageDialogService _messageDialogService;

        private IDetailViewModel _selecteddetailViewModel;

        public MainViewModel(INavigationViewModel navigationViewModel,
           IIndex<string, IDetailViewModel>detailViewModelCreator,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService)
        {
            _detailViewModelCreator = detailViewModelCreator;
           

            _eventAggregator = eventAggregator;
            _messageDialogService = messageDialogService;


            DetailViewModels = new ObservableCollection<IDetailViewModel>();
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
                .Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailDeletedEvent>()
                .Subscribe(AfterDetailDeleted);

            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Subscribe(AfterDetailClosed);

             CreateNewDetailCommand = new DelegateCommand<Type>(OnCreateNewDetailExecute);
            OpenSingleDetailViewCommand = new DelegateCommand<Type>(OpenSingleDetailViewExecute);

             NavigationViewModel = navigationViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selecteddetailViewModel; }
            set
            {
                _selecteddetailViewModel = value;
                OnPropertyChanged();

            }
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == args.Id
                && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
               detailViewModel = _detailViewModelCreator[args.ViewModelName];
                try
                {
                await detailViewModel.LoadAsync(args.Id);

                }
                catch
                {

                  await  _messageDialogService.ShowInfoDialogAsync("Could not load the entity," +
                        "maybe it was deleted in the meantime by another user. " +
                        "The navigation is refreshed for you.");
                    await NavigationViewModel.LoadAsync();
                    return;
                }
                DetailViewModels.Add(detailViewModel);
            }
            SelectedDetailViewModel = detailViewModel;
        }


        private int nextNewItemId = 0;

        private void OnCreateNewDetailExecute( Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = nextNewItemId --,
                    ViewModelName = viewModelType.Name
                });
        }

        private void OpenSingleDetailViewExecute(Type viewModelType)
        {
            OnOpenDetailView(
                new OpenDetailViewEventArgs
                {
                    Id = -1,
                    ViewModelName = viewModelType.Name
                });
        }

        private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        {
            RemoveDetailViewModel(args.Id,args.ViewModelName);
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                            .SingleOrDefault(vm => vm.Id ==id
                            && vm.GetType().Name == viewModelName);
            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);

            }
        }

    }

}

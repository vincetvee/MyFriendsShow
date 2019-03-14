using MyFriendsShow.Data.Repositories;
using MyFriendsShow.View.Service;
using MyFriendsShow.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyFriendsShow.ViewModel
{
    public class ProgrammingLanguageDetailViewModel : DetailViewModelBase
    {
        private IProgrammingLanguageRepository _programmingLanguageRepository;
        private ProgrammingLanguageWrapper _selectedProgrammingLanguage;
       
        public ProgrammingLanguageDetailViewModel(IEventAggregator eventAggregator,
            IProgrammingLanguageRepository programmingLanguageRepository,
            IMessageDialogService messageDialogService)
            : base(eventAggregator, messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Programming Language";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageWrapper>();
            AddCommand = new DelegateCommand(OnAddExecute);
            RemoveCommand = new DelegateCommand(OnRemoveExecute, OnRemoveCanExecute);

        }

        private bool OnRemoveCanExecute()
        {
            return SelectedProgrammingLanguage != null;
        }

        private async  void OnRemoveExecute()
        {
            var isReferenced = await _programmingLanguageRepository.IsReferencedByFriendAsync(
                SelectedProgrammingLanguage.Id);
            if (isReferenced)
            {
                 await MessageDialogService.ShowInfoDialogAsync($"The language {SelectedProgrammingLanguage.Name}"
                    + $"can't be removed, as it is referenced by at least one friend");
                return;
            }
            SelectedProgrammingLanguage.PropertyChanged -= Wrapper_PropertyChanged;
            _programmingLanguageRepository.Remove(SelectedProgrammingLanguage.Model);
            ProgrammingLanguages.Remove(SelectedProgrammingLanguage);
            SelectedProgrammingLanguage = null;
            HasChanges = _programmingLanguageRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddExecute()
        {
            var wrapper = new ProgrammingLanguageWrapper(new FriendsShow.Model.ProgrammingLanguage());
            wrapper.PropertyChanged += Wrapper_PropertyChanged;
            _programmingLanguageRepository.Add(wrapper.Model);
            ProgrammingLanguages.Add(wrapper);
        }

        public ObservableCollection<ProgrammingLanguageWrapper> ProgrammingLanguages { get; }
        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public ProgrammingLanguageWrapper SelectedProgrammingLanguage
        {
            get { return _selectedProgrammingLanguage; }
            set
            {
                _selectedProgrammingLanguage = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveCommand).RaiseCanExecuteChanged();

            }
        }


        public  async override Task LoadAsync(int id)
        {
            Id = id;
            foreach (var wrapper in ProgrammingLanguages)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }

            ProgrammingLanguages.Clear();
            var languages = await _programmingLanguageRepository.GetAllAsync();

            foreach (var model in languages)
            {
                var wrapper = new ProgrammingLanguageWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProgrammingLanguages.Add(wrapper);
            }
           // return Task.Delay(0);
        }
        


        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _programmingLanguageRepository.HasChanges();

            }
            if (e.PropertyName == nameof(ProgrammingLanguageWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected  async  override void OnSaveExecute()
        {
            try
            {

            await _programmingLanguageRepository.SaveAsync();
            HasChanges = _programmingLanguageRepository.HasChanges();
            RaiseCollectionSavedEvent(); 
            }
            catch (Exception ex)
            {
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
               await MessageDialogService.ShowInfoDialogAsync("Error while saving the entites, " +
                    " the data will be reloaded. Details: " + ex.Message);
                await LoadAsync(Id);
            }
        }

       
    }
}

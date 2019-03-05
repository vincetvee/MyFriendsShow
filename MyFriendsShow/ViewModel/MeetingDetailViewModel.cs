using FriendsShow.Model;
using MyFriendsShow.Data.Repositories;
using MyFriendsShow.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace MyFriendsShow.ViewModel
{
    public  class MeetingDetailViewModel : DetailViewModelBase,IMeetingDetailViewModel
    {
        private  IMeetingRepository _meetingRepository;
        private MeetingWrapper _meeting;

        public MeetingDetailViewModel(IEventAggregator eventAggregator, 
            IMeetingRepository meetingRepository) : base(eventAggregator)
        {
            _meetingRepository = meetingRepository;
        }

         
        public MeetingWrapper Meeting
        {
            get { return _meeting; }
             private set
            {
                _meeting = value;
                OnPropertyChanged(); 
            }
        }

        public override  async Task LoadAsync(int? meetingId)
        {
            var meeting = meetingId.HasValue
                ? await _meetingRepository.GetByIdAsync(meetingId.Value)
                : CreeateNewMeeting();

            InitializeMeeting(meeting);
        }


        protected override void OnDeleteExecute()
        {
            _meetingRepository.Remove(Meeting.Model);
            _meetingRepository.SaveAsync();
            RaiseDetailDeletedEvent(Meeting.Id);

        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChanges;
        }

        protected override  async void OnSaveExecute()
        {
            await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseDetailSaveEvent(Meeting.Id, Meeting.Title);
        }
        private Meeting CreeateNewMeeting()
        {
            var meeting = new Meeting
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date
            };
            _meetingRepository.Add(meeting);
            return meeting;
        }

        private void InitializeMeeting(Meeting meeting)
        {
            Meeting = new MeetingWrapper(meeting);
            Meeting.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _meetingRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }
}

using Autofac;
using FriendShowDataAccess;
using MyFriendsShow.Data.LookUps;
using MyFriendsShow.Data.Repositories;
using MyFriendsShow.View.Service;
using MyFriendsShow.ViewModel;
using Prism.Events;

namespace MyFriendsShow.Startup
{
    public  class Bootstrapper
    {
       public  IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();
            builder.RegisterType<FriendsShowDbContext>().AsSelf();

            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            
            builder.RegisterType<FriendDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(FriendDetailViewModel));
            builder.RegisterType<MeetingDetailViewModel>()
                .Keyed<IDetailViewModel>(nameof(MeetingDetailViewModel));
            builder.RegisterType<ProgrammingLanguageDetailViewModel>()
                 .Keyed<IDetailViewModel>(nameof(ProgrammingLanguageDetailViewModel));



            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendRespository>().As<IFriendRepository>();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();
            builder.RegisterType<ProgrammingLanguageRepository>()
                .As<IProgrammingLanguageRepository>();

            return builder.Build();
        }
    }
}

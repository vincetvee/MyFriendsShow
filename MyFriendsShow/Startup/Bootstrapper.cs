using Autofac;
using FriendShowDataAccess;
using MyFriendsShow.Data.LookUps;
using MyFriendsShow.Data.Repositories;
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
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            
            builder.RegisterType<FriendDetailViewModel>().As<IFriendDetailViewModel>();
            builder.RegisterType<MeetingDetailViewModel>().As<IMeetingDetailViewModel>();



            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<FriendRespository>().As<IFriendRepository>();
            builder.RegisterType<MeetingRepository>().As<IMeetingRepository>();

            return builder.Build();
        }
    }
}

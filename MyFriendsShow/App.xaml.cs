using Autofac;
using MyFriendsShow.Startup;
using System;
using System.Windows;

namespace MyFriendsShow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var bootstrapper = new Bootstrapper();
            var Container = bootstrapper.Bootstrap();
            
            var mainWindow = Container.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occured. please inform the admin."
                + Environment.NewLine + e.Exception.Message, "Unexpected error");
            e.Handled = true;
        }
    }

}

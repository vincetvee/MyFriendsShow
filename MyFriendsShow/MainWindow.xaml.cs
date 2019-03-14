using MahApps.Metro.Controls;
using MyFriendsShow.ViewModel;
using System.Windows;

namespace MyFriendsShow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        /// <summary>
        /// storing load data in the object  MainViewModel 
        /// 
        /// </summary>
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadAsync();

        }

    }
}

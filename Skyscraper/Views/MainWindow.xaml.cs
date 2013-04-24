using System;
using System.Windows;
using Skyscraper.ViewModels;

namespace Skyscraper.Views
{
    public partial class MainWindow : Window
    {
        protected MainWindowViewModel ViewModel
        {
            get
            {
                return this.DataContext as MainWindowViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        protected override void OnSourceInitialized(System.EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.ExtendGlassFrame(new Thickness(-1));
        }

        private RawLogWindow rawLogWindow;
        private UserListWindow userListWindow;

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new MainWindowViewModel();

            this.rawLogWindow = new RawLogWindow();
            this.rawLogWindow.DataContext = this.ViewModel;
            this.rawLogWindow.Show();

            this.userListWindow = new UserListWindow();
            this.userListWindow.DataContext = this.ViewModel;
            this.userListWindow.Show();
        }

        public void WindowActive(object sender, EventArgs e)
        {
            this.ViewModel.Client.WindowActive = this.IsActive;
        }
    }
}
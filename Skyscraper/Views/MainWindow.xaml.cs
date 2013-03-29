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

        public MainWindow()
        {
            InitializeComponent();

            this.ViewModel = new MainWindowViewModel();
            this.rawLogWindow = new RawLogWindow();
            this.rawLogWindow.DataContext = this.ViewModel;
            this.rawLogWindow.Show();
        }
    }
}
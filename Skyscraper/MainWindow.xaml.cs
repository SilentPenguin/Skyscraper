using Skyscraper.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Skyscraper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainWindowViewModel();
        }
    }

    public class MainWindowViewModel
    {
        public ObservableCollection<object> Chatlog { get; set; }

        public MainWindowViewModel()
        {
            this.Chatlog = new ObservableCollection<object>();

            IUser Smippy = new User{
                Nickname = "SmellyHippy",
                Hostname = "~asmodean@about/csharp/regular/smellyhippy",
                Server = "roddenberry.freenode.net",
                Status = UserStatus.Active,
            };

            IUser Seppy = new User {
                Nickname = "Seppy",
                Hostname = "~SilentPen@unaffiliated/silentpenguin",
                Server = "hubbard.freenode.net",
                Status = UserStatus.Active,
            };
            
            this.Chatlog.Add(new Join(User: Smippy));
            this.Chatlog.Add(new Message(User: Smippy, Message: "Lonely In Here"));
            this.Chatlog.Add(new Join(User: Seppy));
            this.Chatlog.Add(new Message(User: Smippy, Message: "Parp"));
            this.Chatlog.Add(new Message(User: Seppy, Message: "Sup"));
        }
    }
}

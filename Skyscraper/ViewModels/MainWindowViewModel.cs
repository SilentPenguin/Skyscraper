using System.Windows.Input;
using Skyscraper.Irc;
using Skyscraper.Models;
using Skyscraper.Utilities;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();

        private Channel channel;
        public Channel Channel
        {
            get
            {
                return this.channel;
            }
            set
            {
                this.SetProperty(ref this.channel, value);
            }
        }

        private string chatInput;
        public string ChatInput
        {
            get
            {
                return this.chatInput;
            }
            set
            {
                this.SetProperty(ref this.chatInput, value);
                this.SendCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand ConnectCommand { get; private set; }
        public RelayCommand SendCommand { get; private set; }

        public MainWindowViewModel() 
        {
            this.InitCommands();
        }

        private void InitCommands() 
        {
            this.ConnectCommand = new RelayCommand((param) => { this.Connect(); });
            this.SendCommand = new RelayCommand((param) => { this.Send(); }, (param) => { return !string.IsNullOrEmpty(this.ChatInput); });
        }

        private void Connect()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.connectionManager.Connect();
        }

        private void Send()
        {
            this.connectionManager.Send(this.Channel, this.ChatInput);
            this.ChatInput = string.Empty;
        }

        void connectionManager_JoinedChannel(object sender, JoinedChannelEventArgs e)
        {
            this.Channel = e.Channel;
        }
    }
}
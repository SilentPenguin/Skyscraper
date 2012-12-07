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
            }
        }

        public MainWindowViewModel() { }

        private void Connect()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.connectionManager.Connect();
        }

        void connectionManager_JoinedChannel(object sender, JoinedChannelEventArgs e)
        {
            this.Channel = e.Channel;
        }
    }
}
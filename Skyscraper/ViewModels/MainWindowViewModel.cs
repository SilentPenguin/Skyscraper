using System.Windows.Input;
using Skyscraper.Irc;
using Skyscraper.Models;
using Skyscraper.Utilities;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();

        private Connection connection;
        public Connection Connection
        {
            get
            {
                return this.connection;
            }
            set
            {
                this.SetProperty(ref this.connection, value);
            }
        }

        private IChannel channel;
        public IChannel Channel
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
        public RelayCommand DisconnectCommand { get; private set; }
        public RelayCommand SendCommand { get; private set; }

        public MainWindowViewModel() 
        {
            this.InitCommands();
        }

        private void InitCommands() 
        {
            this.ConnectCommand = new RelayCommand(
            (param) => { this.Connect(); },
            (param) => 
            { 
                if(this.Connection == null)
                    return true;

                return !this.Connection.IsConnected;
            });

            this.DisconnectCommand = new RelayCommand(
            (param) => { this.Disconnect(); }, 
            (param) => 
            {
                if (this.Connection == null)
                    return false;

                return this.Connection.IsConnected;
            });

            this.SendCommand = new RelayCommand((param) => { this.Send(); }, (param) => { return !string.IsNullOrEmpty(this.ChatInput); });
        }

        private void Connect()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.Connection = this.connectionManager.Connect();
            this.Connection.PropertyChanged += Connection_PropertyChanged;
        }

        void Connection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                this.ConnectCommand.RaiseCanExecuteChanged();
                this.DisconnectCommand.RaiseCanExecuteChanged();
            }
        }

        private void Disconnect()
        {
            Connection connection = this.Connection;
            this.Connection = null;

            this.connectionManager.Disconnect(connection);
            connection.PropertyChanged -= Connection_PropertyChanged;
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
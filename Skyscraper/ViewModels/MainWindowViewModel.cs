using Skyscraper.Irc;
using Skyscraper.Data;
using Skyscraper.Utilities;
using System;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();

        private IReplayHistory ReplayHistory { get; set; }

        private INetwork connection;
        public INetwork Connection
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
        public RelayCommand ReplayPreviousCommand { get; private set; }
        public RelayCommand ReplayNextCommand { get; private set; }

        public MainWindowViewModel() 
        {
            this.ReplayHistory = new ReplayHistory();
            this.InitCommands();
        }

        private void InitCommands() 
        {
            this.ConnectCommand = new RelayCommand(
            (executeParam) => { this.Connect(); },
            (canExecuteParam) => { return (this.Connection == null) ? true : !this.Connection.IsConnected; });

            this.DisconnectCommand = new RelayCommand(
            (executeParam) => { this.Disconnect(); },
            (canExecuteParam) => { return (this.Connection == null) ? false : this.Connection.IsConnected; });

            this.SendCommand = new RelayCommand(
            (executeParam) => { this.CommandReceived(); },
            (canExecuteParam) => { return !string.IsNullOrEmpty(this.ChatInput); });

            this.ReplayPreviousCommand = new RelayCommand(
            (executeParam) => { this.NavigateUpReplay(); },
            (canExecuteParam) => { return true; });

            this.ReplayNextCommand = new RelayCommand(
            (executeParam) => { this.NavigateDownReplay(); },
            (canExecuteParam) => { return this.ReplayHistory.IsReplaying; });

        }

        private void CheckReplaying()
        {
            if (!this.ReplayHistory.IsReplaying && !String.IsNullOrEmpty(this.ChatInput))
            {
                this.ReplayHistory.Add(this.ChatInput);
                this.ReplayHistory.GetPreviousCommand();
            }
        }

        private void NavigateUpReplay()
        {
            this.CheckReplaying();
            this.ChatInput = this.ReplayHistory.GetPreviousCommand();
        }

        private void NavigateDownReplay()
        {
            this.CheckReplaying();
            this.ChatInput = this.ReplayHistory.GetNextCommand();
        }

        private void CommandReceived()
        {
            ICommand command = new Command(this.ChatInput);
            switch (command.Type)
            {
                case CommandType.Say:
                    this.Say(command);
                    break;
                case CommandType.Server:
                    this.Connect(command);
                    break;
                case CommandType.Quit:
                    this.Disconnect();
                    break;
            }
            this.ReplayHistory.Add(command);
            this.ChatInput = string.Empty;
        }

        private void Say(ICommand command)
        {
            this.connectionManager.Send(this.channel, command.Body);
        }

        private void Connect(ICommand command)
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            String uri = command.Arguments[0];
            String protocol = "irc://";
            if (!uri.StartsWith(protocol))
            {
                uri = protocol + uri;
            }
            Uri networkUrl = new Uri(uri);
            if (networkUrl.Port < 0)
            {
                Int16 port = 6667;
                uri += ":" + port;
            }
            networkUrl = new Uri(uri);
            INetwork network = new Network {
                Url = networkUrl,
            };
            this.Connection = this.connectionManager.Connect(network);
            this.Connection.PropertyChanged += Connection_PropertyChanged;
        }

        [Obsolete]
        private void Connect()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.Connection = this.connectionManager.Connect(new Network { Url = new Uri("irc://irc.freenode.net:6667") });
            this.Connection.PropertyChanged += Connection_PropertyChanged;
        }

        void Connection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                this.ConnectCommand.RaiseCanExecuteChanged();
                this.DisconnectCommand.RaiseCanExecuteChanged();

                if (this.connection.IsConnected)
                    this.connectionManager.Join(this.connection, "#skyscraper");
            }
        }

        private void Disconnect()
        {
            INetwork connection = this.Connection;
            this.Connection = null;

            this.connectionManager.Disconnect(connection);
            connection.PropertyChanged -= Connection_PropertyChanged;
        }

        private void Send()
        {
            this.ReplayHistory.Add(this.ChatInput);
            this.connectionManager.Send(this.Channel, this.ChatInput);
            this.ChatInput = string.Empty;
        }

        void connectionManager_JoinedChannel(object sender, JoinedChannelEventArgs e)
        {
            this.Channel = e.Channel;
        }
    }
}
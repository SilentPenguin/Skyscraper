using System;
using Skyscraper.Models;
using Skyscraper.Irc;
using Skyscraper.Utilities;
using Skyscraper.ClientCommands;
using Skyscraper.Irc.Events;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();

        private IReplayHistory replayHistory { get; set; }

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

        public RelayCommand SendCommand { get; private set; }
        public RelayCommand ReplayPreviousCommand { get; private set; }
        public RelayCommand ReplayNextCommand { get; private set; }

        public MainWindowViewModel() 
        {
            this.replayHistory = new ReplayHistory();
            this.InitCommands();
            this.InitConnectionManagerEvents();
        }

        private void InitConnectionManagerEvents()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.connectionManager.NetworkAdded += connectionManager_NetworkAdded;
            this.connectionManager.NetworkRemoved += connectionManager_NetworkRemoved;
        }

        private void InitCommands() 
        {
            this.SendCommand = new RelayCommand(
            (executeParam) => { this.CommandReceived(); },
            (canExecuteParam) => { return !string.IsNullOrEmpty(this.ChatInput); });

            this.ReplayPreviousCommand = new RelayCommand(
            (executeParam) => { this.NavigateUpReplay(); },
            (canExecuteParam) => { return true; });

            this.ReplayNextCommand = new RelayCommand(
            (executeParam) => { this.NavigateDownReplay(); },
            (canExecuteParam) => { return this.replayHistory.IsReplaying; });

        }

        private void CheckReplaying()
        {
            if (!this.replayHistory.IsReplaying && !String.IsNullOrEmpty(this.ChatInput))
            {
                this.replayHistory.Add(this.ChatInput);
                this.replayHistory.GetPreviousCommand();
            }
        }

        private void NavigateUpReplay()
        {
            this.CheckReplaying();
            this.ChatInput = this.replayHistory.GetPreviousCommand();
        }

        private void NavigateDownReplay()
        {
            this.CheckReplaying();
            this.ChatInput = this.replayHistory.GetNextCommand();
        }

        private void CommandReceived()
        {
            ICommand command = CommandFactory.Resolve(this.Connection, this.Channel, this.ChatInput);
            command.Execute(this.connectionManager);
            this.replayHistory.Add(command);
            this.ChatInput = string.Empty;
        }

        [Obsolete]
        private void Say(ICommand command)
        {
            this.connectionManager.Send(this.channel, command.Body);
        }

        [Obsolete]
        private void Connect(ICommand command)
        {
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
            
        }

        [Obsolete]
        private void Disconnect()
        {
            INetwork connection = this.Connection;
            this.Connection = null;

            this.connectionManager.Disconnect(connection);
            connection.PropertyChanged -= Connection_PropertyChanged;
        }

        [Obsolete]
        private void Send()
        {
            this.replayHistory.Add(this.ChatInput);
            this.connectionManager.Send(this.Channel, this.ChatInput);
            this.ChatInput = string.Empty;
        }

        void connectionManager_NetworkAdded(object sender, NetworkEventArgs e)
        {
            this.Connection = e.Network;
            this.Connection.PropertyChanged += Connection_PropertyChanged;
        }

        void connectionManager_NetworkRemoved(object sender, NetworkEventArgs e)
        {
            this.connection.PropertyChanged -= Connection_PropertyChanged;
            this.connection = null;
        }

        void connectionManager_JoinedChannel(object sender, ChannelEventArgs e)
        {
            this.Channel = e.Channel;
        }
    }
}
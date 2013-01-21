using System;
using Skyscraper.Models;
using Skyscraper.Irc;
using Skyscraper.Utilities;
using Skyscraper.ClientCommands;
using Skyscraper.Irc.Events;
using System.Windows;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();
        private IReplayHistory replayHistory = new ReplayHistory();

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

        private IRawLog rawLog;
        public IRawLog RawLog
        {
            get
            {
                return this.rawLog;
            }
            set
            {
                this.SetProperty(ref this.rawLog, value);
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
            this.InitCommands();
            this.InitConnectionManagerEvents();
        }

        private void InitCommands() 
        {
            this.SendCommand = new RelayCommand((executeParam) => { this.CommandReceived(); }, (canExecuteParam) => { return !string.IsNullOrEmpty(this.ChatInput); });
            this.ReplayPreviousCommand = new RelayCommand((executeParam) => { this.NavigateUpReplay(); },(canExecuteParam) => { return true; });
            this.ReplayNextCommand = new RelayCommand((executeParam) => { this.NavigateDownReplay(); }, (canExecuteParam) => { return this.replayHistory.IsReplaying; });

            this.rawLog = new RawLog();
        }

        private void InitConnectionManagerEvents()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.connectionManager.NetworkAdded += connectionManager_NetworkAdded;
            this.connectionManager.NetworkRemoved += connectionManager_NetworkRemoved;

            this.connectionManager.RawMessage += connectionManager_RawMessage;
        }

        void connectionManager_RawMessage(object sender, RawMessageEventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                this.RawLog.Log.Add(e.RawMessage);
            });
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
            ICommandHandler command = CommandFactory.Resolve(this.Connection, this.Channel, this.ChatInput);
            command.Execute(this.connectionManager);
            this.replayHistory.Add(command);
            this.ChatInput = string.Empty;
        }

        void Connection_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) { }

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
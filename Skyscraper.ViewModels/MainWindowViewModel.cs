using System;
using System.Linq;
using System.Windows;
using Skyscraper.ClientCommands;
using Skyscraper.Irc;
using Skyscraper.Irc.Events;
using Skyscraper.Models;
using Skyscraper.Utilities;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();
        private IReplayHistory replayHistory = new ReplayHistory();

        private IUser user;
        public IUser User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.SetProperty(ref this.user, value);
            }
        }

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

            string defaultUsername = Environment.UserName
                .Replace(" ", "")
                .Replace(".", "");

            this.user = new User
            {
                Nickname = defaultUsername,
                Realname = defaultUsername,
            };
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
            ICommandState commandState = CommandFactory.Resolve(this.Connection, this.Channel, this.user, this.ChatInput);
            commandState.Execute(this.connectionManager);
            
            this.replayHistory.Add(commandState.Command);
            this.ChatInput = string.Empty;
        }

        void connectionManager_NetworkAdded(object sender, NetworkEventArgs e)
        {
            this.Connection = e.Network;
        }

        void connectionManager_NetworkRemoved(object sender, NetworkEventArgs e)
        {
            this.connection = null;
        }

        void connectionManager_JoinedChannel(object sender, ChannelEventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                this.Channel = e.Channel;
            });
        }

        void connectionManager_PartedChannel(object sender, ChannelEventArgs e)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                this.Channel = this.Connection.Channels.FirstOrDefault();
            });
        }
    }
}
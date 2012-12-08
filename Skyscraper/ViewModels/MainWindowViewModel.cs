﻿using System.Windows.Input;
using Skyscraper.Irc;
using Skyscraper.Models;
using Skyscraper.Utilities;
using System;

namespace Skyscraper.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ConnectionManager connectionManager = new ConnectionManager();

        private IConnection connection;
        public IConnection Connection
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
            (executeParam) => { this.Connect(); },
            (canExecuteParam) => { return (this.Connection == null) ? true : !this.Connection.IsConnected; });

            this.DisconnectCommand = new RelayCommand(
            (executeParam) => { this.Disconnect(); },
            (canExecuteParam) => { return (this.Connection == null) ? false : this.Connection.IsConnected; });

            this.SendCommand = new RelayCommand(
            (executeParam) => { this.Send(); },
            (canExecuteParam) => { return !string.IsNullOrEmpty(this.ChatInput); });
        }

        private void Connect()
        {
            this.connectionManager.JoinedChannel += connectionManager_JoinedChannel;
            this.Connection = this.connectionManager.Connect(new Network { Url = new Uri ("irc://irc.freenode.net:6667") });
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
            IConnection connection = this.Connection;
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
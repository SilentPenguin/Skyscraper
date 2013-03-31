﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using IrcDotNet;
using IrcDotNet.Collections;
using Skyscraper.Irc.Events;
using Skyscraper.Models;

namespace Skyscraper.Irc
{
    internal static class ModesExtensionMethods
    {
        [DebuggerStepThrough]
        internal static string Join(this ReadOnlySet<char> self)
        {
            return new string(self.ToArray());
        }
    }

    public class ConnectionManager : IConnectionManager
    {
        private Dictionary<INetwork, IrcClient> ircClients = new Dictionary<INetwork, IrcClient>();
        private Dictionary<IrcClient, INetwork> connections = new Dictionary<IrcClient, INetwork>();

        private Dictionary<IChannel, IrcChannel> ircChannels = new Dictionary<IChannel, IrcChannel>();
        private Dictionary<IrcChannel, IChannel> channels = new Dictionary<IrcChannel, IChannel>();

        private Dictionary<IUser, IrcChannelUser> ircChannelUsers = new Dictionary<IUser, IrcChannelUser>();
        private Dictionary<IrcChannelUser, IUser> channelUsers = new Dictionary<IrcChannelUser, IUser>();

        private Dictionary<IUser, IrcUser> ircUsers = new Dictionary<IUser, IrcUser>();
        private Dictionary<IrcUser, IUser> users = new Dictionary<IrcUser, IUser>();

        public event EventHandler<NetworkEventArgs> NetworkAdded;
        protected virtual void OnNetworkAdded(INetwork network)
        {
            EventHandler<NetworkEventArgs> eventHandler = this.NetworkAdded;
            if (eventHandler != null)
            {
                eventHandler(this, new NetworkEventArgs(network));
            }
        }

        public event EventHandler<NetworkEventArgs> NetworkRemoved;
        protected virtual void OnNetworkRemoved(INetwork network)
        {
            EventHandler<NetworkEventArgs> eventHandler = this.NetworkRemoved;
            if (eventHandler != null)
            {
                eventHandler(this, new NetworkEventArgs(network));
            }
        }

        public event EventHandler<ChannelEventArgs> JoinedChannel;
        protected virtual void OnJoinedChannel(IChannel channel)
        {
            EventHandler<ChannelEventArgs> eventHandler = this.JoinedChannel;
            if (eventHandler != null)
            {
                eventHandler(this, new ChannelEventArgs(channel));
            }
        }

        public event EventHandler<ChannelEventArgs> PartedChannel;
        protected virtual void OnPartedChannel(IChannel channel)
        {
            EventHandler<ChannelEventArgs> eventHandler = this.PartedChannel;
            if (eventHandler != null)
            {
                eventHandler(this, new ChannelEventArgs(channel));
            }
        }

        public event EventHandler<RawMessageEventArgs> RawMessage;
        protected virtual void OnRawMessage(string message, RawMessageDirection direction)
        {
            EventHandler<RawMessageEventArgs> eventHandler = this.RawMessage;
            if (eventHandler != null)
            {
                eventHandler(this, new RawMessageEventArgs(new RawMessage(message, direction)));
            }
        }

        #region Connect
        public INetwork Connect(INetwork network, IUser user)
        {
            IrcClient ircClient = new IrcClient();

            INetwork connection = this.RegisterNetwork(ircClient, network);

            this.OnNetworkAdded(connection);

            ircClient.Connect(network.Url.Host, network.Url.Port, false, new IrcUserRegistrationInfo()
            {
                NickName = user.Nickname,
                RealName = user.Realname,
                UserName = user.Nickname,
            });

            return network;
        }
        #endregion

        #region Disconnect
        public void Disconnect(INetwork connection, string message = null)
        {
            IrcClient ircClient = this.ircClients[connection];
            ircClient.Quit(message);
            this.OnNetworkRemoved(connection);
        }
        #endregion

        #region Join
        public void Join(INetwork connection, string channelName)
        {
            IrcClient ircClient = this.ircClients[connection];
            ircClient.Channels.Join(channelName);
        }
        #endregion

        #region Part
        public void Part(INetwork connection, string channelName)
        {
            IrcClient ircClient = this.ircClients[connection];
            ircClient.Channels.Leave(channelName);
        }
        #endregion

        #region Nickname
        public void SetNickname(INetwork connection, string newNickname)
        {
            IrcClient ircClient = this.ircClients[connection];
            ircClient.LocalUser.SetNickName(newNickname);
        }
        #endregion

        #region Send
        public void Send(IChannel channel, IUser user, string message)
        {
            IrcChannel ircChannel = this.ircChannels[channel];
            ircChannel.Client.LocalUser.SendMessage(ircChannel, message);

            channel.Log.Add(new Message(user, message));
        }
        #endregion

        #region SendAction
        public void SendAction(INetwork connection, IChannel channel, string action)
        {
            IrcClient ircClient = this.ircClients[connection];
            IrcChannel ircChannel = this.ircChannels[channel];
            IrcDotNet.Ctcp.CtcpClient ctcpClient = new IrcDotNet.Ctcp.CtcpClient(ircClient);
            ctcpClient.SendAction(ircChannel, action);
        }
        #endregion

        #region SendRaw
        public void SendRaw(INetwork network, string message)
        {
            IrcClient ircClient = this.ircClients[network];
            ircClient.SendRawMessage(message);
        }
        #endregion

        #region Create
        private INetwork RegisterNetwork(IrcClient ircClient, INetwork network)
        {
            this.ircClients.Add(network, ircClient);
            this.connections.Add(ircClient, network);

            ircClient.RawMessageReceived += ircClient_RawMessageReceived;
            ircClient.RawMessageSent += ircClient_RawMessageSent;
            ircClient.Registered += ircClient_Registered;
            ircClient.Disconnected +=ircClient_Disconnected;

            return network;
        }

        private IChannel CreateChannel(IrcChannel ircChannel)
        {
            IrcClient ircClient = ircChannel.Client;

            IChannel channel = new Channel()
            {
                Name = ircChannel.Name,
                Topic = ircChannel.Topic,
                Modes = ircChannel.Modes.Join()
            };

            ircChannel.UsersListReceived += ircChannel_UsersListReceived;
            ircChannel.MessageReceived += ircChannel_MessageReceived;
            ircChannel.ModesChanged += ircChannel_ModesChanged;
            ircChannel.TopicChanged += ircChannel_TopicChanged;
            ircChannel.UserJoined += ircChannel_UserJoined;
            ircChannel.UserKicked += ircChannel_UserKicked;
            ircChannel.UserLeft += ircChannel_UserLeft;

            this.ircChannels.Add(channel, ircChannel);
            this.channels.Add(ircChannel, channel);

            INetwork connection = this.connections[ircClient];

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.Add(channel);
            });

            return channel;
        }

        private IUser CreateUser(IrcChannelUser ircChannelUser, IChannel channel)
        {
            IUser user = channel.Users.SingleOrDefault(u => u.Nickname.Equals(ircChannelUser.User.NickName, StringComparison.Ordinal));

            if (user == null)
            {
                user = new User()
                {
                    Nickname = ircChannelUser.User.NickName,
                    Hostname = ircChannelUser.User.HostName,
                    Modes = ircChannelUser.Modes.Join(),
                    IsAway = ircChannelUser.User.IsAway,
                };

                IrcUser ircUser = ircChannelUser.User;

                ircUser.NickNameChanged += ircUser_NickNameChanged;
                ircUser.IsAwayChanged += ircUser_IsAwayChanged;
                ircUser.Quit += ircUser_Quit;

                this.ircChannelUsers.Add(user, ircChannelUser);
                this.channelUsers.Add(ircChannelUser, user);

                this.ircUsers.Add(user, ircUser);
                this.users.Add(ircUser, user);                

                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    channel.Users.Add(user);
                });
            }

            return user;
        }
        #endregion

        #region Destory
        private void DestoryConnection(INetwork connection)
        {
            connection.IsConnected = false;

            IrcClient ircClient = this.ircClients[connection];

            ircClient.Registered -= ircClient_Registered;
            ircClient.Disconnected -= ircClient_Disconnected;
            ircClient.RawMessageReceived -= ircClient_RawMessageReceived;
            ircClient.RawMessageSent -= ircClient_RawMessageSent;

            ircClient.Quit();

            this.ircClients.Remove(connection);
            this.connections.Remove(ircClient);

            foreach (IChannel channel in connection.Channels.ToArray())
            {
                this.DestoryChannel(channel, connection);
            }
        }

        private void DestoryChannel(IChannel channel, INetwork connection)
        {
            IrcChannel ircChannel = this.ircChannels[channel];

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.Remove(channel);
            });

            ircChannel.UsersListReceived -= ircChannel_UsersListReceived;
            ircChannel.MessageReceived -= ircChannel_MessageReceived;
            ircChannel.ModesChanged -= ircChannel_ModesChanged;
            ircChannel.TopicChanged -= ircChannel_TopicChanged;
            ircChannel.UserJoined -= ircChannel_UserJoined;
            ircChannel.UserKicked -= ircChannel_UserKicked;
            ircChannel.UserLeft -= ircChannel_UserLeft;

            this.ircChannels.Remove(channel);
            this.channels.Remove(ircChannel);

            foreach (IUser user in channel.Users.ToArray())
            {
                this.DestoryUser(user, channel);
            }
        }

        private void DestoryUser(IUser user, IChannel channel)
        {
            IrcUser ircUser = this.ircUsers[user];
            IrcChannelUser ircChannelUser = this.ircChannelUsers[user];

            this.ircChannelUsers.Remove(user);
            this.channelUsers.Remove(ircChannelUser);

            this.ircUsers.Remove(user);
            this.users.Remove(ircUser);

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                channel.Users.Remove(user);
            });
        }
        #endregion

        void ircClient_RawMessageReceived(object sender, IrcRawMessageEventArgs e)
        {
            this.OnRawMessage(e.RawContent, RawMessageDirection.Received);
        }

        void ircClient_RawMessageSent(object sender, IrcRawMessageEventArgs e)
        {
            this.OnRawMessage(e.RawContent, RawMessageDirection.Sent);
        }

        void ircChannel_UsersListReceived(object sender, EventArgs e)
        {
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            foreach (IrcChannelUser ircChannelUser in ircChannel.Users)
            {
                IUser user = this.CreateUser(ircChannelUser, channel);
            }
        }

        void ircClient_Registered(object sender, EventArgs e)
        {
            IrcClient ircClient = (IrcClient)sender;
            ircClient.LocalUser.JoinedChannel += LocalUser_JoinedChannel;
            ircClient.LocalUser.NickNameChanged += LocalUser_NicknameChanged;

            INetwork connection = this.connections[ircClient];
            connection.IsConnected = true;
        }

        void ircClient_Disconnected(object sender, EventArgs e)
        {
            IrcClient ircClient = (IrcClient)sender;
            INetwork connection = this.connections[ircClient];

            ircClient.LocalUser.JoinedChannel -= LocalUser_JoinedChannel;
            ircClient.LocalUser.LeftChannel -= LocalUser_LeftChannel;

            this.DestoryConnection(connection);
        }

        void LocalUser_NicknameChanged(object sender, EventArgs e)
        {
            IrcLocalUser ircLocalUser = (IrcLocalUser)sender;
            IrcClient ircClient = ircLocalUser.Client;
            INetwork connection = this.connections[ircClient];
            string oldNickname = connection.LocalUser.Nickname;
            connection.LocalUser.Nickname = ircLocalUser.NickName;

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.ForEach(c=> c.Log.Add(new Nick(connection.LocalUser, oldNickname)));
            });
        }

        void LocalUser_JoinedChannel(object sender, IrcChannelEventArgs e)
        {
            IrcChannel ircChannel = e.Channel;
            IChannel channel = this.CreateChannel(ircChannel);

            this.OnJoinedChannel(channel);
        }
        
        void LocalUser_LeftChannel(object sender, IrcChannelEventArgs e)
        {
            IrcLocalUser ircLocalUser = (IrcLocalUser)sender;
            IrcClient ircClient = ircLocalUser.Client;
            INetwork connection = this.connections[ircClient];
            IrcChannel ircChannel = e.Channel;
            IChannel channel = this.channels[ircChannel];

            this.DestoryChannel(channel, connection);

            this.OnPartedChannel(channel);
        }

        void ircChannel_MessageReceived(object sender, IrcMessageEventArgs e)
        {
            IrcChannel ircChannel = (IrcChannel)sender;
            IrcUser ircUser = (IrcUser)e.Source;
            IChannel channel = this.channels[ircChannel];
            IUser user = this.users[ircUser];

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                channel.Log.Add(new Message(user, e.Text));
            });
        }

        void ircChannel_ModesChanged(object sender, EventArgs e)
        {
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            channel.Modes = ircChannel.Modes.Join();
        }

        void ircChannel_TopicChanged(object sender, EventArgs e)
        {
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            channel.Topic = ircChannel.Topic;
        }

        void ircChannel_UserJoined(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            IUser user = this.CreateUser(ircChannelUser, channel);

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                channel.Log.Add(new Join(user));
            });
        }

        void ircChannel_UserKicked(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcUser ircUser = ircChannelUser.User;
            IUser user = this.users[ircUser];
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            this.DestoryUser(user, channel);

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                channel.Log.Add(new Kick(user, e.Comment));
            });
        }

        void ircChannel_UserLeft(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcUser ircUser = ircChannelUser.User;
            IUser user = this.users[ircUser];
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            this.DestoryUser(user, channel);

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                channel.Log.Add(new Part(user));
            });
        }

        void ircUser_IsAwayChanged(object sender, EventArgs e)
        {
            IrcUser ircUser = (IrcUser)sender;
            IUser user = this.users[ircUser];

            user.IsAway = ircUser.IsAway;
        }

        void ircUser_NickNameChanged(object sender, EventArgs e)
        {
            IrcUser ircUser = (IrcUser)sender;
            INetwork connection = this.connections[ircUser.Client];
            IUser user = this.users[ircUser];

            string oldUsername = user.Nickname;
            user.Nickname = ircUser.NickName;

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.ForEach(c => c.Log.Add(new Nick(user, oldUsername)));
            });
        }

        void ircUser_Quit(object sender, IrcCommentEventArgs e)
        {
            throw new NotImplementedException();

            IrcUser ircUser = (IrcUser)sender;
            INetwork connection = this.connections[ircUser.Client];
            IUser user = this.users[ircUser];

            //TODO handle user quit correctly

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.ForEach(c => c.Log.Add(new Quit(user,e.Comment));
            });
        }

        void ircChannelUser_ModesChanged(object sender, EventArgs e)
        {
            IrcChannelUser ircChannelUser = (IrcChannelUser)sender;
            IUser user = this.channelUsers[ircChannelUser];

            user.Modes = ircChannelUser.Modes.Join();
        }
    }
}
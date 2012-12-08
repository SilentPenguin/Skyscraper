using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using IrcDotNet;
using IrcDotNet.Collections;
using Skyscraper.Models;

namespace Skyscraper.Irc
{
    //AJ: Temp class
    public class JoinedChannelEventArgs : EventArgs 
    {
        public IChannel Channel { get; set; }

        public JoinedChannelEventArgs(IChannel channel) 
        {
            this.Channel = channel;
        }
    }

    internal static class ModesExtensionMethods
    {
        [DebuggerStepThrough]
        internal static string Join(this ReadOnlySet<char> self)
        {
            return new string(self.ToArray());
        }
    }

    //TODO: AJ: Interface
    //TODO: AJ: Move to IrcDotNet specific project
    public class ConnectionManager
    {
        private Dictionary<IConnection, IrcClient> ircClients = new Dictionary<IConnection, IrcClient>();
        private Dictionary<IrcClient, IConnection> connections = new Dictionary<IrcClient, IConnection>();

        private Dictionary<IChannel, IrcChannel> ircChannels = new Dictionary<IChannel, IrcChannel>();
        private Dictionary<IrcChannel, IChannel> channels = new Dictionary<IrcChannel, IChannel>();

        private Dictionary<IUser, IrcChannelUser> ircChannelUsers = new Dictionary<IUser, IrcChannelUser>();
        private Dictionary<IrcChannelUser, IUser> channelUsers = new Dictionary<IrcChannelUser, IUser>();
        private Dictionary<IrcUser, IUser> users = new Dictionary<IrcUser, IUser>();

        public event EventHandler<JoinedChannelEventArgs> JoinedChannel;
        protected virtual void OnJoinedChannel(IChannel channel)
        {
            EventHandler<JoinedChannelEventArgs> eventHandler = this.JoinedChannel;

            if (eventHandler != null)
            {
                eventHandler(this, new JoinedChannelEventArgs(channel));
            }
        }

        public IConnection Connect(INetwork Network)
        {
            IrcUserRegistrationInfo registrationInfo = new IrcUserRegistrationInfo();
            registrationInfo.NickName = "Skyscraper";
            registrationInfo.RealName = "Skyscraper";
            registrationInfo.UserName = "Skyscraper";

            IConnection connection = new Connection();

            IrcClient ircClient = new IrcClient();

            this.ircClients.Add(connection, ircClient);
            this.connections.Add(ircClient, connection);

            ircClient.Registered += ircClient_Registered;
            ircClient.Disconnected += ircClient_Disconnected;
            ircClient.Connect(Network.Url.Host, Network.Url.Port, false, registrationInfo);

            return connection;
        }

        public void Disconnect(IConnection connection, string message = null)
        {
            IrcClient ircClient = this.ircClients[connection];
            ircClient.Quit(message);
        }

        public void Send(IChannel channel, string message)
        {
            IrcChannel ircChannel = this.ircChannels[channel];
            ircChannel.Client.LocalUser.SendMessage(ircChannel, message);

            IUser user = channel.Users.Single(u => u.Nickname.Equals("Skyscraper", StringComparison.Ordinal));

            channel.Log.Add(new Message(user, message));
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
                    IsAway = ircChannelUser.User.IsAway
                };

                IrcUser ircUser = ircChannelUser.User;

                this.users.Add(ircUser, user);
                this.channelUsers.Add(ircChannelUser, user);
                this.ircChannelUsers.Add(user, ircChannelUser);

                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    channel.Users.Add(user);
                });
            }

            return user;
        }

        private IChannel CreateChannel(IrcChannel ircChannel)
        {
            ircChannel.UsersListReceived +=ircChannel_UsersListReceived;
            IrcClient ircClient = ircChannel.Client;

            IChannel channel = new Channel()
            {
                Name = ircChannel.Name,
                Topic = ircChannel.Topic,
                Modes = ircChannel.Modes.Join()
            };
            
            ircChannel.MessageReceived += ircChannel_MessageReceived;
            ircChannel.ModesChanged += ircChannel_ModesChanged;
            ircChannel.TopicChanged += ircChannel_TopicChanged;
            ircChannel.UserJoined += ircChannel_UserJoined;
            ircChannel.UserKicked += ircChannel_UserKicked;
            ircChannel.UserLeft += ircChannel_UserLeft;

            this.ircChannels.Add(channel, ircChannel);
            this.channels.Add(ircChannel, channel);

            IConnection connection = this.connections[ircClient];

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.Add(channel);
            });

            return channel;
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
            ircClient.LocalUser.LeftChannel += LocalUser_LeftChannel;
            ircClient.Channels.Join("#skyscraper");

            IConnection connection = this.connections[ircClient];
            connection.IsConnected = true;
        }

        void ircClient_Disconnected(object sender, EventArgs e)
        {
            IrcClient ircClient = (IrcClient)sender;
            ircClient.LocalUser.JoinedChannel -= LocalUser_JoinedChannel;
            ircClient.LocalUser.LeftChannel -= LocalUser_LeftChannel;

            IConnection connection = this.connections[ircClient];
            connection.IsConnected = false;

            this.ircClients.Remove(connection);
            this.connections.Remove(ircClient);
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
            IrcChannel ircChannel = e.Channel;

            ircChannel.MessageReceived -= ircChannel_MessageReceived;
            ircChannel.ModesChanged -= ircChannel_ModesChanged;
            ircChannel.TopicChanged -= ircChannel_TopicChanged;
            ircChannel.UserJoined -= ircChannel_UserJoined;
            ircChannel.UserKicked -= ircChannel_UserKicked;
            ircChannel.UserLeft -= ircChannel_UserLeft;

            IChannel channel = this.channels[ircChannel];

            this.ircChannels.Remove(channel);
            this.channels.Remove(ircChannel);

            IConnection connection = this.connections[ircClient];

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                connection.Channels.Remove(channel);
            });
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

            ircUser.IsAwayChanged -= ircUser_IsAwayChanged;
            ircUser.NickNameChanged -= ircUser_NickNameChanged;
            ircChannelUser.ModesChanged -= ircChannelUser_ModesChanged;

            Application.Current.Dispatcher.Invoke(() =>
            {
                channel.Users.Remove(user);
            });

            this.users.Remove(ircUser);
            this.channelUsers.Remove(ircChannelUser);
            this.ircChannelUsers.Remove(user);
        }

        void ircChannel_UserLeft(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcUser ircUser = ircChannelUser.User;
            IUser user = this.users[ircUser];
            IrcChannel ircChannel = (IrcChannel)sender;
            IChannel channel = this.channels[ircChannel];

            ircUser.IsAwayChanged -= ircUser_IsAwayChanged;
            ircUser.NickNameChanged -= ircUser_NickNameChanged;
            ircChannelUser.ModesChanged -= ircChannelUser_ModesChanged;
            
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                channel.Users.Remove(user);
            });

            this.users.Remove(ircUser);
            this.channelUsers.Remove(ircChannelUser);
            this.ircChannelUsers.Remove(user);

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

        void ircChannelUser_ModesChanged(object sender, EventArgs e)
        {
            IrcChannelUser ircChannelUser = (IrcChannelUser)sender;
            IUser user = this.channelUsers[ircChannelUser];

            user.Modes = ircChannelUser.Modes.Join();
        }

        void ircUser_NickNameChanged(object sender, EventArgs e)
        {
            IrcUser ircUser = (IrcUser)sender;
            IUser user = this.users[ircUser];

            user.Nickname = ircUser.NickName;
        }
    }
}
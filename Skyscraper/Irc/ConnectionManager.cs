using System;
using System.Linq;
using System.Collections.Generic;
using IrcDotNet;
using Skyscraper.Models;
using IrcDotNet.Collections;
using System.Threading.Tasks;

namespace Skyscraper.Irc
{
    //AJ: Temp class
    public class JoinedChannelEventArgs : EventArgs 
    {
        public Channel Channel { get; set; }

        public JoinedChannelEventArgs(Channel channel) 
        {
            this.Channel = channel;
        }
    }

    public class ConnectionManager
    {
        private Dictionary<Connection, IrcClient> ircClients = new Dictionary<Connection, IrcClient>();
        private Dictionary<IrcClient, Connection> connections = new Dictionary<IrcClient, Connection>();

        private Dictionary<Channel, IrcChannel> ircChannels = new Dictionary<Channel, IrcChannel>();
        private Dictionary<IrcChannel, Channel> channels = new Dictionary<IrcChannel, Channel>();

        private Dictionary<User, IrcChannelUser> ircChannelUsers = new Dictionary<User, IrcChannelUser>();
        private Dictionary<IrcChannelUser, User> channelUsers = new Dictionary<IrcChannelUser, User>();
        private Dictionary<IrcUser, User> users = new Dictionary<IrcUser, User>();

        public event EventHandler<JoinedChannelEventArgs> JoinedChannel;
        protected virtual void OnJoinedChannel(Channel channel)
        {
            EventHandler<JoinedChannelEventArgs> eventHandler = this.JoinedChannel;

            if (eventHandler != null)
            {
                eventHandler(this, new JoinedChannelEventArgs(channel));
            }
        }

        public Connection Connect()
        {
            IrcUserRegistrationInfo registrationInfo = new IrcUserRegistrationInfo();
            registrationInfo.NickName = "Skyscraper";
            registrationInfo.RealName = "Skyscraper";
            registrationInfo.UserName = "Skyscraper";

            Connection connection = new Connection();
            IrcClient ircClient = new IrcClient();

            this.ircClients.Add(connection, ircClient);
            this.connections.Add(ircClient, connection);

            ircClient.Connected += ircClient_Connected;
            ircClient.LocalUser.JoinedChannel += LocalUser_JoinedChannel;
            ircClient.LocalUser.LeftChannel += LocalUser_LeftChannel;

            ircClient.Connect("chat.freenode.net", 6667, false, registrationInfo);

            return connection;
        }

        void ircClient_Connected(object sender, EventArgs e)
        {
            IrcClient ircClient = (IrcClient)sender;
            ircClient.Channels.Join("#skyscraper");
        }

        void LocalUser_JoinedChannel(object sender, IrcChannelEventArgs e)
        {
            IrcClient ircClient = (IrcClient)sender;
            IrcChannel ircChannel = e.Channel;

            Channel channel = new Channel()
            {
                Name = ircChannel.Name,
                Topic = ircChannel.Topic,
                Modes = ircChannel.Modes.Join()
            };

            ircChannel.ModesChanged += ircChannel_ModesChanged;
            ircChannel.TopicChanged += ircChannel_TopicChanged;
            ircChannel.UserJoined += ircChannel_UserJoined;
            ircChannel.UserKicked += ircChannel_UserKicked;
            ircChannel.UserLeft += ircChannel_UserLeft;

            this.ircChannels.Add(channel, ircChannel);
            this.channels.Add(ircChannel, channel);

            Connection connection = this.connections[ircClient];
            connection.Channels.Add(channel);

            
        }
        
        void LocalUser_LeftChannel(object sender, IrcChannelEventArgs e)
        {
            IrcClient ircClient = (IrcClient)sender;
            IrcChannel ircChannel = e.Channel;

            ircChannel.ModesChanged -= ircChannel_ModesChanged;
            ircChannel.TopicChanged -= ircChannel_TopicChanged;
            ircChannel.UserJoined -= ircChannel_UserJoined;
            ircChannel.UserKicked -= ircChannel_UserKicked;
            ircChannel.UserLeft -= ircChannel_UserLeft;

            Channel channel = this.channels[ircChannel];

            this.ircChannels.Remove(channel);
            this.channels.Remove(ircChannel);

            Connection connection = this.connections[ircClient];
            connection.Channels.Remove(channel);
        }

        void ircChannel_ModesChanged(object sender, EventArgs e)
        {
            IrcChannel ircChannel = (IrcChannel)sender;
            Channel channel = this.channels[ircChannel];
            channel.Modes = ircChannel.Modes.Join();
        }

        void ircChannel_TopicChanged(object sender, EventArgs e)
        {
            IrcChannel ircChannel = (IrcChannel)sender;
            Channel channel = this.channels[ircChannel];
            channel.Topic = ircChannel.Topic;
        }

        void ircChannel_UserJoined(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcUser ircUser = ircChannelUser.User;
            IrcChannel ircChannel = (IrcChannel)sender;
            Channel channel = this.channels[ircChannel];

            User user = new User()
            {
                Nickname = ircChannelUser.User.NickName,
                Hostname = ircChannelUser.User.HostName,
                Modes=ircChannelUser.Modes.Join(),
                IsAway = ircChannelUser.User.IsAway
            };

            channel.Users.Add(user);
            this.users.Add(ircUser, user);
            this.channelUsers.Add(ircChannelUser,user);
            this.ircChannelUsers.Add(user, ircChannelUser);

            ircChannelUser.User.IsAwayChanged += ircUser_IsAwayChanged;
            ircChannelUser.User.NickNameChanged += ircUser_NickNameChanged;
            ircChannelUser.ModesChanged += ircChannelUser_ModesChanged;                    
        }

        void ircChannel_UserKicked(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcUser ircUser = ircChannelUser.User;
            User user = this.users[ircUser];
            IrcChannel ircChannel = (IrcChannel)sender;
            Channel channel = this.channels[ircChannel];

            ircUser.IsAwayChanged -= ircUser_IsAwayChanged;
            ircUser.NickNameChanged -= ircUser_NickNameChanged;
            ircChannelUser.ModesChanged -= ircChannelUser_ModesChanged;

            channel.Users.Remove(user);
            this.users.Remove(ircUser);
            this.channelUsers.Remove(ircChannelUser);
            this.ircChannelUsers.Remove(user);
        }

        void ircChannel_UserLeft(object sender, IrcChannelUserEventArgs e)
        {
            IrcChannelUser ircChannelUser = e.ChannelUser;
            IrcUser ircUser = ircChannelUser.User;
            User user = this.users[ircUser];
            IrcChannel ircChannel = (IrcChannel)sender;
            Channel channel = this.channels[ircChannel];

            ircUser.IsAwayChanged -= ircUser_IsAwayChanged;
            ircUser.NickNameChanged -= ircUser_NickNameChanged;
            ircChannelUser.ModesChanged -= ircChannelUser_ModesChanged;

            channel.Users.Remove(user);
            this.users.Remove(ircUser);
            this.channelUsers.Remove(ircChannelUser);
            this.ircChannelUsers.Remove(user);
        }

        void ircUser_IsAwayChanged(object sender, EventArgs e)
        {
            IrcUser ircUser = (IrcUser)sender;
            User user = this.users[ircUser];

            user.IsAway = ircUser.IsAway;
        }

        void ircChannelUser_ModesChanged(object sender, EventArgs e)
        {
            IrcChannelUser ircChannelUser = (IrcChannelUser)sender;
            User user = this.channelUsers[ircChannelUser];

            user.Modes = ircChannelUser.Modes.Join();
        }

        void ircUser_NickNameChanged(object sender, EventArgs e)
        {
            IrcUser ircUser = (IrcUser)sender;
            User user = this.users[ircUser];

            user.Nickname = ircUser.NickName;
        }
    }

    //TODO: AJ: Move to utils?
    public static class ModesExtensionMethods
    {
        public static string Join(this ReadOnlySet<char> self)
        {
            return new string(self.ToArray());
        }
    }
}
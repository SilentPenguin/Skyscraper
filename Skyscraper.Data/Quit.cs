using System;

namespace Skyscraper.Data
{
    public interface IQuit : IUserEvent
    {
        String Message { get; }
    }

    public class Quit : UserEvent, IQuit
    {
        private String message;
        public String Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.SetProperty(ref this.message, value);
            }
        }

        public Quit(IUser user, String message) : base(user) 
        {
            this.Message = message;
        }
    }
}
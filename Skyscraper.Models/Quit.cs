using System;

namespace Skyscraper.Models
{
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

        public Quit(INetwork network, ILogSource source, IUser user, string message)
            : base(network, source, user) 
        {
            this.Message = message;
        }
    }
}
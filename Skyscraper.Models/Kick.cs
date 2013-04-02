namespace Skyscraper.Models
{
    public class Kick : UserEvent, IPart
    {
        public string Message { get; private set; }

        public Kick(INetwork network, ILogSource source, IUser user, string message)
            : base(network, source, user)
        {
            this.Message = message;
        }
    }
}
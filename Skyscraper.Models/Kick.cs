namespace Skyscraper.Models
{
    public class Kick : UserEvent, IPart
    {
        public string Message { get; private set; }

        public Kick(IUser user, string message)
            : base(user)
        {
            this.Message = message;
        }
    }
}
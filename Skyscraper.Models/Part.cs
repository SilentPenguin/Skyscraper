namespace Skyscraper.Models
{
    public class Part : UserEvent, IPart
    {
        public Part(IUser user) : base(user) { }
    }
}
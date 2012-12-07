namespace Skyscraper.Models
{
    public interface IPart : IUserEvent
    {

    }

    class Part : UserEvent, IPart
    {
        public Part(IUser user) : base(user) { }
    }
}
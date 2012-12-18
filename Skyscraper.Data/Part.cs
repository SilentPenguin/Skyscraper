namespace Skyscraper.Data
{
    public interface IPart : IUserEvent
    {

    }

    public class Part : UserEvent, IPart
    {
        public Part(IUser user) : base(user) { }
    }
}
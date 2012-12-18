namespace Skyscraper.Data
{
    public class Part : UserEvent, IPart
    {
        public Part(IUser user) : base(user) { }
    }
}
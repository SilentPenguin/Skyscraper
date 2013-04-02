namespace Skyscraper.Models
{
    public class Part : UserEvent, IPart
    {
        public Part(INetwork network, ILogSource source, IUser user)
            : base(network, source, user) { }
    }
}
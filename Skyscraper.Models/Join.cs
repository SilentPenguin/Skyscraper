namespace Skyscraper.Models
{
    public class Join : UserEvent
    {
        public Join(INetwork network, ILogSource source, IUser user) 
            : base(network, source, user) { }
    }
}
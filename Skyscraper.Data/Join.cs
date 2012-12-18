namespace Skyscraper.Data
{
    public class Join : UserEvent
    {
        public Join(IUser user) : base(user) { }
    }
}
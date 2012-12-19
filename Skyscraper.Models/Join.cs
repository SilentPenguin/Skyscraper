namespace Skyscraper.Models
{
    public class Join : UserEvent
    {
        public Join(IUser user) : base(user) { }
    }
}
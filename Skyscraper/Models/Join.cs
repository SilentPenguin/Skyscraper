namespace Skyscraper.Models
{
    public interface IJoin : IUserEvent
    {
        
    }

    public class Join : UserEvent
    {
        public Join(IUser user) : base(user) { }
    }
}

namespace Skyscraper.Models
{
    public class Nick : UserEvent
    {
        public Nick(IUser user, string oldNickname) 
            : base(user) 
        {
            base.NicknameContinuity = oldNickname;
        }
    }
}
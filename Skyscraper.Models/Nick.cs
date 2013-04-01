namespace Skyscraper.Models
{
    public class Nick : UserEvent, INick
    {
        public Nick(INetwork network, ILogSource source, IUser user, string oldNickname)
            : base(network, source, user)
        {
            base.NicknameContinuity = oldNickname;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.Models
{
    public interface IQuit : IUserEvent
    {
        String Message { get; }
    }

    public class Quit : UserEvent, IQuit
    {
        public Quit(IUser User, String Message) : base(User) { }
        public String Message { get; set; }
    }
}

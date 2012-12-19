using System;

namespace Skyscraper.Models
{
    public interface IUserEvent : ILogEntry
    {
        IUser User { get; }
        String NicknameContinuity { get; }
    }
}
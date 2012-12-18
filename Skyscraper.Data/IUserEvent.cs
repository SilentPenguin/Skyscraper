using System;

namespace Skyscraper.Data
{
    public interface IUserEvent : ILogEntry
    {
        IUser User { get; }
        String NicknameContinuity { get; }
    }
}
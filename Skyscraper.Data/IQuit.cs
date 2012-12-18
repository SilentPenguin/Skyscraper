using System;

namespace Skyscraper.Data
{
    public interface IQuit : IUserEvent
    {
        String Message { get; }
    }
}
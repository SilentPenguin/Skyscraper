using System;

namespace Skyscraper.Models
{
    public interface IQuit : IUserEvent
    {
        String Message { get; }
    }
}
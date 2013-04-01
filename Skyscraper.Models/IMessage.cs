using System;

namespace Skyscraper.Models
{
    public interface IMessage : IUserEvent
    {
        String MessageBody { get; }
    }
}
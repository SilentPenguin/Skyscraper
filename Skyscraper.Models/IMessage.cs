using System;

namespace Skyscraper.Models
{
    public interface IMessage : IUserEvent
    {
        string MessageBody { get; }
        bool Highlight { get; } 
    }
}
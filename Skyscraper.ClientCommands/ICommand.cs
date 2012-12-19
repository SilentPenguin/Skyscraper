using System;

namespace Skyscraper.Models
{
    public interface ICommand
    {
        String Text { get; }
        String Body { get; }
        CommandType Type { get; }
        String[] Arguments { get; }
    }
}
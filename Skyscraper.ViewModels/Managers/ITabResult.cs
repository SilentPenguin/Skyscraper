using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.ViewModels.Behaviours
{
    public interface ITabResult
    {
        IChannel Channel { get; }
        string Text { get; }
    }
}

using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.ViewModels.Managers
{
    public interface ITabResult
    {
        IChannel Channel { get; }
        Range SelectedText { get; }
        string Text { get; }
        int CursorIndex { get; }
    }
}

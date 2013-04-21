using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.ViewModels.Managers
{
    public interface ITabQuery
    {
        string ReplaceKeyword(string match);
        int GetCursorIndexAtEndOfKeyword(string newCommand);
        string Keyword{ get; }
        string Text { get; }
        int CursorLocation { get; }
    }
}

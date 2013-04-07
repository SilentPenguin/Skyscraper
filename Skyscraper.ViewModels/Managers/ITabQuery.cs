using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Skyscraper.ViewModels.Behaviours
{
    public interface ITabQuery
    {
        string ReplaceKeyword(string match);
        string Keyword{ get; }
        string Text { get; }
        int CursorLocation { get; }
    }
}

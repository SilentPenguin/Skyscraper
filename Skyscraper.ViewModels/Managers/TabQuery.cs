using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Managers
{
    public class TabQuery : ITabQuery
    {
        readonly string[] seperators = new string[] { " ", "/", ":", ".", "," };
        public string ReplaceKeyword(string match) {
            StringBuilder builder = new StringBuilder(this.text);
            builder.Replace(this.Keyword, match);
            return builder.ToString();
        }

        public int GetCursorIndexAtEndOfKeyword(string newCommand)
        {
            int nextSpace = this.seperators.Min(s => this.text.IndexOf(newCommand, this.cursorLocation));
            return nextSpace < newCommand.Length && nextSpace >= 0 ? nextSpace : newCommand.Length;
        }

        public string Keyword
        {
            get
            {
                int previousSpace = this.seperators.Max(s => this.text.LastIndexOf(s, this.cursorLocation)) + 1;
                int nextSpace = this.seperators.Min(s => this.text.IndexOf(s, this.cursorLocation));

                nextSpace = nextSpace < this.text.Length && nextSpace >= 0 ? nextSpace : this.text.Length;

                return this.text.Substring(previousSpace, nextSpace - previousSpace);
            }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        private int cursorLocation;
        public int CursorLocation
        {
            get { return this.cursorLocation; }
            set { this.cursorLocation = value; }
        }
    }
}

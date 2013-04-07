using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours
{
    public class TabQuery : ITabQuery
    {
        public string ReplaceKeyword(string match) {
            StringBuilder builder = new StringBuilder(this.text);
            builder.Replace(this.Keyword, match);
            return builder.ToString();
        }

        public string Keyword
        {
            get
            {
                int previousSpace = this.text.LastIndexOf(" ", this.cursorLocation);
                int nextSpace = this.text.IndexOf(" ", this.cursorLocation);
                return this.text.Substring(previousSpace, nextSpace);
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

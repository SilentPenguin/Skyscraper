using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Managers
{
    public class TabResult : ITabResult
    {
        private IChannel channel;
        public IChannel Channel
        {
            get { return this.channel; }
            set { this.channel = value; }
        }

        private Range selectedText;
        public Range SelectedText
        {
            get { return this.selectedText; }
            set { this.selectedText = value; }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        private int cursorIndex;
        public int CursorIndex
        {
            get { return this.cursorIndex; }
            set { this.cursorIndex = value; }
        }
    }
}

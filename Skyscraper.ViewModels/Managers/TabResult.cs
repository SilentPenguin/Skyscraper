﻿using Skyscraper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours
{
    public class TabResult : ITabResult
    {
        private IChannel channel;
        public IChannel Channel
        {
            get { return this.channel; }
            set { this.channel = value; }
        }

        private string text;
        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }
    }
}
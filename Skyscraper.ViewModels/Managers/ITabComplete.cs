﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscraper.ViewModels.Behaviours
{
    public interface ITabComplete
    {
        ITabResult GetTabResults(ITabQuery query);
    }
}

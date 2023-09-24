﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Senparc.Xncf.DaprClient
{
    public class DaprClientConfigOptions
    {
        public int HttpApiPort { get; set; } = 3500;
        public string StateStoreName { get; set; }
        public string PubSubName { get; set; }
    }
}

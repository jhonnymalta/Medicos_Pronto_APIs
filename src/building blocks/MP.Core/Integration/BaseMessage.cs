﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.Core.Integration
{
    public class BaseMessage
    {
        public long Id { get; set; }
        public DateTime MessageCreated { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Faker
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FakerIgnoreAttribute : Attribute { }
    
}

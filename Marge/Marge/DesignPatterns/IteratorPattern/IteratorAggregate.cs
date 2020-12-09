﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marge.DesignPatterns.IteratorPattern
{
    public abstract class IteratorAggregate : Aggregate
    {
        public abstract IEnumerator GetEnumerator();
    }
}

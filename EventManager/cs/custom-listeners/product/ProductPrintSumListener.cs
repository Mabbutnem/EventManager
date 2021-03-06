﻿using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager
{
   class ProductPrintSumListener : Listener<Product>, IListener<int, int>
   {
      private static readonly Logger logger = LogManager.GetCurrentClassLogger();

      public void Invoke(int t1, int t2)
      {
         int sum = t1 + t2;
         logger.Trace("Sum : " + sum);
      }
   }
}

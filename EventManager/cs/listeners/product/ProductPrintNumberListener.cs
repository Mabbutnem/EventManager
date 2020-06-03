using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager
{
   class ProductPrintNumberListener : Listener<Product>, IListener
   {
      private static readonly Logger logger = LogManager.GetCurrentClassLogger();

      public int Number { get; set; }

      public void Invoke()
      {
         logger.Trace("Number : " + Number);
      }
   }
}

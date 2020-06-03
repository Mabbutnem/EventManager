using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace Event.Listeners
{
   class ElementPrintNumberListener : Listener<Element>, IListener
   {
      private static readonly Logger logger = LogManager.GetCurrentClassLogger();

      public int Number { get; set; }

      public void Invoke()
      {
         logger.Trace("Number : " + Number);
      }
   }
}

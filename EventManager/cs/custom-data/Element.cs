using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EventManager
{
   class Element
   {
      public int Id { get; }
      public EventAndListenerPair<Element>[] Listeners { get; }

      [JsonConstructor]
      public Element(int id, EventAndListenerPair<Element>[] listeners)
      {
         Id = id;

         Listeners = listeners;
         foreach (EventAndListenerPair<Element> pair in Listeners)
         {
            pair.Listener.Subject = this;
         }
      }
   }
}

using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EventManager
{
   class Product
   {
      public string Name { get; }
      public EventAndListenerPair<Product>[] Listeners { get; }

      [JsonConstructor]
      public Product(string name, EventAndListenerPair<Product>[] listeners)
      {
         Name = name;

         Listeners = listeners;
         foreach (EventAndListenerPair<Product> pair in Listeners)
         {
            pair.Listener.Subject = this;
         }
      }
   }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EventManager
{
   class ElementJsonRepo : IElementRepo
   {
      public Element[] GetAll()
      {
         using (StreamReader r = new StreamReader("../../../Resources/Elements.json"))
         {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Element[]>(json);
         }
      }
   }
}

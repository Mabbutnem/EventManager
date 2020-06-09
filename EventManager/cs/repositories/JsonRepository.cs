using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EventManager
{
   class JsonRepository<T> : IRepository<T>
   {
      private readonly string file;

      public JsonRepository(string file)
      {
         this.file = file;
      }

      public T[] FindAll()
      {
         using StreamReader r = new StreamReader("../../../Resources/" + file + ".json");
         string json = r.ReadToEnd();
         return JsonConvert.DeserializeObject<T[]>(json);
      }
   }
}

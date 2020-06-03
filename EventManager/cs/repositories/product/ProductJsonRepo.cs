using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EventManager
{
   class ProductJsonRepo : IProductRepo
   {
      public Product[] GetAll()
      {
         using StreamReader r = new StreamReader("../../../Resources/Products.json");
         string json = r.ReadToEnd();
         return JsonConvert.DeserializeObject<Product[]>(json);
      }
   }
}

using Event.Listeners;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Event
{
   class Program
   {
      static void Main(string[] args)
      {
         LogConf.SetLogConf();

         Product[] products = new Product[] { };
         Element[] elements = new Element[] { };

         using (StreamReader r = new StreamReader("../../../Resources/Products.json"))
         {
            string json = r.ReadToEnd();
            products = JsonConvert.DeserializeObject<Product[]>(json);
         }
         using (StreamReader r = new StreamReader("../../../Resources/Elements.json"))
         {
            string json = r.ReadToEnd();
            elements = JsonConvert.DeserializeObject<Element[]>(json);
         }

         Client client = new Client();

         foreach (Product p in products) { client.AddProduct(p); }
         foreach (Element e in elements) { client.AddElement(e); }

         client.DoAction1();
         client.DoAction2();
         client.DoAction3();
         client.DoAction4(2, 2);
         client.DoAction5(50, 24);

         foreach (Product p in products) { client.RemoveProduct(p); }
         foreach (Element e in elements) { client.RemoveElement(e); }

         client.DoAction1();
         client.DoAction2();
         client.DoAction3();
         client.DoAction4(2, 2);
         client.DoAction5(50, 24);
      }
   }
}

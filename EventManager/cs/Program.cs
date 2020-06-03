using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace EventManager
{
   class Program
   {
      static void Main(string[] args)
      {
         LogConf.SetLogConf();

         IProductRepo productRepo = new ProductJsonRepo();
         IElementRepo elementRepo = new ElementJsonRepo();

         Product[] products = productRepo.GetAll();
         Element[] elements = elementRepo.GetAll();

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

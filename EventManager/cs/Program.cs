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

         IRepository<Product> productRepo = new JsonRepository<Product>("Products");
         IRepository<Element> elementRepo = new JsonRepository<Element>("Elements");

         Product[] products = productRepo.FindAll();
         Element[] elements = elementRepo.FindAll();

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

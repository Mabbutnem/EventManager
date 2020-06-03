using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager
{
   interface IProductRepo
   {
      Product[] GetAll();
   }
}

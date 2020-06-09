using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager
{
   interface IRepository<T>
   {
      T[] FindAll();
   }
}

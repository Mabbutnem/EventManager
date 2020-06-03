using System;
using System.Collections.Generic;
using System.Text;

namespace Event.Listeners
{
   interface IBaseListener { }

   interface IListener : IBaseListener
   {
      void Invoke();
   }
   interface IListener<T> : IBaseListener
   {
      void Invoke(T t);
   }
   interface IListener<T1, T2> : IBaseListener
   {
      void Invoke(T1 t1, T2 t2);
   }
   interface IListener<T1, T2, T3> : IBaseListener
   {
      void Invoke(T1 t1, T2 t2, T3 t3);
   }
   interface IListener<T1, T2, T3, T4> : IBaseListener
   {
      void Invoke(T1 t1, T2 t2, T3 t3, T4 t4);
   }
   interface IListener<T1, T2, T3, T4, T5> : IBaseListener
   {
      void Invoke(T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
   }
}

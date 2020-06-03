using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Text;

namespace EventManager
{
   class Client
   {
      public List<Product> Products { get; } = new List<Product>();
      public List<Element> Elements { get; } = new List<Element>();

      #region Add Components
      public void AddProduct(Product product)
      {
         Products.Add(product);
         SubAll(product.Listeners);
      }

      public void RemoveProduct(Product product)
      {
         Products.Remove(product);
         UnsubAll(product.Listeners);
      }

      public void AddElement(Element elem)
      {
         Elements.Add(elem);
         SubAll(elem.Listeners);
      }

      public void RemoveElement(Element elem)
      {
         Elements.Remove(elem);
         UnsubAll(elem.Listeners);
      }
      #endregion

      #region Sub
      private void SubAll<U>(EventAndListenerPair<U>[] listeners)
      {
         foreach (EventAndListenerPair<U> pair in listeners)
         {
            switch (pair.Event)
            {
               case EventType.EVENT_1:
                  EventManager.Subscriber.Sub<U>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_2:
                  EventManager.Subscriber.Sub<U>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_3:
                  EventManager.Subscriber.Sub<U>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_4:
                  EventManager.Subscriber.Sub<U, int, int>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_5:
                  EventManager.Subscriber.Sub<U, int, int>(pair.Event, pair.Listener);
                  break;
            }
         }
      }

      private void UnsubAll<U>(EventAndListenerPair<U>[] listeners)
      {
         foreach (EventAndListenerPair<U> pair in listeners)
         {
            switch (pair.Event)
            {
               case EventType.EVENT_1:
                  EventManager.Subscriber.Unsub<U>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_2:
                  EventManager.Subscriber.Unsub<U>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_3:
                  EventManager.Subscriber.Unsub<U>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_4:
                  EventManager.Subscriber.Unsub<U, int, int>(pair.Event, pair.Listener);
                  break;
               case EventType.EVENT_5:
                  EventManager.Subscriber.Unsub<U, int, int>(pair.Event, pair.Listener);
                  break;
            }
         }
      }
      #endregion

      #region Actions
      public void DoAction1()
      {
         EventManager.Invoker.Invoke(EventType.EVENT_1);
      }

      public void DoAction2()
      {
         EventManager.Invoker.Invoke(EventType.EVENT_2);
      }

      public void DoAction3()
      {
         EventManager.Invoker.Invoke(EventType.EVENT_3);
      }

      public void DoAction4(int i1, int i2)
      {
         EventManager.Invoker.Invoke(EventType.EVENT_4, i1, i2);
      }

      public void DoAction5(int i1, int i2)
      {
         EventManager.Invoker.Invoke(EventType.EVENT_5, i1, i2);
      }
      #endregion
   }
}

using Event.Listeners;
using NLog;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;

namespace Event
{
   #region Interface
   interface IEventInvoker
   {
      void Invoke(EventType @event);
      void Invoke<T>(EventType @event, T t);
      void Invoke<T1, T2>(EventType @event, T1 t1, T2 t2);
      void Invoke<T1, T2, T3>(EventType @event, T1 t1, T2 t2, T3 t3);
      void Invoke<T1, T2, T3, T4>(EventType @event, T1 t1, T2 t2, T3 t3, T4 t4);
      void Invoke<T1, T2, T3, T4, T5>(EventType @event, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5);
   }

   interface IEventSubscriber
   {
      void Sub<U>(EventType @event, Listener<U> listenerU);
      void Sub<U, T>(EventType @event, Listener<U> listenerU);
      void Sub<U, T1, T2>(EventType @event, Listener<U> listenerU);
      void Sub<U, T1, T2, T3>(EventType @event, Listener<U> listenerU);
      void Sub<U, T1, T2, T3, T4>(EventType @event, Listener<U> listenerU);
      void Sub<U, T1, T2, T3, T4, T5>(EventType @event, Listener<U> listenerU);

      void Unsub<U>(EventType @event, Listener<U> listenerU);
      void Unsub<U, T>(EventType @event, Listener<U> listenerU);
      void Unsub<U, T1, T2>(EventType @event, Listener<U> listenerU);
      void Unsub<U, T1, T2, T3>(EventType @event, Listener<U> listenerU);
      void Unsub<U, T1, T2, T3, T4>(EventType @event, Listener<U> listenerU);
      void Unsub<U, T1, T2, T3, T4, T5>(EventType @event, Listener<U> listenerU);
   }
   #endregion

   #region EventManager
   class EventManager
   {
      public static IEventInvoker Invoker { get; } = LogConf.LOG_EVENT ? (IEventInvoker)new LoggedEventInvoker() : new EventInvoker();
      public static IEventSubscriber Subscriber { get; } = LogConf.LOG_SUB ? (IEventSubscriber)new LoggedEventSubscriber() : new EventSubscriber();

      private static EventGetSet GetSet { get; } = new EventGetSet();

      private EventManager() { }

      #region EventGetter
      // On suppose que les getters et setters sont bien utilisés (l'utilisateur connait les arguments que possède un event)
      internal class EventGetSet
      {
         private readonly EventContainer eventContainer = EventContainer.Instance;

         public Action Get(EventType @event)
         {
            return (Action)eventContainer.Events[@event];
         }

         public Action<T> Get<T>(EventType @event)
         {
            return (Action<T>)eventContainer.Events[@event];
         }

         public Action<T1, T2> Get<T1, T2>(EventType @event)
         {
            return (Action<T1, T2>)eventContainer.Events[@event];
         }

         public Action<T1, T2, T3> Get<T1, T2, T3>(EventType @event)
         {
            return (Action<T1, T2, T3>)eventContainer.Events[@event];
         }

         public Action<T1, T2, T3, T4> Get<T1, T2, T3, T4>(EventType @event)
         {
            return (Action<T1, T2, T3, T4>)eventContainer.Events[@event];
         }

         public Action<T1, T2, T3, T4, T5> Get<T1, T2, T3, T4, T5>(EventType @event)
         {
            return (Action<T1, T2, T3, T4, T5>)eventContainer.Events[@event];
         }

         public void Set(EventType @event, Action action)
         {
            eventContainer.Events[@event] = action;
         }

         public void Set<T>(EventType @event, Action<T> action)
         {
            eventContainer.Events[@event] = action;
         }

         public void Set<T1, T2>(EventType @event, Action<T1, T2> action)
         {
            eventContainer.Events[@event] = action;
         }

         public void Set<T1, T2, T3>(EventType @event, Action<T1, T2, T3> action)
         {
            eventContainer.Events[@event] = action;
         }

         public void Set<T1, T2, T3, T4>(EventType @event, Action<T1, T2, T3, T4> action)
         {
            eventContainer.Events[@event] = action;
         }

         public void Set<T1, T2, T3, T4, T5>(EventType @event, Action<T1, T2, T3, T4, T5> action)
         {
            eventContainer.Events[@event] = action;
         }
      }
      #endregion

      #region EventInvoker
      internal class EventInvoker : IEventInvoker
      {
         public void Invoke(EventType @event)
         {
            GetSet.Get(@event)();
         }

         public void Invoke<T>(EventType @event, T t)
         {
            GetSet.Get<T>(@event)(t);
         }

         public void Invoke<T1, T2>(EventType @event, T1 t1, T2 t2)
         {
            GetSet.Get<T1, T2>(@event)(t1, t2);
         }

         public void Invoke<T1, T2, T3>(EventType @event, T1 t1, T2 t2, T3 t3)
         {
            GetSet.Get<T1, T2, T3>(@event)(t1, t2, t3);
         }

         public void Invoke<T1, T2, T3, T4>(EventType @event, T1 t1, T2 t2, T3 t3, T4 t4)
         {
            GetSet.Get<T1, T2, T3, T4>(@event)(t1, t2, t3, t4);
         }

         public void Invoke<T1, T2, T3, T4, T5>(EventType @event, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
         {
            GetSet.Get<T1, T2, T3, T4, T5>(@event)(t1, t2, t3, t4, t5);
         }
      }
      #endregion

      #region LoggedEventInvoker
      internal class LoggedEventInvoker : IEventInvoker
      {
         private readonly ILogger logger = LogManager.GetLogger("EventInvoker");
         private readonly IEventInvoker proxy = new EventInvoker();

         public void Invoke(EventType @event)
         {
            LogEnter(@event, GetSet.Get(@event).GetInvocationList().Length - 1);
            proxy.Invoke(@event);
            LogExit(@event);
         }

         public void Invoke<T>(EventType @event, T t)
         {
            LogEnter(@event, GetSet.Get<T>(@event).GetInvocationList().Length - 1);
            proxy.Invoke(@event, t);
            LogExit(@event);
         }

         public void Invoke<T1, T2>(EventType @event, T1 t1, T2 t2)
         {
            LogEnter(@event, GetSet.Get<T1, T2>(@event).GetInvocationList().Length - 1);
            proxy.Invoke(@event, t1, t2);
            LogExit(@event);
         }

         public void Invoke<T1, T2, T3>(EventType @event, T1 t1, T2 t2, T3 t3)
         {
            LogEnter(@event, GetSet.Get<T1, T2, T3>(@event).GetInvocationList().Length - 1);
            proxy.Invoke(@event, t1, t2, t3);
            LogExit(@event);
         }

         public void Invoke<T1, T2, T3, T4>(EventType @event, T1 t1, T2 t2, T3 t3, T4 t4)
         {
            LogEnter(@event, GetSet.Get<T1, T2, T3, T4>(@event).GetInvocationList().Length - 1);
            proxy.Invoke(@event, t1, t2, t3, t4);
            LogExit(@event);
         }

         public void Invoke<T1, T2, T3, T4, T5>(EventType @event, T1 t1, T2 t2, T3 t3, T4 t4, T5 t5)
         {
            LogEnter(@event, GetSet.Get<T1, T2, T3, T4, T5>(@event).GetInvocationList().Length - 1);
            proxy.Invoke(@event, t1, t2, t3, t4, t5);
            LogExit(@event);
         }

         private void LogEnter(EventType @event, int subNb)
         {
            logger.Debug("Enter " + @event + " : (" + subNb + " sub)");
         }

         private void LogExit(EventType @event)
         {
            logger.Debug("Exit " + @event + " ----------");
         }
      }
      #endregion

      #region EventSubscriber
      internal class EventSubscriber : IEventSubscriber
      {
         private readonly ILogger logger = LogManager.GetLogger("EventSubscriber");

         public void Sub<U>(EventType @event, Listener<U> listenerU)
         {
            Action action = GetSet.Get(@event);
            if (listenerU is IListener)
            {
               action += ((IListener)listenerU).Invoke;
               GetSet.Set(@event, action);
            }
            else { LogSubWarn(listenerU, @event); }
         }

         public void Sub<U, T>(EventType @event, Listener<U> listenerU)
         {
            Action<T> action = GetSet.Get<T>(@event);
            if (listenerU is IListener<T>)
            {
               action += ((IListener<T>)listenerU).Invoke;
               GetSet.Set<T>(@event, action);
            }
            else { LogSubWarn(listenerU, @event); }
         }

         public void Sub<U, T1, T2>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2> action = GetSet.Get<T1, T2>(@event);
            if (listenerU is IListener<T1, T2>)
            {
               action += ((IListener<T1, T2>)listenerU).Invoke;
               GetSet.Set<T1, T2>(@event, action);
            }
            else { LogSubWarn(listenerU, @event); }
         }

         public void Sub<U, T1, T2, T3>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2, T3> action = GetSet.Get<T1, T2, T3>(@event);
            if (listenerU is IListener<T1, T2, T3>)
            {
               action += ((IListener<T1, T2, T3>)listenerU).Invoke;
               GetSet.Set<T1, T2, T3>(@event, action);
            }
            else { LogSubWarn(listenerU, @event); }
         }

         public void Sub<U, T1, T2, T3, T4>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2, T3, T4> action = GetSet.Get<T1, T2, T3, T4>(@event);
            if (listenerU is IListener<T1, T2, T3, T4>)
            {
               action += ((IListener<T1, T2, T3, T4>)listenerU).Invoke;
               GetSet.Set<T1, T2, T3, T4>(@event, action);
            }
            else { LogSubWarn(listenerU, @event); }
         }

         public void Sub<U, T1, T2, T3, T4, T5>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2, T3, T4, T5> action = GetSet.Get<T1, T2, T3, T4, T5>(@event);
            if (listenerU is IListener<T1, T2, T3, T4, T5>)
            {
               action += ((IListener<T1, T2, T3, T4, T5>)listenerU).Invoke;
               GetSet.Set<T1, T2, T3, T4, T5>(@event, action);
            }
            else { LogSubWarn(listenerU, @event); }
         }

         public void Unsub<U>(EventType @event, Listener<U> listenerU)
         {
            Action action = GetSet.Get(@event);
            if (listenerU is IListener)
            {
               action -= ((IListener)listenerU).Invoke;
               GetSet.Set(@event, action);
            }
            else { LogUnsubWarn(listenerU, @event); }
         }

         public void Unsub<U, T>(EventType @event, Listener<U> listenerU)
         {
            Action<T> action = GetSet.Get<T>(@event);
            if (listenerU is IListener<T>)
            {
               action -= ((IListener<T>)listenerU).Invoke;
               GetSet.Set<T>(@event, action);
            }
            else { LogUnsubWarn(listenerU, @event); }
         }

         public void Unsub<U, T1, T2>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2> action = GetSet.Get<T1, T2>(@event);
            if (listenerU is IListener<T1, T2>)
            {
               action -= ((IListener<T1, T2>)listenerU).Invoke;
               GetSet.Set<T1, T2>(@event, action);
            }
            else { LogUnsubWarn(listenerU, @event); }
         }

         public void Unsub<U, T1, T2, T3>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2, T3> action = GetSet.Get<T1, T2, T3>(@event);
            if (listenerU is IListener<T1, T2, T3>)
            {
               action -= ((IListener<T1, T2, T3>)listenerU).Invoke;
               GetSet.Set<T1, T2, T3>(@event, action);
            }
            else { LogUnsubWarn(listenerU, @event); }
         }

         public void Unsub<U, T1, T2, T3, T4>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2, T3, T4> action = GetSet.Get<T1, T2, T3, T4>(@event);
            if (listenerU is IListener<T1, T2, T3, T4>)
            {
               action -= ((IListener<T1, T2, T3, T4>)listenerU).Invoke;
               GetSet.Set<T1, T2, T3, T4>(@event, action);
            }
            else { LogUnsubWarn(listenerU, @event); }
         }

         public void Unsub<U, T1, T2, T3, T4, T5>(EventType @event, Listener<U> listenerU)
         {
            Action<T1, T2, T3, T4, T5> action = GetSet.Get<T1, T2, T3, T4, T5>(@event);
            if (listenerU is IListener<T1, T2, T3, T4, T5>)
            {
               action -= ((IListener<T1, T2, T3, T4, T5>)listenerU).Invoke;
               GetSet.Set<T1, T2, T3, T4, T5>(@event, action);
            }
            else { LogUnsubWarn(listenerU, @event); }
         }

         private void LogSubWarn<U>(Listener<U> listenerU, EventType @event)
         {
            logger.Warn(listenerU + " can't subscribe to " + @event);
         }

         private void LogUnsubWarn<U>(Listener<U> listenerU, EventType @event)
         {
            logger.Warn(listenerU + " can't unsubscribe from " + @event);
         }
      }
      #endregion

      #region LoggedEventSubscriber
      internal class LoggedEventSubscriber : IEventSubscriber
      {
         private readonly ILogger logger = LogManager.GetLogger("EventSubscriber");
         private readonly IEventSubscriber proxy = new EventSubscriber();

         public void Sub<U>(EventType @event, Listener<U> listenerU)
         {
            LogSub(listenerU, @event);
            proxy.Sub<U>(@event, listenerU);
         }

         public void Sub<U, T>(EventType @event, Listener<U> listenerU)
         {
            LogSub(listenerU, @event);
            proxy.Sub<U, T>(@event, listenerU);
         }

         public void Sub<U, T1, T2>(EventType @event, Listener<U> listenerU)
         {
            LogSub(listenerU, @event);
            proxy.Sub<U, T1, T2>(@event, listenerU);
         }

         public void Sub<U, T1, T2, T3>(EventType @event, Listener<U> listenerU)
         {
            LogSub(listenerU, @event);
            proxy.Sub<U, T1, T2, T3>(@event, listenerU);
         }

         public void Sub<U, T1, T2, T3, T4>(EventType @event, Listener<U> listenerU)
         {
            LogSub(listenerU, @event);
            proxy.Sub<U, T1, T2, T3, T4>(@event, listenerU);
         }

         public void Sub<U, T1, T2, T3, T4, T5>(EventType @event, Listener<U> listenerU)
         {
            LogSub(listenerU, @event);
            proxy.Sub<U, T1, T2, T3, T4, T5>(@event, listenerU);
         }

         public void Unsub<U>(EventType @event, Listener<U> listenerU)
         {
            LogUnsub(listenerU, @event);
            proxy.Unsub<U>(@event, listenerU);
         }

         public void Unsub<U, T>(EventType @event, Listener<U> listenerU)
         {
            LogUnsub(listenerU, @event);
            proxy.Unsub<U, T>(@event, listenerU);
         }

         public void Unsub<U, T1, T2>(EventType @event, Listener<U> listenerU)
         {
            LogUnsub(listenerU, @event);
            proxy.Unsub<U, T1, T2>(@event, listenerU);
         }

         public void Unsub<U, T1, T2, T3>(EventType @event, Listener<U> listenerU)
         {
            LogUnsub(listenerU, @event);
            proxy.Unsub<U, T1, T2, T3>(@event, listenerU);
         }

         public void Unsub<U, T1, T2, T3, T4>(EventType @event, Listener<U> listenerU)
         {
            LogUnsub(listenerU, @event);
            proxy.Unsub<U, T1, T2, T3, T4>(@event, listenerU);
         }

         public void Unsub<U, T1, T2, T3, T4, T5>(EventType @event, Listener<U> listenerU)
         {
            LogUnsub(listenerU, @event);
            proxy.Unsub<U, T1, T2, T3, T4, T5>(@event, listenerU);
         }

         private void LogSub<U>(Listener<U> listenerU, EventType @event)
         {
            logger.Debug(listenerU + " subscribing to " + @event + "...");
         }

         private void LogUnsub<U>(Listener<U> listenerU, EventType @event)
         {
            logger.Debug(listenerU + " unsubscribing from " + @event + "...");
         }
      }
      #endregion
   }
   #endregion
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Event
{
   #region Event Names
   enum EventType
   {
      #region Client
      EVENT_1,
      EVENT_2,
      EVENT_3,
      EVENT_4,
      EVENT_5
      #endregion
   }
   #endregion

   #region Event Container
   class EventContainer
   {
      private EventContainer() { }
      public static EventContainer Instance = new EventContainer();

      public Dictionary<EventType, Delegate> Events = new Dictionary<EventType, Delegate>()
      {
         #region Client
         {EventType.EVENT_1, new Action(() => { }) },
         {EventType.EVENT_2, new Action(() => { }) },
         {EventType.EVENT_3, new Action(() => { }) },
         {EventType.EVENT_4, new Action<int, int>((i1, i2) => { }) },
         {EventType.EVENT_5, new Action<int, int>((i1, i2) => { }) },
         #endregion
      };
   }
   #endregion
}

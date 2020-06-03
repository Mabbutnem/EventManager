using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventManager
{
   #region SubTypes
   [JsonConverter(typeof(JsonSubtypes), "Type")]

   #region Product
   [JsonSubtypes.KnownSubType(typeof(ProductPrintNumberListener), "ProductPrintNumberListener")]
   [JsonSubtypes.KnownSubType(typeof(ProductPrintSumListener),    "ProductPrintSumListener")]
   #endregion

   #region Element
   [JsonSubtypes.KnownSubType(typeof(ElementPrintNumberListener), "ElementPrintNumberListener")]
   [JsonSubtypes.KnownSubType(typeof(ElementPrintSumListener),    "ElementPrintSumListener")]
   #endregion

   #endregion

   abstract class Listener<T> : IBaseListener
   {
      public string Type { get => GetType().Name; }
      [JsonIgnore] public T Subject { get; set; }
   }
}

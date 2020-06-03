using NLog;

namespace Event
{
   class LogConf
   {
      public static readonly bool LOG_EVENT = true;
      public static readonly bool LOG_SUB = false;
      public static readonly LogLevel LOG_LEVEL = LogLevel.Trace;

      #region At Start
      public static void SetLogConf()
      {
         var config = new NLog.Config.LoggingConfiguration();

         var console = new NLog.Targets.ColoredConsoleTarget("console");
         config.AddRule(LOG_LEVEL, LogLevel.Fatal, console);

         NLog.LogManager.Configuration = config;
      }
      #endregion
   }
}

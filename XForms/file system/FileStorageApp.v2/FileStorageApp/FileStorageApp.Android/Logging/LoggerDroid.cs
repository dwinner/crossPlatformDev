using System;
using Android.Util;
using FileStorageApp.Logging;

namespace FileStorageApp.Droid.Logging
{
   public class LoggerDroid : ILogger
   {
      public void WriteLine(string message) => Log.WriteLine(LogPriority.Info, message, null);

      public void WriteLineTime(string message, params object[] args) => Log.WriteLine(LogPriority.Info,
         $"{DateTime.Now.Ticks} {string.Format(message, args)}", null);
   }
}
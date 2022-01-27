// --------------------------------------------------------------------------------------------------
//  <copyright file="LogiOS.cs" company="Health Connex">
//    Copyright (c) 2014 Health Connex All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Camera.Portable.Logging;

namespace Camera.iOS.Logging
{
   /// <summary>
   ///    iOS log.
   /// </summary>
   public class LoggeriOS : ILogger
   {
      #region IDebug implementation

      /// <summary>
      ///    Writes the line.
      /// </summary>
      /// <returns>The line.</returns>
      /// <param name="text">Text.</param>
      public void WriteLine(string text)
      {
         Debug.WriteLine(text);
      }

      /// <summary>
      ///    Writes the line time.
      /// </summary>
      /// <returns>The line time.</returns>
      /// <param name="text">Text.</param>
      /// <param name="args">Arguments.</param>
      public void WriteLineTime(string text, params object[] args)
      {
         Debug.WriteLine(DateTime.Now.Ticks + " " + string.Format(text, args));
      }

      #endregion
   }
}
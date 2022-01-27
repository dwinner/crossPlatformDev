// --------------------------------------------------------------------------------------------------
//  <copyright file="BoolToPartialConverter.cs" company="Flush Arcade Pty Ltd.">
//    Copyright (c) 2014 Flush Arcade Pty Ltd. All rights reserved.
//  </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Linq;
using Camera.Portable.Ioc;
using Camera.Portable.Logging;
using Xamarin.Forms;

namespace Camera.Converters
{
   /// <summary>
   ///    Bool to partial converter.
   /// </summary>
   public class BoolToPartialConverter : IValueConverter
   {
      #region Public Methods

      /// <summary>
      ///    Converts the back.
      /// </summary>
      /// <returns>The back.</returns>
      /// <param name="value">Value.</param>
      /// <param name="targetType">Target type.</param>
      /// <param name="parameter">Parameter.</param>
      /// <param name="culture">Culture.</param>
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         try
         {
            var str = parameter as string;

            if (str != null)
            {
               // split string by ',', convert to int and store in case list
               var cases = str.Split(',').Select(x => double.Parse(x)).ToList();

               if (value is bool)
               {
                  return (bool) value ? cases[0] : cases[1];
               }
            }
         }
         catch (Exception error)
         {
            IoC.Resolve<ILogger>().WriteLineTime("BoolToPartialConverter \n" +
                                                 "Convert() Failed to switch flash on/off \n " +
                                                 "ErrorMessage: \n" +
                                                 error.Message + "\n" +
                                                 "Stacktrace: \n " +
                                                 error.StackTrace);
         }

         return 0;
      }

      /// <summary>
      ///    Converts the back.
      /// </summary>
      /// <returns>The back.</returns>
      /// <param name="value">Value.</param>
      /// <param name="targetType">Target type.</param>
      /// <param name="parameter">Parameter.</param>
      /// <param name="culture">Culture.</param>
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
         throw new NotImplementedException();

      #endregion
   }
}
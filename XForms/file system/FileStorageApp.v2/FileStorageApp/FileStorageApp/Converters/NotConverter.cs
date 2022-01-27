using System;
using System.Globalization;
using Xamarin.Forms;

namespace FileStorageApp.Converters
{
   public class NotConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var boolValue = value as bool?;
         return boolValue != null ? !boolValue : value;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
         throw new NotImplementedException();
   }
}
using System;
using System.Globalization;
using Xamarin.Forms;

namespace PaperBoy.Common.Converters
{
   public sealed class LableFontWeightConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
         ((string) value).Contains("Facebook") ? FontAttributes.Bold : FontAttributes.None;

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
   }

   public sealed class AgoLabelConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var articleDateTime = (DateTime) value;
         var minDifference = (int) (DateTime.Now.ToUniversalTime() - articleDateTime).TotalMinutes;

         return minDifference > 60 ? "more than an houre ago" : minDifference + " minutes ago";
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => null;
   }

   public sealed class SelectedItemConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var eventArgs = value as SelectedItemChangedEventArgs;
         return eventArgs.SelectedItem;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
         throw new NotImplementedException();
   }
}
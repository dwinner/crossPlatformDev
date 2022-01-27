using System;
using System.Globalization;
using Xamarin.Forms;

namespace IslandMenu.App.Helpers
{
   public class ImageResourceConverter : IValueConverter
   {
      private const string Ns = "IslandMenu.App";

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         var imageSource = ImageSource.FromResource($"{Ns}.Images.{value ?? string.Empty}");
         return imageSource;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotSupportedException();
      }
   }
}
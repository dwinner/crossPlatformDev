using Xamarin.Forms;

namespace StockListApp.Xam.Controls
{
   public class CustomLabel : Label
   {
      public static readonly BindableProperty AndroidFontStyleProperty =
         BindableProperty.Create(nameof(AndroidFontStyle), typeof(string), typeof(CustomLabel), string.Empty);

      public string AndroidFontStyle
      {
         get => (string) GetValue(AndroidFontStyleProperty);
         set => SetValue(AndroidFontStyleProperty, value);
      }
   }
}
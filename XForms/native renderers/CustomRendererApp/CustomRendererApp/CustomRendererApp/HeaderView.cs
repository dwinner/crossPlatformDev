using System;
using Xamarin.Forms;

namespace CustomRendererApp
{
   public class HeaderView : View
   {
      public static readonly BindableProperty TextProperty =
         BindableProperty.Create(nameof(Text), typeof(string), typeof(HeaderView), string.Empty);

      public static readonly BindableProperty TextColorProperty =
         BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(HeaderView), Color.Default);

      public string Text
      {
         get => (string) GetValue(TextProperty);
         set => SetValue(TextProperty, value);
      }

      public Color TextColor
      {
         get => (Color) GetValue(TextColorProperty);
         set => SetValue(TextColorProperty, value);
      }

      public event EventHandler TappedOrClickEvent;

      public void FireClick() => TappedOrClickEvent?.Invoke(this, EventArgs.Empty);
   }
}
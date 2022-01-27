using Xamarin.Essentials;
using Xamarin.Forms;

namespace MvvxSandboxApp.UI.Controls
{
   public class HyperlinkSpan : Span
   {
      public static readonly BindableProperty UrlProperty =
         BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan));

      public HyperlinkSpan()
      {
         TextDecorations = TextDecorations.Underline;
         TextColor = Color.Blue;
         GestureRecognizers.Add(new TapGestureRecognizer
         {
            Command = new Command(async () => await Launcher.OpenAsync(Url).ConfigureAwait(false))
         });
      }

      public string Url
      {
         get => (string) GetValue(UrlProperty);
         set => SetValue(UrlProperty, value);
      }
   }
}
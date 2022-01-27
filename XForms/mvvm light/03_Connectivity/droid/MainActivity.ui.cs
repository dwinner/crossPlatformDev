using Android.Webkit;

namespace connectivity.Droid
{
   public partial class MainActivity
   {
      private WebView _webView;

      public WebView WebView => _webView ?? (_webView = FindViewById<WebView>(Resource.Id.webView));
   }
}
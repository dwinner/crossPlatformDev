using System;
using Xamarin.Forms.Xaml;

namespace IntroducingPages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class NavigationSample
   {
      public NavigationSample()
      {
         InitializeComponent();
      }

      private async void Button1_Clicked(object sender, EventArgs e)
      {
         await Navigation.PushAsync(new SecondaryPage());
      }
   }
}
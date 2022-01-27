using System;
using Xamarin.Forms.Xaml;

namespace IntroducingPages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SecondaryPage
   {
      public SecondaryPage()
      {
         InitializeComponent();
      }

      private async void Button1_Clicked(object sender, EventArgs e)
      {
         await Navigation.PopAsync();
      }

      protected override bool OnBackButtonPressed()
      {
         return base.OnBackButtonPressed();
      }
   }
}
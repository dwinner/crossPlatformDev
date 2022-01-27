using System.ComponentModel;
using UsingMAT.App.Interfaces;
using UsingMAT.App.Resources;
using Xamarin.Forms;

namespace UsingMAT.App
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         englishButton.Clicked += (sender, args) => { ChangeCulture("en"); };
         spanishButton.Clicked += (sender, args) => { ChangeCulture("es"); };
         chineseButton.Clicked += (sender, args) => { ChangeCulture("zh-Hans"); };

         UpdateText();
      }

      private void UpdateText()
      {
         welcomeLabel.Text = MainPageText.Welcome ?? "Welcome to my translated app";
      }

      private void ChangeCulture(string culture)
      {
         DependencyService.Get<ICultureOverride>().SetCultureOverride(culture);
         UpdateText();
      }
   }
}
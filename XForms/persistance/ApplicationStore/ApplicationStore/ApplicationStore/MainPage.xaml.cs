using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ApplicationStore
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      private const string NameKey = "name";

      public MainPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         if (Application.Current.Properties.TryGetValue(NameKey, out var name))
         {
            nameBox.Text = (string) name;
         }

         base.OnAppearing();
      }

      private void OnSave(object sender, EventArgs e)
      {
         var value = nameBox.Text;
         Application.Current.Properties[NameKey] = value;
      }
   }
}
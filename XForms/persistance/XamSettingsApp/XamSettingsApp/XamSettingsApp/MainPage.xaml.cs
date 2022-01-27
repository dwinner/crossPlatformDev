using System;
using System.ComponentModel;
using Plugin.Settings;

namespace XamSettingsApp
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
         var name = CrossSettings.Current.GetValueOrDefault(NameKey, "missing");
         nameBox.Text = name;
         base.OnAppearing();
      }

      private void OnSave(object sender, EventArgs e)
      {
         var value = nameBox.Text;
         CrossSettings.Current.AddOrUpdateValue(NameKey, value);
      }
   }
}
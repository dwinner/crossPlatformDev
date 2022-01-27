using System;
using System.ComponentModel;

namespace CustomRendererApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      private int _clicks;

      public MainPage()
      {
         InitializeComponent();
      }

      private void OnChangeText(object sender, EventArgs e)
      {
         if (sender is HeaderView headerView)
         {
            headerView.Text = $"{++_clicks} clicks";
         }
      }
   }
}
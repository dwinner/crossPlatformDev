using System;
using System.ComponentModel;
using AuthDemoXForms.Models;
using Xamarin.Forms;

namespace AuthDemoXForms.Views
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class NewItemPage : ContentPage
   {
      public NewItemPage()
      {
         InitializeComponent();

         Item = new Item
         {
            Text = "Item name",
            Description = "This is an item description."
         };

         BindingContext = this;
      }

      public Item Item { get; set; }

      private async void Save_Clicked(object sender, EventArgs e)
      {
         MessagingCenter.Send(this, "AddItem", Item);
         await Navigation.PopModalAsync();
      }

      private async void Cancel_Clicked(object sender, EventArgs e)
      {
         await Navigation.PopModalAsync();
      }
   }
}
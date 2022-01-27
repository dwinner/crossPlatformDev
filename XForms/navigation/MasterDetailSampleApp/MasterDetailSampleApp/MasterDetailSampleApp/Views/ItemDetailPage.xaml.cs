using System.ComponentModel;
using MasterDetailSampleApp.Models;
using MasterDetailSampleApp.ViewModels;

namespace MasterDetailSampleApp.Views
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class ItemDetailPage
   {
      public ItemDetailPage(ItemDetailViewModel viewModel)
      {
         InitializeComponent();
         BindingContext = viewModel;
      }

      public ItemDetailPage()
      {
         InitializeComponent();

         var item = new Item
         {
            Text = "Item 1",
            Description = "This is an item description."
         };

         var viewModel = new ItemDetailViewModel(item);
         BindingContext = viewModel;
      }
   }
}
using ShellDuo.Models;
using ShellDuo.ViewModels;
using Xamarin.Forms;

namespace ShellDuo.Views
{
   public partial class NewItemPage : ContentPage
   {
      public NewItemPage()
      {
         InitializeComponent();
         BindingContext = new NewItemViewModel();
      }

      public Item Item { get; set; }
   }
}
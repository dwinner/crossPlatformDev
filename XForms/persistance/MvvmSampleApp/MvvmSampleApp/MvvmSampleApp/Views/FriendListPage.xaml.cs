using MvvmSampleApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace MvvmSampleApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FriendListPage
   {
      public FriendListPage()
      {
         InitializeComponent();
         BindingContext = new FriendListViewModel {Navigation = Navigation};
      }
   }
}
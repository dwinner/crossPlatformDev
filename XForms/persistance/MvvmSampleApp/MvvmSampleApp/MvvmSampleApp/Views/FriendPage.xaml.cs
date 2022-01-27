using MvvmSampleApp.ViewModels;
using Xamarin.Forms.Xaml;

namespace MvvmSampleApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class FriendPage
   {
      public FriendPage(FriendViewModel friendViewModel)
      {
         InitializeComponent();
         ViewModel = friendViewModel;
         BindingContext = ViewModel;
      }

      public FriendViewModel ViewModel { get; }
   }
}
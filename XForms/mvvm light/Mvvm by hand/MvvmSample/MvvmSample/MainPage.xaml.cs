using MvvmSample.ViewModel;

namespace MvvmSample
{
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         ViewModel = new PersonViewModel();
         BindingContext = ViewModel;
      }

      // Not using a field here because properties
      // are optimized for data binding
      private PersonViewModel ViewModel { get; }
   }
}
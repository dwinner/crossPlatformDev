using SpeechTalk.App.ViewModels;

namespace SpeechTalk.App.Pages
{
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      public MainPage([NotNull] MainPageViewModel model)
      {
         BindingContext = model;
         InitializeComponent();
      }
   }
}

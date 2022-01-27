using MvvmCross.Forms.Presenters.Attributes;

namespace StarWarsSample.Forms.UI.Views
{
   [MvxModalPresentation(WrapInNavigationPage = true)]
   public partial class StatusPage
   {
      public StatusPage()
      {
         InitializeComponent();
      }
   }
}
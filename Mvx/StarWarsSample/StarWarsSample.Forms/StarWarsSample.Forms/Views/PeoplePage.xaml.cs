using MvvmCross.Forms.Presenters.Attributes;

namespace StarWarsSample.Forms.UI.Views
{
   [MvxMasterDetailPagePresentation(
      WrapInNavigationPage = true,
      NoHistory = true)]
   public partial class PeoplePage
   {
      public PeoplePage()
      {
         InitializeComponent();
      }
   }
}
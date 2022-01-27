using System;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.Forms.Views;
using Xamarin.Forms;

namespace StarWarsSample.Forms.UI.Views
{
   [MvxMasterDetailPagePresentation(MasterDetailPosition.Master)]
   public partial class MenuPage
   {
      public MenuPage()
      {
         InitializeComponent();
      }

      private void ToggleClicked(object sender, EventArgs e)
      {
         if (Parent is MvxMasterDetailPage mvxMasterDetailPage && Device.RuntimePlatform != Device.UWP)
         {
            mvxMasterDetailPage.MasterBehavior = MasterBehavior.Popover;
            mvxMasterDetailPage.IsPresented = !mvxMasterDetailPage.IsPresented;
         }
      }
   }
}
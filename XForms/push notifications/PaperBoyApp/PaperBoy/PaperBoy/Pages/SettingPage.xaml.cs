using System;
using PaperBoy.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PaperBoy.Pages
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SettingPage : ContentPage
   {
      public SettingPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         InitializaeSetting();
         base.OnAppearing();
      }

      private void InitializaeSetting()
      {
         BindingContext = App.ViewModel;
         articleCounterSlider.Value = 10;
         categoryPicker.SelectedIndex = 1;

         var label = GeneralHelper.GetLabel();
         var extendedLabel = GeneralHelper.GetLabel("Running PaperBoy on", true);

         App.ViewModel.PlatformLabel = label;
         App.ViewModel.ExtendedPlatformLabel = extendedLabel;

         var orientation = GeneralHelper.GetOrientation();
         App.ViewModel.DeviceOrientation = orientation;
      }

      private void OnSaveClicked(object sender, EventArgs e)
      {
         Application.Current.Resources["ListTextColor"] = Color.Blue;
      }
   }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using JetBrains.Annotations;
using StockList.Core.UI;
using StockList.Core.ViewModels;
using StockListApp.Xam.UI;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockListApp.Xam.Pages
{
   [DesignTimeVisible(false)]
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class MainPage : INavigableXamarinFormsPage
   {
      /// <summary>
      ///    The stocklist command property
      /// </summary>
      public static readonly BindableProperty StocklistCommandProperty =
         BindableProperty.Create(nameof(StocklistCommand), typeof(ICommand), typeof(MainPage));

      /// <summary>
      ///    The exit command property
      /// </summary>
      public static readonly BindableProperty ExitCommandProperty =
         BindableProperty.Create(nameof(ExitCommand), typeof(ICommand), typeof(MainPage));

      private readonly ControlTemplate _blackTemplate;
      private bool _originalTemplate = true;
      private readonly ControlTemplate _whiteTemplate;

      public MainPage()
      {
         InitializeComponent();

         _blackTemplate = (ControlTemplate) Application.Current.Resources["MainBlackTemplate"];
         _whiteTemplate = (ControlTemplate) Application.Current.Resources["MainWhiteTemplate"];
      }

      [UsedImplicitly]
      public MainPage(MainPageViewModel model)
         : this() => BindingContext = model;

      public ICommand StocklistCommand => (ICommand) GetValue(StocklistCommandProperty);

      public ICommand ExitCommand => (ICommand) GetValue(ExitCommandProperty);

      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);

      private void OnChangeTheme(object sender, EventArgs e)
      {
         _originalTemplate = !_originalTemplate;
         ControlTemplate = _originalTemplate ? _blackTemplate : _whiteTemplate;
         BackgroundColor = _originalTemplate ? Color.Black : Color.White;
      }
   }
}
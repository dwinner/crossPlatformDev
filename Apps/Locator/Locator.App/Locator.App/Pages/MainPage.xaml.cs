using System.Collections.Generic;
using Locator.App.UI;
using Locator.Common.ViewModels;

namespace Locator.App.Pages
{
   /// <summary>
   ///    Main page.
   /// </summary>
   public partial class MainPage : INavigableXamarinFormsPage
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.App.Pages.MainPage" /> class.
      /// </summary>
      public MainPage() => InitializeComponent();

      /// <summary>
      ///    Initializes a new instance of the <see cref="T:Locator.App.Pages.MainPage" /> class.
      /// </summary>
      /// <param name="model">Model.</param>
      public MainPage(MainPageViewModel model)
      {
         BindingContext = model;
         InitializeComponent();
      }

      /// <summary>
      ///    Called when page is navigated to.
      /// </summary>
      /// <returns>The navigated to.</returns>
      /// <param name="navigationParameters">Navigation parameters.</param>
      public void OnNavigatedTo(IDictionary<string, object> navigationParameters) => this.Show(navigationParameters);
   }
}
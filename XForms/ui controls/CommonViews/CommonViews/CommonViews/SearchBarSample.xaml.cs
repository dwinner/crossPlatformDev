using System;
using Xamarin.Forms.Xaml;

namespace CommonViews
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SearchBarSample
   {
      public SearchBarSample()
      {
         InitializeComponent();
      }

      private void SearchBar1_SearchButtonPressed(object sender, EventArgs e)
      {
         // Handle text here...
      }
   }
}
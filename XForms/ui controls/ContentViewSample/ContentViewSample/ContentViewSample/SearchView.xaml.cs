using System;
using Xamarin.Forms.Xaml;

namespace ContentViewSample
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SearchView
   {
      public SearchView() => InitializeComponent();

      public event EventHandler<string> Search;

      protected virtual void OnSearch(string e) => Search?.Invoke(this, e);

      private void OnClicked(object sender, EventArgs e) => OnSearch(searchEntry.Text);
   }
}
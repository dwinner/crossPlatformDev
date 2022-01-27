using System;
using Xamarin.Forms.Xaml;

namespace CommonViews
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class WorkingWithText
   {
      public WorkingWithText()
      {
         InitializeComponent();
      }

      private void Entry1_Completed(object sender, EventArgs e)
      {
         // Handle text changes here..
      }
   }
}
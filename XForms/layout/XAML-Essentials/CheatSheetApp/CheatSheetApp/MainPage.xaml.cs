using System;
using System.ComponentModel;

namespace CheatSheetApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      private int _index;

      private readonly string[] _quotes =
      {
         "Life is like riding a bicycle. To keep your balance, you must keep moving.",
         "You can't blame gravity for falling in love.",
         "Look deep into nature, and then you will understand everything better."
      };

      public MainPage()
      {
         InitializeComponent();
         currentQuoteLabel.Text = _quotes[_index];
      }

      private void Button_OnClicked(object sender, EventArgs e)
      {
         _index++;
         if (_index >= _quotes.Length)
         {
            _index = 0;
         }

         currentQuoteLabel.Text = _quotes[_index];
      }
   }
}
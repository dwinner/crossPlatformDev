using System;
using Xamarin.Forms.Xaml;

namespace CommonViews
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ImageWithGestureRecognizerSample
   {
      public ImageWithGestureRecognizerSample()
      {
         InitializeComponent();
      }

      private void ImageTap_Tapped(object sender, EventArgs e)
      {
         // Image has been tapped
      }
   }
}
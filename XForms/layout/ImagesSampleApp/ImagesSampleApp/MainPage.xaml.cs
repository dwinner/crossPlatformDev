using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ImagesSampleApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      private int _currentImageId = 1;

      public MainPage()
      {
         InitializeComponent();
         LoadImage();
      }

      private void LoadImage()
      {
         galleryImage.Source = new UriImageSource
         {
            Uri = new Uri($"http://lorempixel.com/1920/1080/city/{_currentImageId}/"),
            CachingEnabled = false
         };
      }

      private void OnPrev(object sender, EventArgs e)
      {
         _currentImageId--;
         if (_currentImageId == 0)
         {
            _currentImageId = 10;
         }

         LoadImage();
      }

      private void OnNext(object sender, EventArgs e)
      {
         _currentImageId++;
         if (_currentImageId == 11)
         {
            _currentImageId = 1;
         }

         LoadImage();
      }
   }
}
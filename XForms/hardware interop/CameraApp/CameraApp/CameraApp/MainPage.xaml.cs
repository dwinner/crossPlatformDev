using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CameraApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();

         // Photo selection
         getPhotoButton.Clicked += async (sender, args) =>
         {
            var photo = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);
            if (photo?.Path != null)
            {
               photoImage.Source = ImageSource.FromFile(photo.Path);
            }
         };

         // Taking photo
         takePhotoButton.Clicked += async (sender, args) =>
         {
            await CrossMedia.Current.Initialize().ConfigureAwait(true);

            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
               var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
               {
                  SaveToAlbum = true,
                  Directory = "Sample",
                  Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
               }).ConfigureAwait(true);

               if (file == null)
               {
                  return;
               }

               photoImage.Source = ImageSource.FromFile(file.Path);
            }
         };
      }
   }
}
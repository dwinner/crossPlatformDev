using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Gallery.Droid.Bitmap;
using Gallery.Droid.Utils;
using AppIds = Gallery.Droid.Resource.Id;
using AppLayouts = Gallery.Droid.Resource.Layout;

// ReSharper disable AvoidAsyncVoid

namespace Gallery.Droid
{
   /// <summary>
   ///    Main activity.
   /// </summary>
   [Activity(Label = "Gallery", Icon = "@drawable/icon")]
   public class PhotoActivity : AppCompatActivity
   {
      /// <summary>
      ///    Raises the create event.
      /// </summary>
      /// <param name="savedInstanceState">Saved instance state.</param>
      protected override async void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(AppLayouts.photo);

         var imageData = Intent.GetByteArrayExtra(IntentData.ImageData);
         var title = Intent.GetStringExtra(IntentData.Title) ?? string.Empty;
         var date = Intent.GetStringExtra(IntentData.Date) ?? string.Empty;

         // set image
         var imageView = FindViewById<ImageView>(AppIds.image_photo);
         await BitmapHelpers.CreateBitmapAsync(imageView, imageData).ConfigureAwait(true);

         // set labels
         var titleTextView = FindViewById<TextView>(AppIds.title_photo);
         titleTextView.Text = title;
         var dateTextView = FindViewById<TextView>(AppIds.date_photo);
         dateTextView.Text = date;
      }
   }
}
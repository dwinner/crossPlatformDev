using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Gallery.Droid.Adapters;
using Gallery.Droid.Utils;
using static Gallery.Droid.Utils.PermissionHelpers;

namespace Gallery.Droid
{
   /// <summary>
   ///    Main activity.
   /// </summary>
   [Activity(Label = "Gallery.Droid", MainLauncher = true, Icon = "@drawable/icon")]
   public class MainActivity : AppCompatActivity
   {
      /// <summary>
      ///    The adapter.
      /// </summary>
      private ListAdapter _adapter;

      /// <summary>
      ///    Raises the create event.
      /// </summary>
      /// <param name="savedInstanceState">Saved instance state.</param>
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Set our view from the "main" layout resource
         SetContentView(Resource.Layout.main);

         // Check permission
         PermissionHelpers.CheckPermission(this, Manifest.Permission.ReadExternalStorage, "External storage",
            ReadExternalStorage);

         _adapter = new ListAdapter(this);
         var listView = FindViewById<ListView>(Resource.Id.listView);
         listView.Adapter = _adapter;

         listView.ItemClick += (sender, e) =>
         {
            var galleryItem = _adapter.GetItemByPosition(e.Position);
            var photoActivity = new Intent(this, typeof(PhotoActivity));
            photoActivity.PutExtra(IntentData.ImageData, galleryItem.ImageData);
            photoActivity.PutExtra(IntentData.Title, galleryItem.Title);
            photoActivity.PutExtra(IntentData.Date, galleryItem.Date);
            StartActivity(photoActivity);
         };
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
      {
         switch (requestCode)
         {
            case ReadExternalStorage:
               if (grantResults[0] == Permission.Granted)
               {
                  // Ok - permission granted
               }
               else
               {
                  Toast.MakeText(this, "Read external stotage denied", ToastLength.Long).Show();
               }

               break;

            default:
               base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
               break;
         }
      }
   }
}
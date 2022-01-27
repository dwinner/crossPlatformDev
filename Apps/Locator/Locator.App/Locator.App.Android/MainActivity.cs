using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using Locator.App.Droid.Modules;
using Locator.App.Modules;
using Locator.Common.IoC;
using Locator.Common.Modules;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Locator.App.Droid
{
   /// <summary>
   ///    Main activity.
   /// </summary>
   [Activity(
      Label = "Locator",
      Icon = "@drawable/icon",
      Theme = "@style/MainTheme",
      MainLauncher = true,
      ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
   public class MainActivity : FormsAppCompatActivity
   {
      private static readonly Dictionary<RequestCodes, (string permission, string alertMessage)>
         LocationPermissionMap = new Dictionary<RequestCodes, (string permission, string alertMessage)>
         {
            {
               RequestCodes.WriteExternalStorage,
               (Manifest.Permission.WriteExternalStorage, "Need coarse location permission")
            },
            {
               RequestCodes.AccessFineLocation,
               (Manifest.Permission.AccessFineLocation, "Need fine location permission")
            },
            {
               RequestCodes.AccessCoarseLocation,
               (Manifest.Permission.AccessCoarseLocation, "Need network state permission")
            },
            {
               RequestCodes.AccessNetworkState,
               (Manifest.Permission.AccessNetworkState, "Need network state permission")
            }
         };

      /// <summary>
      ///    Inits the IoC container and modules.
      /// </summary>
      /// <returns>The io c.</returns>
      private static void InitIoC()
      {
         IoC.CreateContainer();
         IoC.RegisterModule(new DroidModule());
         IoC.RegisterModule(new SharedModule(false));
         IoC.RegisterModule(new XamFormsModule());
         IoC.RegisterModule(new PortableModule());
         IoC.StartContainer();
      }

      /// <summary>
      ///    Called when the activity is created.
      /// </summary>
      /// <returns>The create.</returns>
      /// <param name="bundle">Bundle.</param>
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         Forms.Init(this, bundle);
         FormsMaps.Init(this, bundle);
         InitIoC();
         LoadApplication(new App());
      }

      protected override void OnResume()
      {
         base.OnResume();

         // Request required permissions
         foreach (var (requestCode, (permission, message)) in LocationPermissionMap)
         {
            RequestPermission(permission, message, requestCode);
         }
      }

      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
      {
         var code = (RequestCodes) requestCode;
         switch (code)
         {
            case RequestCodes.WriteExternalStorage:
            case RequestCodes.AccessFineLocation:
            case RequestCodes.AccessCoarseLocation:
            case RequestCodes.AccessNetworkState:
               if (grantResults.Length > 0 && grantResults[0] != Permission.Granted)
               {
                  Toast.MakeText(this, "Permission denied", ToastLength.Long).Show();
               }

               break;

            default:
               base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
               break;
         }
      }

      private void RequestPermission(string permission, string alertMessage, RequestCodes locationRequestCode)
      {
         if (CheckSelfPermission(permission) == Permission.Granted)
         {
            return;
         }

         if (ShouldShowRequestPermissionRationale(permission))
         {
            new AlertDialog.Builder(this).SetMessage(alertMessage)
               .SetPositiveButton(Android.Resource.String.Ok,
                  (sender, args) => RequestPermissions(new[] {permission}, (int) locationRequestCode))
               .Create()
               .Show();
         }
         else
         {
            RequestPermissions(new[] {permission}, (int) locationRequestCode);
         }
      }

      private enum RequestCodes : short
      {
         WriteExternalStorage = 1,
         AccessFineLocation = 2,
         AccessCoarseLocation = 3,
         AccessNetworkState = 4
      }
   }
}
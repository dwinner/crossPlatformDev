using Android.App;
using Android.Content.PM;
using Android.OS;
using AlertDialog = Android.Support.V7.App.AlertDialog;

namespace Gallery.Droid.Utils
{
   internal static class PermissionHelpers
   {
      public const int ReadExternalStorage = 1;

      public static void CheckPermission(Activity context, string permission, string permissionMessage, int requestCode)
      {
         var currentApiVersion = Build.VERSION.SdkInt;
         if (currentApiVersion < BuildVersionCodes.M)
         {
            return;
         }

         if (context.CheckSelfPermission(permission) == Permission.Granted)
         {
            return;
         }

         if (context.ShouldShowRequestPermissionRationale(permission))
         {
            ShowPermissionDialog(permissionMessage, context, permission);
         }
         else
         {
            context.RequestPermissions(new[] {permission}, requestCode);
         }
      }

      private static void ShowPermissionDialog(string message, Activity context, string permission) =>
         new AlertDialog.Builder(context)
            .SetCancelable(true)
            .SetTitle("Permission required")
            .SetMessage($"{message} permission required")
            .SetPositiveButton(Android.Resource.String.Yes,
               (sender, args) => { context.RequestPermissions(new[] {permission}, ReadExternalStorage); })
            .Create()
            .Show();
   }
}
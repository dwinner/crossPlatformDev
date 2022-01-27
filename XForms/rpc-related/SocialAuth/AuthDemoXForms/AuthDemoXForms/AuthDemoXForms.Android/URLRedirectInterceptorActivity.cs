﻿using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using AuthDemoXForms.ViewModels;

namespace AuthDemoXForms.Droid
{
   [Activity(Label = "URLRedirectInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
   [IntentFilter(
      new[] {Intent.ActionView},
      Categories = new[] {Intent.CategoryDefault, Intent.CategoryBrowsable},
      DataSchemes = new[]
      {
         "com.googleusercontent.apps.29...-7aecvn..."
      },
      DataPath = "/oauth2redirect")]
   public class URLRedirectInterceptorActivity : Activity
   {
      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);

         // Convert Android.Net.Url to Uri
         var uri = new Uri(Intent.Data.ToString());

         // Load redirectUrl page
         AuthenticationState.Authenticator.OnPageLoading(uri);

         //var intent = new Intent(this, typeof(MainActivity));
         //intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
         //StartActivity(intent);

         Finish();
      }
   }
}
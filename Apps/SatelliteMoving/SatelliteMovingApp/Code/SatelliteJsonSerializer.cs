using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Newtonsoft.Json;

namespace SatelliteMovingApp.Code
{
   public sealed class SatelliteJsonSerializer
   {
      private const string SatellitePrefKey = nameof(SatellitePrefKey);
      private const string SharedPreferenceFileName = nameof(SatelliteJsonSerializer);

      private readonly Activity _activity;

      public SatelliteJsonSerializer(Activity activity) => _activity = activity;

      public IList<Satellite> Read()
      {
         var satellites = Array.Empty<Satellite>();
         var sharedPreferences = _activity.GetSharedPreferences(SharedPreferenceFileName, FileCreationMode.Private);
         if (sharedPreferences.Contains(SatellitePrefKey))
         {
            var jsonSatellites = sharedPreferences.GetString(SatellitePrefKey, string.Empty);
            if (!string.IsNullOrEmpty(jsonSatellites))
               satellites = JsonConvert.DeserializeObject<Satellite[]>(jsonSatellites);
         }

         return satellites;
      }

      public void Save(IList<Satellite> satellites)
      {
         var satelliteJson = JsonConvert.SerializeObject(satellites, Formatting.Indented);
         var sharedPreferences = _activity.GetSharedPreferences(SharedPreferenceFileName, FileCreationMode.Private);
         var editor = sharedPreferences.Edit();
         editor.PutString(SatellitePrefKey, satelliteJson);
         editor.Commit();
      }
   }
}
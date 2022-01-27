/**
 * Управляет фрагментом MainActivityFragment на телефоне и фрагментом
 * MainActivityFragment и SettingsActivityFragment на планшете
 */

using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Preferences;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using static FlagQuiz.App.Resource;
using JavaObj = Java.Lang.Object;
using Orientation = Android.Content.Res.Orientation;
using Toolbar = Android.Support.V7.Widget.Toolbar;

// ReSharper disable BitwiseOperatorOnEnumWithoutFlags

namespace FlagQuiz.App
{
   [Activity(
      //Name = "FlagQuiz.App.MainActivity",
      Label = "@string/app_name",
      LaunchMode = LaunchMode.SingleTop,
      Theme = "@style/AppTheme.NoActionBar",
      MainLauncher = true)]
   public class MainActivity : AppCompatActivity
   {
      // Ключи для чтения данных из SharedPreferences
      internal const string Choices = "pref_numberOfChoices";
      internal const string Regions = "pref_regionsToInclude";
      private const string QuizFragmentTag = "quizFragment";

      private bool _phoneDevice = true; // Включение портретного режима
      private bool _preferencesChanged = true; // Настройки изменились

      protected override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         SetContentView(Layout.activity_main);

         var toolbar = FindViewById<Toolbar>(Id.toolbar);
         SetSupportActionBar(toolbar);

         // Задание значений про умолчанию в файле SharedPreferences
         PreferenceManager.SetDefaultValues(this, Xml.preferences, false);

         // Регистрация слушателя для изменений SharedPreferences
         ISharedPreferencesOnSharedPreferenceChangeListener preferencesChangeListener =
            new DefaultSharedPreferenceChangeListener(this);
         var sharedPreferences = PreferenceManager.GetDefaultSharedPreferences(this);
         sharedPreferences.RegisterOnSharedPreferenceChangeListener(preferencesChangeListener);

         // Определение размера экрана
         var screenSize = Resources.Configuration.ScreenLayout & ScreenLayout.SizeMask;

         // Для планшетного утройства phoneDevice присваивается false
         if (screenSize == ScreenLayout.SizeLarge || screenSize == ScreenLayout.SizeXlarge)
            _phoneDevice = false; // Не соответствует размерам телефона

         // На телефоне разрешена только портретная ориентация
         if (_phoneDevice)
            RequestedOrientation = ScreenOrientation.Portrait;
      }

      protected override void OnStart()
      {
         base.OnStart();

         if (_preferencesChanged)
         {
            // После задания настроек по умолчанию инициализировать MainActivityFragment и запустить викторину
            var quizFragment = (MainActivityFragment) SupportFragmentManager.FindFragmentById(Id.quizFragment);
            quizFragment.UpdateGuessRows(PreferenceManager.GetDefaultSharedPreferences(this));
            quizFragment.UpdateRegions(PreferenceManager.GetDefaultSharedPreferences(this));
            quizFragment.ResetQuiz();
            _preferencesChanged = false;
         }
      }

      public override bool OnCreateOptionsMenu(IMenu menu)
      {
         // Получение текущей ориентации устройства
         var orientation = Resources.Configuration.Orientation;

         // Отображение меню приложения только в портретной ориентации
         if (orientation == Orientation.Portrait)
         {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
         }

         return false;
      }

      public override bool OnOptionsItemSelected(IMenuItem item)
      {
         var preferencesIntent = new Intent(this, typeof(SettingsActivity));
         StartActivity(preferencesIntent);

         return base.OnOptionsItemSelected(item);
      }

      /// <summary>
      ///    Слушатель изменений конфигурации SharedPreferences приложения
      /// </summary>
      private sealed class DefaultSharedPreferenceChangeListener
         : JavaObj, ISharedPreferencesOnSharedPreferenceChangeListener
      {
         private readonly MainActivity _activity;

         public DefaultSharedPreferenceChangeListener(MainActivity activity)
            => _activity = activity;

         public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
         {
            _activity._preferencesChanged = true; // Пользователь изменил настройки

            var quizFragment =
               (MainActivityFragment) _activity.SupportFragmentManager.FindFragmentByTag(QuizFragmentTag);
            if (key.Equals(Choices, StringComparison.Ordinal))
            {
               // Изменилось число вариантов
               quizFragment.UpdateGuessRows(sharedPreferences);
               quizFragment.ResetQuiz();
            }
            else if (key.Equals(Regions, StringComparison.Ordinal))
            {
               // Изменились регионы
               var regions = sharedPreferences.GetStringSet(Regions, null);
               if (regions != null)
               {
                  if (regions.Count > 0)
                  {
                     quizFragment.UpdateRegions(sharedPreferences);
                     quizFragment.ResetQuiz();
                  }
                  else
                  {
                     // Хотя бы один регион - по умолчанию Северная Америка
                     var editor = sharedPreferences.Edit();
                     regions.Add(_activity.GetString(Resource.String.default_region));
                     editor.PutStringSet(Regions, regions);
                     editor.Apply();

                     Toast.MakeText(_activity, Resource.String.default_region_message, ToastLength.Short).Show();
                  }
               }
            }

            Toast.MakeText(_activity, Resource.String.restarting_quiz, ToastLength.Short).Show();
         }
      }
   }
}
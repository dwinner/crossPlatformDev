/**
 * Создание файла настроек по файлу preferences.xml из res/xml
 */

using Android.OS;
using Android.Preferences;

namespace FlagQuiz.App
{
   public class SettingsActivityFragment : PreferenceFragment
   {
      public override void OnCreate(Bundle savedInstanceState)
      {
         base.OnCreate(savedInstanceState);
         AddPreferencesFromResource(Resource.Xml.preferences); // Загрузить из XML
      }
   }
}
/**
 * Фрагмент для стирания изображения
 */

using Android.App;
using Android.Content;
using Android.OS;
using DialogFragmentV4 = Android.Support.V4.App.DialogFragment;
using StringRes = Doodlz.App.Resource.String;
using IdRes = Doodlz.App.Resource.Id;

namespace Doodlz.App
{
   /// <summary>
   ///    Класс диалогового окна
   /// </summary>
   public class EraseImageDialogFragment : DialogFragmentV4
   {
      /// <summary>
      ///    Получение ссылки на MainActivityFragment
      /// </summary>
      private MainActivityFragment DoodleFragment =>
         (MainActivityFragment) FragmentManager.FindFragmentById(IdRes.doodleFragment);

      public override Dialog OnCreateDialog(Bundle savedInstanceState) // Создание и возвращение объекта AlertDialog
         => new AlertDialog.Builder(Activity)
            .SetMessage(StringRes.message_erase)
            .SetPositiveButton(StringRes.button_erase, (sender, args) => DoodleFragment.DoodleView.Clear())
            .SetNegativeButton(Android.Resource.String.Cancel, (sender, args) => { })
            .Create();

      public override void OnAttach(Context context) // Сообщает MainActivityFragment, что диалог находится на экране
      {
         base.OnAttach(context);
         if (DoodleFragment != null) DoodleFragment.DialogOnScreen = true;
      }

      public override void OnDetach() // Сообщает MainActivityFragment, что окно не отображается
      {
         base.OnDetach();
         if (DoodleFragment != null) DoodleFragment.DialogOnScreen = false;
      }
   }
}
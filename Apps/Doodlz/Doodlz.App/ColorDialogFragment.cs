using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using DialogFragmentV4 = Android.Support.V4.App.DialogFragment;
using LayoutRes = Doodlz.App.Resource.Layout;
using StringRes = Doodlz.App.Resource.String;
using IdRes = Doodlz.App.Resource.Id;

namespace Doodlz.App
{
   /// <summary>
   ///    Используется для выбора цвета в <see cref="DoodleView" />
   /// </summary>
   public class ColorDialogFragment : DialogFragmentV4
   {
      private SeekBar _alphaSeekBar;
      private SeekBar _blueSeekBar;
      private Color _color;
      private View _colorView;
      private SeekBar _greenSeekBar;
      private SeekBar _redSeekBar;

      public override Dialog OnCreateDialog(Bundle savedInstanceState) // Создание и возвращение объекта AlertDialog
      {
         // Создание диалогового окна
         var builder = new AlertDialog.Builder(Activity);
         var colorDialogView = Activity.LayoutInflater.Inflate(LayoutRes.fragment_color, null);
         builder.SetView(colorDialogView); // Добавление GUI в диалоговое окно
         builder.SetTitle(StringRes.title_color_dialog); // Назначение сообщения AlertDialog

         // Получение значений SeekBar и назначение и назначение слушателей onChange
         _alphaSeekBar = colorDialogView.FindViewById<SeekBar>(IdRes.alphaSeekBar);
         _redSeekBar = colorDialogView.FindViewById<SeekBar>(IdRes.redSeekBar);
         _greenSeekBar = colorDialogView.FindViewById<SeekBar>(IdRes.greenSeekBar);
         _blueSeekBar = colorDialogView.FindViewById<SeekBar>(IdRes.blueSeekBar);
         _colorView = colorDialogView.FindViewById<View>(IdRes.colorView);

         // Регистрация слушателей событий SeekBar
         _alphaSeekBar.ProgressChanged += OnColorChanged;
         _redSeekBar.ProgressChanged += OnColorChanged;
         _greenSeekBar.ProgressChanged += OnColorChanged;
         _blueSeekBar.ProgressChanged += OnColorChanged;

         // Использование текущего цвета линии для инициализации
         var doodleView = GetDoodleFragment().DoodleView;
         _color = doodleView.DrawingColor;
         _alphaSeekBar.Progress = Color.GetAlphaComponent(_color);
         _redSeekBar.Progress = Color.GetRedComponent(_color);
         _greenSeekBar.Progress = Color.GetGreenComponent(_color);
         _blueSeekBar.Progress = Color.GetBlueComponent(_color);
         _colorView.SetBackgroundColor(
            Color.Argb(_alphaSeekBar.Progress, _redSeekBar.Progress, _greenSeekBar.Progress, _blueSeekBar.Progress));
         builder.SetPositiveButton(StringRes.button_set_color,
            (sender, args) => doodleView.DrawingColor = _color); // Добавление кнопки назначения цвета

         return builder.Create(); // Возвращение диалогового окна
      }

      public override void OnAttach(Context context) // Сообщает DoodleFragment, что диалоговое окно находится на экране
      {
         base.OnAttach(context);
         var fragment = GetDoodleFragment();
         if (fragment != null) fragment.DialogOnScreen = true;
      }

      public override void OnDetach() // Сообщает DoodleFragment, что диалоговое окно не отображается
      {
         base.OnDetach();
         var fragment = GetDoodleFragment();
         if (fragment != null) fragment.DialogOnScreen = false;
      }

      private MainActivityFragment GetDoodleFragment() =>
         (MainActivityFragment) FragmentManager.FindFragmentById(IdRes.doodleFragment);

      private void OnColorChanged(object sender, SeekBar.ProgressChangedEventArgs e)
      {
         if (e.FromUser) // Изменено пользователем, а не программой
         {
            _color = Color.Argb(
               _alphaSeekBar.Progress, _redSeekBar.Progress, _greenSeekBar.Progress, _blueSeekBar.Progress);
            _colorView.SetBackgroundColor(_color);
         }
      }
   }
}
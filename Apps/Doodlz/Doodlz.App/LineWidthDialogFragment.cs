/**
 * Используется для выбора толщины линии
 */

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using DialogFragmentV4 = Android.Support.V4.App.DialogFragment;
using IdRes = Doodlz.App.Resource.Id;
using LayoutRes = Doodlz.App.Resource.Layout;
using StringRes = Doodlz.App.Resource.String;

namespace Doodlz.App
{
   /// <summary>
   ///    Класс диалогового окна выбора цвета
   /// </summary>
   public class LineWidthDialogFragment : DialogFragmentV4
   {
      private ImageView _widthImageView;

      /// <summary>
      ///    Возвращает ссылку на MainActivityFragment
      /// </summary>
      private MainActivityFragment DoodleFragment =>
         (MainActivityFragment) FragmentManager.FindFragmentById(IdRes.doodleFragment);

      public override Dialog OnCreateDialog(Bundle savedInstanceState) // Создает и возвращает AlertDialog
      {
         // Создание диалогового окна
         var builder = new AlertDialog.Builder(Activity);
         var lineWidthDialogView = Activity.LayoutInflater.Inflate(LayoutRes.fragment_line_width, null);
         builder.SetView(lineWidthDialogView); // Добавление GUI
         builder.SetTitle(StringRes.title_line_width_dialog); // Назначение сообщения AlertDialog
         _widthImageView = lineWidthDialogView.FindViewById<ImageView>(IdRes.widthImageView); // Получение ImageView

         // Настройка widthSeekBar
         var doodleView = DoodleFragment.DoodleView;
         var widthSeekBar = lineWidthDialogView.FindViewById<SeekBar>(IdRes.widthSeekBar);
         widthSeekBar.ProgressChanged += OnSeekBarProgressChanged;
         widthSeekBar.Progress = doodleView.LineWidth;

         // Добавление кнопки Set Line Width
         builder.SetPositiveButton(StringRes.button_set_line_width,
            (sender, args) => doodleView.LineWidth = widthSeekBar.Progress);

         return builder.Create(); // Возвращение диалогового окна
      }

      public override void OnAttach(Context context) // Сообщает MainActivityFragment, что диалоговое окно находится на экране
      {
         base.OnAttach(context);
         if (DoodleFragment != null) DoodleFragment.DialogOnScreen = true;
      }

      public override void OnDetach() // Сообщает MainActivityFragment, что окно не отображается
      {
         base.OnDetach();
         if (DoodleFragment != null) DoodleFragment.DialogOnScreen = false;
      }

      private void OnSeekBarProgressChanged(object sender, SeekBar.ProgressChangedEventArgs e)
      {
         var bitmap = Bitmap.CreateBitmap(400, 100, Bitmap.Config.Argb8888);
         var canvas = new Canvas(bitmap); // Рисует на bitmap

         // Настройка объекта Paint для текущего значения SeekBar
         var paint = new Paint
         {
            Color = DoodleFragment.DoodleView.DrawingColor,
            StrokeCap = Paint.Cap.Round,
            StrokeWidth = e.Progress
         };

         // Стирание объекта bitmap и перерисовка линии
         bitmap.EraseColor(Resources.GetColor(Android.Resource.Color.Transparent, Context.Theme));
         canvas.DrawLine(30, 50, 370, 50, paint);
         _widthImageView.SetImageBitmap(bitmap);
      }
   }
}
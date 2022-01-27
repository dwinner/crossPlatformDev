using Android;
using Android.Content;
using Android.Content.PM;
using Android.Hardware;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using FragmentV4 = Android.Support.V4.App.Fragment;
using R = Doodlz.App.Resource;
using JavaObj = Java.Lang.Object;

namespace Doodlz.App
{
   /// <summary>
   ///    Фрагмент, в котором отображается DoodleView
   /// </summary>
   public class MainActivityFragment : FragmentV4
   {
      private const int AccelerationThreshold = 100000; // Используется для обнаружения встряхивания устройства      
      private const int SaveImagePermissionRequestCode = 1; // Используется для идентификации запросов на использование внещнего хранилища; необходимо для работы функций сохранения      

      private float _acceleration;
      private float _currentAcceleration;
      private float _lastAcceleration;

      private ISensorEventListener _sensorEventListener;

      /// <summary>
      ///    Обработка событий касания и рисования
      /// </summary>
      public DoodleView DoodleView { get; private set; }

      /// <summary>
      ///    Показывать ли диалог
      /// </summary>
      public bool DialogOnScreen { get; set; }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         base.OnCreateView(inflater, container, savedInstanceState);

         var view = inflater.Inflate(R.Layout.fragment_main, container, false);
         HasOptionsMenu = true; // У фрагмента имеются команды меню
         DoodleView = view.FindViewById<DoodleView>(R.Id.doodleView); // Получение ссылки на DoodleView

         // Инициализация параметров ускорения
         _acceleration = 0.00f;
         _currentAcceleration = SensorManager.GravityEarth;
         _lastAcceleration = SensorManager.GravityEarth;
         _sensorEventListener = new DefaultSensorEventListener(this);

         return view;
      }

      public override void OnResume()
      {
         base.OnResume();
         EnableAccelerometerListening(); // Прослушивание событий встряхивания
      }

      public override void OnPause()
      {
         base.OnPause();
         DisableAccelerometerListening(); // Прекращения прослушивания
      }

      public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater) // Отображение команд меню фрагмента
      {
         base.OnCreateOptionsMenu(menu, inflater);
         inflater.Inflate(R.Menu.doodle_fragment_menu, menu);
      }

      public override bool OnOptionsItemSelected(IMenuItem item) // Обработка выбора команд меню
      {
         switch (item.ItemId) // Выбор в зависимости от идентификатора MenuItem
         {
            case R.Id.color:
               var colorDialog = new ColorDialogFragment();
               colorDialog.Show(FragmentManager, "color dialog"); // i18n: Move To String resource
               return true; // Событие меню обработано

            case R.Id.line_width:
               var widthDialog = new LineWidthDialogFragment();
               widthDialog.Show(FragmentManager, "line width dialog"); // i18n: Move To String resource
               return true; // Событие меню обработано

            case R.Id.delete_drawing:
               ConfirmErase(); // Получить подтверждение перед стиранием
               return true; // Событие меню обработано

            case R.Id.save:
               SaveImage(); // Проверить разрешение и сохранить рисунок
               return true; // Событие меню обработано

            case R.Id.print:
               DoodleView.PrintImage(); // Напечатать текущий рисунок
               return true; // Событие меню обработано
         }

         return base.OnOptionsItemSelected(item);
      }

      /// <summary>
      ///    Вызывается системой, когда пользователь предоставляет
      ///    или отклоняет разрешение для сохранения изображения
      /// </summary>
      /// <param name="requestCode">Код запроса</param>
      /// <param name="permissions">Массив разрешений</param>
      /// <param name="grantResults">Массив предоставленных разрешений</param>
      public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
      {
         switch (requestCode)
         {
            case SaveImagePermissionRequestCode:
               if (grantResults[0] == Permission.Granted) DoodleView.SaveImage();
               break;
         }
      }

      /// <summary>
      ///    При необходимости метод запрашивает разрешение,
      ///    или сохраняет изображение, если разрешение уже имеется
      /// </summary>
      private void SaveImage()
      {
         const string writeExternalStoragePermission = Manifest.Permission.WriteExternalStorage;

         // Проверить, есть ли у приложения разрешение, необходимое для сохранения
         if (Context.CheckSelfPermission(writeExternalStoragePermission) != Permission.Granted)
         {
            // Объяснить, почему понадобилось разрешение
            if (ShouldShowRequestPermissionRationale(writeExternalStoragePermission))
            {
               var builder = new AlertDialog.Builder(Activity)
                  .SetMessage(R.String.permission_explanation) /* Назначить сообщение AlertDialog */
                  .SetPositiveButton(Android.Resource.String.Ok,
                     (sender, args) => RequestPermissions(new[] {writeExternalStoragePermission},
                        SaveImagePermissionRequestCode)) /* Добавить кнопку OK в диалоговое окно, обработать запрос разрешения */
                  .Create();

               // Отображение диалогового окна
               builder.Show();
            }
            else
            {
               // Запросить разрешение
               RequestPermissions(new[] {writeExternalStoragePermission}, SaveImagePermissionRequestCode);
            }
         }
         else
         {
            // Разрешение для записи уже есть
            DoodleView.SaveImage(); // Сохранить изображение
         }
      }

      private void EnableAccelerometerListening() // Включение прослушивания событий акселерометра
      {
         var sensorManager =
            (SensorManager) Activity.GetSystemService(Context.SensorService); // Получение объекта SensorManager
         sensorManager.RegisterListener(
            _sensorEventListener, sensorManager.GetDefaultSensor(SensorType.Accelerometer),
            SensorDelay.Normal); // Регистрация для прослушивания событий акселерометра
      }

      private void DisableAccelerometerListening()
      {
         var sensorManager =
            (SensorManager) Activity.GetSystemService(Context.SensorService); // Получение объекта SensorManager
         sensorManager.UnregisterListener(_sensorEventListener,
            sensorManager.GetDefaultSensor(SensorType
               .Accelerometer)); // Прекращение прослушивания событий акселерометра
      }

      private void ConfirmErase() // Подтверждение стирания рисунка
      {
         var fragment = new EraseImageDialogFragment();
         fragment.Show(FragmentManager, "Erase Dialog"); // i18n: Move To String resource
      }

      /// <summary>
      ///    Обработчик для событий акселерометра
      /// </summary>
      private sealed class DefaultSensorEventListener : JavaObj, ISensorEventListener
      {
         private readonly MainActivityFragment _fragment;

         public DefaultSensorEventListener(MainActivityFragment mainActivityFragment) =>
            _fragment = mainActivityFragment;

         public void OnAccuracyChanged(Sensor sensor, SensorStatus accuracy)
         {
         }

         public void OnSensorChanged(SensorEvent e) // Проверка встряхивания по показаниям акселерометра
         {
            // На экране не должно быть других диалоговых окон
            if (_fragment.DialogOnScreen)
               return;

            // Получить значения x, y, z для SensorEvent
            var x = e.Values[0];
            var y = e.Values[1];
            var z = e.Values[2];

            // Сохранить предыдущие данные ускорения
            _fragment._lastAcceleration = _fragment._currentAcceleration;

            // Вычислить текущее ускорение
            _fragment._currentAcceleration = x * x + y * y + z * z;

            // Вычислить изменение ускорения
            _fragment._acceleration = _fragment._currentAcceleration *
                                      (_fragment._currentAcceleration - _fragment._lastAcceleration);

            // Если изменение превышает заданный порог, то вызвать стирание
            if (_fragment._acceleration > AccelerationThreshold) _fragment.ConfirmErase();
         }
      }
   }
}
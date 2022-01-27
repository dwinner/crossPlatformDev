using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Java.Lang;
using AlertDialog = Android.Support.V7.App.AlertDialog;
using DialogFragment = Android.Support.V4.App.DialogFragment;

namespace AppDevUnited.CannonGame.App
{
   public sealed partial class CannonView
   {
      /// <summary>
      ///    Обработчик поверхности холста
      /// </summary>
      private sealed class SurfaceHolderCallbackImpl : Object, ISurfaceHolderCallback
      {
         private readonly CannonView _cannonView;

         public SurfaceHolderCallbackImpl(CannonView cannonView) => _cannonView = cannonView;

         public void SurfaceChanged(ISurfaceHolder holder, Format format, int width, int height)
         {
         }

         public void SurfaceCreated(ISurfaceHolder holder)
         {
            if (!_cannonView._dialogIsDisplayed)
            {
               _cannonView.NewGame(); // Создание новой игры
               _cannonView._cannonThread = new CannonThread(holder, _cannonView); // Создание потока
               _cannonView._cannonThread.SetRunning(true); // Запуск игры
               _cannonView._cannonThread.Start(); // Запуск потока игрового цикла
            }
         }

         public void SurfaceDestroyed(ISurfaceHolder holder)
         {
            var retry = true; // Обеспечить корректную зависимость потока
            _cannonView._cannonThread.SetRunning(false); // Завершение cannonThread

            while (retry)
               try
               {
                  _cannonView._cannonThread.Join(); // Ожидать завершения cannonThread
                  retry = false;
               }
               catch (InterruptedException interruptedEx)
               {
                  Log.Error(ErrorTag, "Thread interrupted", interruptedEx);
               }
         }
      }

      /// <summary>
      ///    Управление циклом игры
      /// </summary>
      private sealed class CannonThread : Thread
      {
         private readonly CannonView _cannonView;
         private readonly ISurfaceHolder _surfaceHolder; // Для работы с Canvas
         private bool _threadIsRunning = true;

         internal CannonThread(ISurfaceHolder surfaceHolder, CannonView cannonView)
         {
            _surfaceHolder = surfaceHolder;
            _cannonView = cannonView;
            Name = nameof(CannonThread);
         }

         /// <summary>
         ///    Изменение состояния выполнения
         /// </summary>
         /// <param name="isRunning"></param>
         internal void SetRunning(bool isRunning) => _threadIsRunning = isRunning;

         public override void Run()
         {
            Canvas canvas = null; // Используется для рисования
            var previousFrameTime = JavaSystem.CurrentTimeMillis();

            while (_threadIsRunning)
               try
               {
                  // Получение Canvas для монопольного рисования из этого потока
                  canvas = _surfaceHolder.LockCanvas();

                  // Блокировка surfaceHolder для рисования
                  lock (_surfaceHolder)
                  {
                     var currentTime = JavaSystem.CurrentTimeMillis();
                     var elapsedTimeMs = currentTime - previousFrameTime;
                     _cannonView._totalElapsedTime += elapsedTimeMs / 1000.0;
                     _cannonView.UpdatePositions(elapsedTimeMs); // Обновление состояния игры
                     _cannonView.TestForCollisions(); // Проверка столкновений
                     _cannonView.DrawGameElements(canvas); // Рисование на объекте canvas
                     previousFrameTime = currentTime; // Обновление времени
                  }
               }
               finally
               {
                  // Вывести содержимое canvas на CannonView и разрешить использовать Canvas другим потокам
                  if (canvas != null)
                  {
                     _surfaceHolder.UnlockCanvasAndPost(canvas);
                  }
               }
         }
      }

      /// <summary>
      ///    DialogFragment для вывода статистики и начала новой игры
      /// </summary>
      private sealed class GameOverDialogFragment : DialogFragment
      {
         private readonly CannonView _cannonView;
         private readonly string _message;

         public GameOverDialogFragment(string message, CannonView cannonView)
         {
            _message = message;
            _cannonView = cannonView;
         }

         public override Dialog OnCreateDialog(Bundle savedInstanceState) =>
            new AlertDialog.Builder(_cannonView._activity)
               .SetTitle(_message)
               // Вывод кол-ва выстрелов и затраченного времени
               .SetMessage(
                  _cannonView._activity.Resources.GetString(
                     Resource.String.results_format, _cannonView._shotsFired, _cannonView._totalElapsedTime))
               .SetPositiveButton(Resource.String.reset_game,
                  (sender, args) =>
                  {
                     _cannonView._dialogIsDisplayed = false;
                     _cannonView.NewGame(); // Создание и начало новой партии
                  })
               .Create();
      }
   }
}
using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Support.V4.App;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using AppDevUnited.CannonGame.App.GameElements;
using static Android.Views.SystemUiFlags;
using RawRes = AppDevUnited.CannonGame.App.Resource.Raw;
using ColorRes = AppDevUnited.CannonGame.App.Resource.Color;
using StringRes = AppDevUnited.CannonGame.App.Resource.String;

namespace AppDevUnited.CannonGame.App
{
   /// <summary>
   ///    Класс отображает игровые объекты и управляет приложением Cannon Game
   /// </summary>
   public sealed partial class CannonView : SurfaceView
   {
      private readonly AppCompatActivity _activity; // Для отображения окна в потоке GUI
      private readonly Paint _backgroundPaint; // Для стирания области рисования
      private readonly SparseIntArray _soundMap; // Связь идентификаторов с SoundPool
      private readonly SoundPool _soundPool; // Воспроизведение звуков
      private readonly List<Target> _targets = new List<Target>((int) TargetPieces);

      // Переменные Paint для рисования элементов на экране
      private readonly Paint _textPaint; // Для вывода текста
      private Blocker _blocker;

      // Игровые объекты
      private Cannon _cannon;
      private CannonThread _cannonThread; // Управляет циклом игры
      private bool _dialogIsDisplayed;

      // Переменные для игрового цикла и отслеживания состояния игры
      private bool _gameOver; // Игра закончена
      private int _screenHeight; // Получение высоты экрана
      private int _screenWidth; // Получение ширины экрана
      private int _shotsFired; // Кол-во сделанных выстрелов
      private double _timeLeft; // Оставшееся время в секундах
      private double _totalElapsedTime; // Затраты времени в секундах

      public CannonView(Context context, IAttributeSet attrs)
         : base(context, attrs)
      {
         _activity = (AppCompatActivity) context; // Ссылка на MainActivity
         Holder.AddCallback(new SurfaceHolderCallbackImpl(this)); // Регистрация слушателя

         // Инициализация SoundPool для воспроизведения звука
         _soundPool = new SoundPool.Builder()
            .SetMaxStreams(1)
            .SetAudioAttributes(
               new AudioAttributes.Builder()
                  .SetUsage(AudioUsageKind.Game)
                  .Build())
            .Build();

         // Создание Map и предварительная загрузка звуков
         _soundMap = new SparseIntArray(3);
         _soundMap.Put(TargetSoundId, _soundPool.Load(context, RawRes.target_hit, 1));
         _soundMap.Put(CannonSoundId, _soundPool.Load(context, RawRes.cannon_fire, 1));
         _soundMap.Put(BlockerSoundId, _soundPool.Load(context, RawRes.blocker_hit, 1));

         _textPaint = new Paint();
         _backgroundPaint = new Paint {Color = Color.White};
      }

      protected override void OnSizeChanged(int width, int height, int oldWidth, int oldHeight)
      {
         base.OnSizeChanged(width, height, oldWidth, oldHeight);

         _screenWidth = width; // Сохранение ширины CannonView
         _screenHeight = height; // Сохранение высоты CannonView

         // Настройка свойств текста
         _textPaint.TextSize = (int) (TextSizePercent * _screenHeight);
         _textPaint.AntiAlias = true; // Сглаживание текста
      }

      public override bool OnTouchEvent(MotionEvent e)
      {
         var action = e.Action;

         // Пользователь коснулся экрана или провел пальцем по экрану
         if (action == MotionEventActions.Down || action == MotionEventActions.Move)
            AlignAndFireCannonBall(
               (
                  (int) e.GetX(),
                  (int) e.GetY()
               ));

         return true;
      }

      /// <summary>
      ///    Остановка игры
      /// </summary>
      internal void StopGame() => _cannonThread?.SetRunning(false);

      /// <summary>
      ///    Освобождение ресурсов
      /// </summary>
      public void ReleaseResources() => _soundPool?.Release();

      /// <summary>
      ///    Проигрывание звука
      /// </summary>
      /// <param name="soundId">Идентификатор звука</param>
      internal void PlaySound(int soundId) => _soundPool.Play(_soundMap.Get(soundId), 1, 1, 1, 0, 1F);

      /// <summary>
      ///    Сброс всех экранных элементов
      /// </summary>
      private void NewGame()
      {
         // Создание новой пушки
         _cannon = new Cannon(this,
            (int) (CannonBaseRadiusPercent * _screenHeight),
            (int) (CannonBarrelLengthPercent * _screenWidth),
            (int) (CannonBarrelWidthPercent * _screenHeight));

         var random = new Random(); // Для случайных скоростей
         _targets.Clear(); // Построение нового списка мишеней
         var targetX = (int) (TargetFirstXPercent * _screenWidth); // Инициализация targetX для первой мишени слева
         var targetY = (int) ((0.5 - TargetLengthPercent / 2) * _screenHeight); // Вычисление координаты Y

         // Добавление TargetPieces мишеней в список
         for (var n = 0; n < TargetPieces; n++)
         {
            // Получение случайной скорости в диапазоне от min до max для мишени n
            var velocity = _screenHeight * (random.NextDouble() * (TargetMaxSpeedPercent - TargetMinSpeedPercent) +
                                            TargetMinSpeedPercent);

            // Цвета мишеней чередуются между белым и чёрным
            var color = Resources.GetColor(n % 2 == 0 ? ColorRes.dark : ColorRes.light, Context.Theme);
            velocity *= n % 2 == 0 ? 1 : -1; // Противоположная скорость следующей мишени

            // Создание и добавление новой мишени в список
            _targets.Add(new Target(this, color, HitReward, targetX, targetY,
               (int) (TargetWidthPercent * _screenWidth),
               (int) (TargetLengthPercent * _screenHeight),
               (int) velocity));

            // Увеличение координаты x для смещения следующей мишени вправо
            targetX += (int) ((TargetWidthPercent + TargetSpacingPercent) * _screenWidth);
         }

         // Создание нового блока
         _blocker = new Blocker(this,
            Color.Black,
            MissPenalty,
            (int) (BlockerXPercent * _screenWidth),
            (int) ((0.5 - BlockerLengthPercent / 2) * _screenHeight),
            (int) (BlockerWidthPercent * _screenWidth),
            (int) (BlockerLengthPercent * _screenHeight),
            (float) (BlockerSpeedPercent * _screenHeight));

         _timeLeft = CountDownSeconds; // Обратный отсчет с 10-ти секунд
         _shotsFired = 0; // Начальное кол-во выстрелов
         _totalElapsedTime = 0.0; // Обнулить затраченное время

         if (_gameOver) // Начать новую игру после завершения предыдущей
         {
            _gameOver = false; // Игра не закончена
            _cannonThread = new CannonThread(Holder, this); // Создать поток
            _cannonThread.Start(); // Запуск потока игрового цикла
         }

         HideSystemBars();
      }

      /// <summary>
      ///    Сокрытие системных панелей и панели приложения
      /// </summary>
      private void HideSystemBars()
      {
         if (_MoreOrEqualThanKitKatFunc())
            SystemUiVisibility = (StatusBarVisibility)
               (LayoutStable | HideNavigation | Fullscreen | LayoutHideNavigation | LayoutFullscreen | Immersive);
      }

      /// <summary>
      ///    Многократно вызывается <see cref="CannonThread" /> для обновления элементов игры
      /// </summary>
      /// <param name="elapsedTimeMs">Частота обновления в мс</param>
      private void UpdatePositions(double elapsedTimeMs)
      {
         var interval = elapsedTimeMs / 1000.0; // Преобразовать в секунды
         _cannon.CannonBall?.Update(interval);

         _blocker.Update(interval); // Обновление позиции ядра
         _targets.ForEach(target => target.Update(interval)); // Обновление позиции мишени
         _timeLeft -= interval; // Уменьшение оставшегося времени

         // Если счетчик достиг нуля
         if (_timeLeft <= 0)
         {
            _timeLeft = 0.0;
            _gameOver = true; // Игра закончена
            _cannonThread.SetRunning(false); // Завершение потока
            ShowGameOverDialog(_activity.Resources.GetString(StringRes.lose)); // Сообщение о проигрыше
         }

         // Если все мишени поражены
         if (_targets.Count == 0)
         {
            _cannonThread.SetRunning(false); // Завершение потока
            ShowGameOverDialog(_activity.Resources.GetString(StringRes.win)); // Сообщение о выигрыше
            _gameOver = true;
         }
      }

      /// <summary>
      ///    Метод определяет угол наклона ствола и стреляет из пушки,
      ///    если ядро не находится на экране
      /// </summary>
      /// <param name="touchCoords">Событие касания</param>
      private void AlignAndFireCannonBall((int x, int y) touchCoords)
      {
         var touchPoint =
            new Point(touchCoords.x, touchCoords.y); // Получение точки касания в этом представлении         
         var centerMinusY =
            _screenHeight / 2 - touchPoint.Y; // Вычисление расстояния точки касания от центра экрана по оси y
         var angle = Math.Atan2(touchPoint.X, centerMinusY); // Вычислить угол ствола относительно горизонтали
         _cannon.Align(angle); // Ствол наводится в точку касания
         if (_cannon.CannonBall == null || !_cannon.CannonBall.OnScreen
         ) // Пушка стреляет, если ядро не находится на экране
         {
            _cannon.FireCannonBall();
            ++_shotsFired;
         }
      }

      /// <summary>
      ///    Отображение окна AlertDialog при завершении игры
      /// </summary>
      private void ShowGameOverDialog(string message)
      {
         DialogFragment gameResult = new GameOverDialogFragment(message, this);

         // В UI-потоке FragmentManager используется для вывода DialogFragment
         _activity.RunOnUiThread(() =>
         {
            ShowSystemBars(); // Выход из режима погружения
            _dialogIsDisplayed = true;
            gameResult.Cancelable = false; // Модальное окно
            gameResult.Show(_activity.SupportFragmentManager, ResultsTag);
         });
      }

      /// <summary>
      ///    Рисование элементов игры
      /// </summary>
      /// <param name="canvas">Холст</param>
      private void DrawGameElements(Canvas canvas)
      {
         canvas.DrawRect(0, 0, canvas.Width, canvas.Height, _backgroundPaint); // Очистка фона
         canvas.DrawText(
            Resources.GetString(StringRes.time_remaining_format, _timeLeft), 50, 100,
            _textPaint); // Вывод оставшегося времени
         _cannon.Draw(canvas); // Рисование пушки

         // Рисование игровых элементов
         if (_cannon?.CannonBall?.OnScreen == true) _cannon.CannonBall.Draw(canvas);

         _blocker.Draw(canvas); // Рисование блока
         _targets.ForEach(target => target.Draw(canvas)); // Рисование всех мишеней
      }

      /// <summary>
      ///    Проверка столкновений с блоками или мишенями и обработка столкновений
      /// </summary>
      private void TestForCollisions()
      {
         var cannonBall = _cannon.CannonBall;

         if (cannonBall?.OnScreen == true)
            for (var n = 0; n < _targets.Count; n++)
               // Удаление мишеней, с которыми сталкивается ядро
               if (cannonBall.CollidesWith(_targets[n]))
               {
                  _targets[n].PlaySound(); // Звук попадания в мишень
                  _timeLeft += _targets[n].HitReward; // Прибавление награды к оставшемуся времени
                  _cannon.RemoveCannonBall(); // Удаление ядра из игры
                  _targets.RemoveAt(n); // Удаление пораженной мишени
                  --n; // Чтобы не пропустить проверку новой мишени
                  break;
               }
         else
            _cannon.RemoveCannonBall();

         // Проверка столкновения с блоком
         if (cannonBall?.CollidesWith(_blocker) == true)
         {
            _blocker.PlaySound();
            cannonBall.ReverseVelocityX(); // Изменение направления
            _timeLeft -= _blocker.MissPenalty; // Уменьшение оставшегося времени на величину штрафа
         }
      }

      /// <summary>
      ///    Вывести системные панели и панель приложения
      /// </summary>
      private void ShowSystemBars()
      {
         if (_MoreOrEqualThanKitKatFunc())
            SystemUiVisibility = (StatusBarVisibility) (LayoutStable | LayoutHideNavigation | LayoutFullscreen);
      }
   }
}
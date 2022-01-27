using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Provider;
using Android.Support.V4.Print;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
/**
 * Главное представление приложения Doodlz
 */
using StringRes = Doodlz.App.Resource.String;

namespace Doodlz.App
{
   public class DoodleView : View
   {
      private const float TouchTolerance = 10; // Смещение, необходимое для продолжения рисования

      // Используется для рисования линий на Bitmap
      private readonly Paint _paintLine = new Paint
      {
         AntiAlias = true, // Сглаживание краев
         Color = Color.Black, // По-умолчанию чёрный цвет
         StrokeWidth = 5, // Толщина линии по-умолчанию
         StrokeCap = Paint.Cap.Round // Закругленные концы
      };

      private readonly Paint _paintScreen; // Используется для вывода Bitmap на экран

      // Данные нарисованных контуров Path и содержащихся в них точек
      private readonly Dictionary<int, Path> _pathMap = new Dictionary<int, Path>();
      private readonly Dictionary<int, Point> _previousPointMap = new Dictionary<int, Point>();
      private Bitmap _bitmap; // Область рисования для вывода и сохранения
      private Canvas _bitmapCanvas; // Используется для рисования на Bitmap

      public DoodleView()
         : base(null, null)
      {
      }

      public DoodleView(Context context, IAttributeSet attrs)
         : base(context, attrs)
      {
      }

      public DoodleView(Context context, IAttributeSet attrs, Paint paintScreen)
         : base(context, attrs)
      {
         _paintScreen = paintScreen; // Используется для вывода на экран
         _paintLine.SetStyle(Paint.Style.Stroke); // Сплошная линия
      }

      /// <summary>
      ///    Цвет рисуемой линии
      /// </summary>
      public Color DrawingColor
      {
         get => _paintLine.Color;
         set => _paintLine.Color = value;
      }

      /// <summary>
      ///    Толщина рисуемой линии
      /// </summary>
      public int LineWidth
      {
         get => (int) _paintLine.StrokeWidth;
         set => _paintLine.StrokeWidth = value;
      }

      protected override void OnSizeChanged(int newWidth, int newHeight, int oldWidth, int oldHeight)
      {
         _bitmap = Bitmap.CreateBitmap(newWidth, newHeight, Bitmap.Config.Argb8888);
         _bitmapCanvas = new Canvas(_bitmap);
         _bitmap.EraseColor(Color.White); // bitmap стирается белым цветом
      }

      /// <summary>
      ///    Печать текущего изображения
      /// </summary>
      public void PrintImage()
      {
         if (PrintHelper.SystemSupportsPrint())
         {
            // Использование класса PrintHelper для печати
            var printHelper = new PrintHelper(Context)
            {
               ScaleMode = PrintHelper.ScaleModeFit
            };

            // Изображение масштабируется и выводится на печать
            printHelper.PrintBitmap("Doodlz Image", _bitmap);
         }
         else
         {
            // Вывод сообщения о том, что система не поддерживает печать
            var message = Toast.MakeText(Context, StringRes.message_error_printing, ToastLength.Short);
            message.SetGravity(GravityFlags.Center, message.XOffset / 2, message.YOffset / 2);
            message.Show();
         }
      }

      /// <summary>
      ///    Сохранение текущего изображения в галерее
      /// </summary>
      public void SaveImage()
      {
         // Имя состоит из префикса Doodlz и текущего времени
         var name = $"Doodlz{JavaSystem.CurrentTimeMillis()}.jpg";

         // Сохранение изображения в галерее устойства
         var saveLocation =
            MediaStore.Images.Media.InsertImage(Context.ContentResolver, _bitmap, name, "Doodlz Drawing");
         var saveResultMessage = saveLocation != null ? StringRes.message_saved : StringRes.message_error_saving;
         var toastMessage = Toast.MakeText(Context, saveResultMessage, ToastLength.Short);
         toastMessage.SetGravity(GravityFlags.Center, toastMessage.XOffset / 2, toastMessage.YOffset / 2);
         toastMessage.Show();
      }

      /// <summary>
      ///    Стирание рисунка
      /// </summary>
      public void Clear()
      {
         _pathMap.Clear(); // Удалить все контуры
         _previousPointMap.Clear(); // Удалить все предыдущие точки
         _bitmap.EraseColor(Color.White); // Очистка изображения
         Invalidate(); // Перерисовать изображение
      }

      /// <inheritdoc />
      protected override void OnDraw(Canvas canvas) // Перерисовка при обновлении DoodleView на экране
      {
         canvas.DrawBitmap(_bitmap, 0, 0, _paintScreen); // Перерисовка фона

         // Для каждой выводимой линии
         foreach (var key in _pathMap.Keys) canvas.DrawPath(_pathMap[key], _paintLine);
      }

      /// <inheritdoc />
      public override bool OnTouchEvent(MotionEvent e) // Обработка события касания
      {
         var action = e.ActionMasked; // Тип события
         var actionIndex = e.ActionIndex; // Указатель (палец)

         // Что происходит: начало касания, конец, перемещение?
         switch (action)
         {
            case MotionEventActions.Down: // Первый палец, коснувшийся экрана
            case MotionEventActions.PointerDown: // Остальные пальцы, коснувшиеся экрана
               TouchStarted(e.GetX(actionIndex), e.GetY(actionIndex), e.GetPointerId(actionIndex));
               break;

            case MotionEventActions.Up:
            case MotionEventActions.PointerUp:
               TouchEnded(e.GetPointerId(actionIndex));
               break;

            default:
               TouchMoved(e);
               break;
         }

         Invalidate(); // Перерисовка
         return true;
      }

      /// <summary>
      ///    Вызывается при перемещении пальца по экрану
      /// </summary>
      /// <param name="motionEvent">Событие перемещения</param>
      private void TouchMoved(MotionEvent motionEvent)
      {
         // Для каждого указателя (пальца) в объекте MotionEvent
         for (var currentPointerIndex = 0; currentPointerIndex < motionEvent.PointerCount; currentPointerIndex++)
         {
            // Получить идентификатор и индекс указателя
            var pointerId = motionEvent.GetPointerId(currentPointerIndex);
            var pointerIndex = motionEvent.FindPointerIndex(pointerId);

            // Если существует объект Path, связанный с указателем
            if (_pathMap.ContainsKey(pointerId))
            {
               // Получить новые координаты для указателя
               var newX = motionEvent.GetX(pointerIndex);
               var newY = motionEvent.GetY(pointerIndex);

               // Получить объект Path и предыдущий объект Point, связанный с указателем
               var path = _pathMap[pointerId];
               var point = _previousPointMap[pointerId];

               // Вычислить величину смещения от последнего обновления
               var deltaX = Math.Abs(newX - point.X);
               var deltaY = Math.Abs(newY - point.Y);

               // Если расстояние достаточно велико
               if (deltaX >= TouchTolerance || deltaY >= TouchTolerance)
               {
                  // Расширение контура до новой точки
                  path.QuadTo(point.X, point.Y, (newX + point.X) / 2, (newY + point.Y) / 2);

                  // Сохранение новых координат
                  point.X = (int) newX;
                  point.Y = (int) newY;
               }
            }
         }
      }

      /// <summary>
      ///    Вызывается при завершении касания
      /// </summary>
      /// <param name="pointerId">Идентификатор пальца</param>
      private void TouchEnded(int pointerId)
      {
         var path = _pathMap[pointerId]; // Получение объекта Path
         _bitmapCanvas.DrawPath(path, _paintLine); // Рисование на bitmapCanvas
         path.Reset(); // Сброс объекта Path
      }

      /// <summary>
      ///    Вызывается при касании экрана
      /// </summary>
      /// <param name="x">Горизонтальная координата</param>
      /// <param name="y">Вертикальная координата</param>
      /// <param name="pointerId">Идентификатор пальца</param>
      private void TouchStarted(float x, float y, int pointerId)
      {
         Path path; // Для хранения контура с заданным идентификатором
         Point point; // Для хранения последней точки в контуре

         // Если для pointerId уже существует объект Path
         if (_pathMap.ContainsKey(pointerId))
         {
            path = _pathMap[pointerId]; // Получение Path
            path.Reset(); // Очистка Path с началом нового касания
            point = _previousPointMap[pointerId]; // Последняя точка Path
         }
         else
         {
            path = new Path();
            _pathMap.Add(pointerId, path); // Добавление Path в Map
            point = new Point(); // Создание нового объекта Point
            _previousPointMap.Add(pointerId, point); // Добавление Point в Map
         }

         // Переход к координатам касания
         path.MoveTo(x, y);
         point.X = (int) x;
         point.Y = (int) y;
      }
   }
}
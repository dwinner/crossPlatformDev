using Android.Graphics;
using System;

namespace AppDevUnited.CannonGame.App.GameElements
{
   /// <summary>
   ///    Класс представляет пушку, стреляющую ядрами
   /// </summary>
   internal class Cannon
   {
      private readonly Point _barrelEnd = new Point(); // Конечная точка ствола
      private readonly int _barrelLength; // Длина ствола
      private readonly int _baseRadius; // Радиус основания
      private readonly Paint _paint = new Paint(); // Объект Paint для рисования пушки
      private readonly CannonView _view; // Представление, в котором находится пушка
      private double _barrelAngle; // Угол наклона ствола

      /// <summary>
      ///    Конструктор
      /// </summary>
      /// <param name="view">Представление <see cref="CannonView" /></param>
      /// <param name="baseRadius">Радиус основания пушки</param>
      /// <param name="barrelLength">Длина ствола</param>
      /// <param name="barrelWidth">Толщина ствола</param>
      internal Cannon(CannonView view, int baseRadius, int barrelLength, int barrelWidth)
      {
         _view = view;
         _baseRadius = baseRadius;
         _barrelLength = barrelLength;
         _paint.StrokeWidth = barrelWidth; // Назначение толщины ствола
         _paint.Color = Color.Black; // Пушка окрашена в чёрный цвет
         Align(Math.PI / 2); // Ствол пушки обращен вправо
      }

      /// <summary>
      ///    Возвращает объект Cannonball, представляющий выпущенное ядро
      /// </summary>
      internal CannonBall CannonBall { get; private set; }

      /// <summary>
      ///    Задает направление ствола пушки
      /// </summary>
      /// <param name="barrelAngle">Угол наклона</param>
      internal void Align(double barrelAngle)
      {
         _barrelAngle = barrelAngle;
         _barrelEnd.X = (int)(_barrelLength * Math.Sin(_barrelAngle));
         _barrelEnd.Y = (int)(-_barrelLength * Math.Cos(_barrelAngle)) + _view.Height / 2;
      }

      /// <summary>
      ///    Метод создает ядро и стреляет в направлении ствола
      /// </summary>
      internal void FireCannonBall()
      {
         // Вычисление горизонтальной состявляющей скорости ядра
         var velocityX = (int)(CannonView.CannonBallSpeedPercent * _view.Width * Math.Sin(_barrelAngle));

         // Вычисление вертикальной составляющей скорости ядра
         var velocityY = (int)(CannonView.CannonBallSpeedPercent * _view.Width * -Math.Cos(_barrelAngle));

         // Вычисление радиуса ядра
         var radius = (int)(_view.Height * CannonView.CannonBallRadiusPercent);

         // Построение ядра и размещение его в стволе
         CannonBall = new CannonBall(_view,
            Color.Black,
            CannonView.CannonSoundId,
            -radius,
            _view.Height / 2 - radius,
            radius,
            velocityX,
            velocityY);

         CannonBall.PlaySound(); // Воспроизведение звука выстрела
      }

      /// <summary>
      ///    Рисование пушки на объекте Canvas
      /// </summary>
      /// <param name="canvas">Объект Canvas</param>
      internal void Draw(Canvas canvas)
      {
         // Рисование ствола пушки
         canvas.DrawLine(0, (float)_view.Height / 2, _barrelEnd.X, _barrelEnd.Y, _paint);

         // Рисование основания пушки
         canvas.DrawCircle(0, (float)_view.Height / 2, _baseRadius, _paint);
      }

      internal void RemoveCannonBall() => CannonBall = null;
   }
}
using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   /// <summary>
   ///    Выпущенное ядро
   /// </summary>
   internal class CannonBall : GameElement
   {
      private float _velocityX;

      /// <summary>
      ///    Конструктор ядра
      /// </summary>
      /// <param name="view">Представление логики игры</param>
      /// <param name="color">Цвет</param>
      /// <param name="soundId">Идентификатор звука</param>
      /// <param name="x">Координата x левого верхнего элемента</param>
      /// <param name="y">Координата y левого верхнего элемента</param>
      /// <param name="radius">Радиус ядра</param>
      /// <param name="velocotyX">Горизонтальная скорость</param>
      /// <param name="velocotyY">Вертикальная скорость</param>
      internal CannonBall(
         CannonView view, Color color, int soundId, int x, int y, int radius, float velocotyX, float velocotyY)
         : base(view, color, soundId, x, y, 2 * radius, 2 * radius, velocotyY)
      {
         _velocityX = velocotyX;
         OnScreen = true;
      }

      /// <summary>
      ///    Радиус ядра
      /// </summary>
      private int Radius => (Shape.Right - Shape.Left) / 2;

      /// <summary>
      ///    Факт присутствия ядра на экране
      /// </summary>
      internal bool OnScreen { get; private set; }

      /// <summary>
      ///    Столкнулось ли ядро с объектом GameElement
      /// </summary>
      /// <param name="element">Объект GameElement</param>
      /// <returns>true, если столкнулось, false - в противном случае</returns>
      internal bool CollidesWith(GameElement element) =>
         Rect.Intersects(Shape, element.Shape) && _velocityX > 0 /* Ядро движется слева направо */;

      /// <summary>
      ///    Инвертирует горизонтальную скорость ядра
      /// </summary>
      internal void ReverseVelocityX() => _velocityX *= -1;

      /// <inheritdoc />
      protected internal override void Update(double interval)
      {
         base.Update(interval); // Обновление вертикальной позиции

         Shape.Offset((int)(_velocityX * interval), 0); // Обновление горизонтальной позиции
         // BUG: Координаты Top, Left всегда отрицательны
         // Если ядро уходит за пределы экрана
         OnScreen = !(Shape.Top < 0 || Shape.Left < 0 || Shape.Bottom > View.Height || Shape.Right > View.Width);
      }

      /// <summary>
      ///    Рисование ядра на объекте Canvas
      /// </summary>
      /// <param name="canvas">Объект Canvas</param>
      public override void Draw(Canvas canvas) =>
         canvas.DrawCircle(Shape.Left + Radius, Shape.Top + Radius, Radius, Paint);
   }
}
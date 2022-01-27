using Android.Graphics;

namespace AppDevUnited.CannonGame.App.Based
{
   /// <summary>
   ///    Прямоугольный игровой элемент
   /// </summary>
   public abstract class GameElement
   {
      private readonly int _soundId;   // Звук, связанный с GameElement
      private float _velocityY;  // Вертикальная скорость GameElement

      /// <summary>
      ///    Конструктор GameElement
      /// </summary>
      /// <param name="view">Представление логики игры</param>
      /// <param name="color">Цвет</param>
      /// <param name="soundId">Идентификатор звука</param>
      /// <param name="x">Координата x левого верхнего элемента</param>
      /// <param name="y">Координата y левого верхнего элемента</param>
      /// <param name="width">Ширина GameElement</param>
      /// <param name="height">Высота GameElement</param>
      /// <param name="velocityY">Начальная вертикальная скорость GameElement</param>
      protected GameElement(
         CannonView view, Color color, int soundId, int x, int y, int width, int height, float velocityY)
      {
         View = view;
         Paint.Color = color;
         Shape = new Rect(x, y, x + width, y + height); // Определение границ
         _soundId = soundId;
         _velocityY = velocityY;
      }

      /// <summary>
      ///    Объект Paint для рисования
      /// </summary>
      protected Paint Paint { get; } = new Paint();

      /// <summary>
      ///    Ограничивающий прямоугольник GameElement
      /// </summary>
      protected internal Rect Shape { get; }

      /// <summary>
      ///    Представление, содержащие GameElement
      /// </summary>
      protected CannonView View { get; }

      /// <summary>
      ///    Обновление позиции GameElement и проверка столкновений со стенами
      /// </summary>
      /// <param name="interval">Интервал перемещения по вертикали</param>
      protected internal virtual void Update(double interval)
      {
         Shape.Offset(0, (int) (_velocityY * interval)); // Обновление вертикальной позиции

         // Если GameElement сталкивается со стеной, изменить направление
         if (Shape.Top < 0 && _velocityY < 0
             || Shape.Bottom > View.Height && _velocityY > 0)
            _velocityY *= -1; // Изменить скорость на противоположную
      }

      /// <summary>
      ///    Прорисовка GameElement
      /// </summary>
      /// <param name="canvas">Объект Canvas</param>
      public virtual void Draw(Canvas canvas) => canvas.DrawRect(Shape, Paint);

      /// <summary>
      ///    Воспроизведение звука, соответствующего типу GameElement
      /// </summary>
      public void PlaySound() => View.PlaySound(_soundId);
   }
}
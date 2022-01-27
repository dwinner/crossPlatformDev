using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   /// <summary>
   ///    Класс для представления мишени
   /// </summary>
   public class Target : GameElement
   {
      /// <summary>
      ///    Конструктор
      /// </summary>
      /// <param name="view">Представление логики игры</param>
      /// <param name="color">Цвет</param>
      /// <param name="hitReward">Прирост времени при попадании в мишень</param>
      /// <param name="x">Координата x левого верхнего элемента</param>
      /// <param name="y">Координата y левого верхнего элемента</param>
      /// <param name="width">Ширина GameElement</param>
      /// <param name="height">Высота GameElement</param>
      /// <param name="velocotyY">Начальная вертикальная скорость GameElement</param>
      public Target(CannonView view, Color color, int hitReward, int x, int y, int width, int height, float velocotyY)
         : base(view, color, CannonView.TargetSoundId, x, y, width, height, velocotyY) => HitReward = hitReward;

      /// <summary>
      ///    Прирост времени при попадании в мишень
      /// </summary>
      public int HitReward { get; }
   }
}
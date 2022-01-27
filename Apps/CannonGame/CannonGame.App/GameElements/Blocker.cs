using Android.Graphics;
using AppDevUnited.CannonGame.App.Based;

namespace AppDevUnited.CannonGame.App.GameElements
{
   /// <summary>
   ///    Класс для представления блока
   /// </summary>
   public class Blocker : GameElement
   {
      /// <summary>
      ///    Конструктор
      /// </summary>
      /// <param name="view">Представление логики игры</param>
      /// <param name="color">Цвет</param>
      /// <param name="missPenalty">Потеря времени при попадании в блок</param>
      /// <param name="x">Координата x левого верхнего элемента</param>
      /// <param name="y">Координата y левого верхнего элемента</param>
      /// <param name="width">Ширина GameElement</param>
      /// <param name="height">Высота GameElement</param>
      /// <param name="velocityY">Начальная вертикальная скорость GameElement</param>
      public Blocker(
         CannonView view, Color color, int missPenalty, int x, int y, int width, int height, float velocityY)
         : base(view, color, CannonView.BlockerSoundId, x, y, width, height, velocityY)
         => MissPenalty = missPenalty;

      /// <summary>
      ///    Потеря времени при попадании в блок
      /// </summary>
      public int MissPenalty { get; }
   }
}
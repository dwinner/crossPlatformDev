using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;

namespace AppDevUnited.TwitterSearches.App
{
   /// <summary>
   ///    Класс определяет разделители, отображаемые между элементами
   ///    RecyclerView; см. пример реализации bit.ly/DividerItemDecoration
   /// </summary>
   public class ItemDivider : RecyclerView.ItemDecoration
   {
      private readonly Drawable _divider;

      /// <summary>
      ///    Конструктор загружает встроенный разделитель элементов списка
      /// </summary>
      /// <param name="context">Контекст</param>
      public ItemDivider(Context context)
      {
         int[] attrs = {Android.Resource.Attribute.ListDivider};
         _divider = context.ObtainStyledAttributes(attrs).GetDrawable(0);
      }

      /// <summary>
      ///    Рисование разделителей элементов списка в RecyclerView
      /// </summary>
      /// <param name="canvas"></param>
      /// <param name="parent"></param>
      /// <param name="state"></param>
      public override void OnDrawOver(Canvas canvas, RecyclerView parent, RecyclerView.State state)
      {
         base.OnDrawOver(canvas, parent, state);

         // Вычисление координат x для всех разделителей
         var left = parent.PaddingLeft;
         var right = parent.Width - parent.PaddingRight;

         // Для каждого элемента, кроме последнего, нарисовать линию
         for (var i = 0; i < parent.ChildCount - 1; i++)
         {
            var item = parent.GetChildAt(i); // Получить i-й элемент списка

            // Вычисление координат у текущего разделителя
            var top = item.Bottom + ((RecyclerView.LayoutParams) item.LayoutParameters).BottomMargin;
            var bottom = top + _divider.IntrinsicHeight;

            // Рисование разделителя с вычисленными границами
            _divider.SetBounds(left, top, right, bottom);
            _divider.Draw(canvas);
         }
      }
   }
}
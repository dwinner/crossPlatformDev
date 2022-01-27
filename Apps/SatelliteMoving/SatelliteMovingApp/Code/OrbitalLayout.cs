using System;
using System.Collections.Generic;
using Android.Content;
using Android.Views;

namespace SatelliteMovingApp.Code
{
   /// <summary>
   ///    Менеджер расположения узлов View, упорядоченных по орбите включающего контейнера, начиная с центра
   /// </summary>
   public sealed class OrbitalLayout : ViewGroup
   {
      private List<Satellite> _satelliteList;
      private SortedDictionary<Satellite, View> _satelliteMap;

      /// <summary>
      ///    Создание расположения по орбите
      /// </summary>
      /// <param name="satelliteMap">Карта соответствия параметров спутника с его View</param>
      /// <param name="context">Контекст раположения</param>
      public OrbitalLayout(SortedDictionary<Satellite, View> satelliteMap, Context context)
         : this(context) => SatelliteMap = satelliteMap;

      private OrbitalLayout(Context context) : base(context)
      {
      }

      /// <summary>
      ///    Карта соответствия параметров спутника с его View
      /// </summary>
      private SortedDictionary<Satellite, View> SatelliteMap
      {
         get => _satelliteMap;
         set
         {
            _satelliteMap = value;
            var layoutParams = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
            foreach (var view in _satelliteMap.Values)
               AddView(view, layoutParams);
            _satelliteList = new List<Satellite>(SatelliteMap.Keys);
         }
      }

      protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
      {
         var availableWidth = right - left;
         var availableHeight = bottom - top;
         var count = ChildCount;
         var (width, height) = MeasureChildrenSizes(availableWidth, availableHeight);
         availableWidth -= width;
         availableHeight -= height;
         var centerX = availableWidth / 2;
         var centerY = availableHeight / 2;

         for (var i = 0; i < count; i++)
         {
            var child = GetChildAt(i);

            int x, y;
            if (i == 0)
            {
               x = left + centerX;
               y = top + centerY;
            }
            else
            {
               var distance = _satelliteList[i].Distance;
               var angle = _satelliteList[i].Angle;
               x = (int) (left + centerX - distance * Math.Cos(angle));
               y = (int) (top + centerY - distance * Math.Sin(angle));
            }

            child.Layout(x, y, x + child.MeasuredWidth, y + child.MeasuredHeight);
         }
      }

      /// <summary>
      ///    Вычисление максимальных значений ширины и высоты дочерних узлов View во ViewGroup
      /// </summary>
      /// <param name="sw">Ширина контейнера</param>
      /// <param name="sh">Высота контейнера</param>
      /// <returns>(ширина, высота)</returns>
      private (int width, int height) MeasureChildrenSizes(int sw, int sh)
      {
         var maxWidth = 0;
         var maxHeight = 0;

         for (var i = 0; i < ChildCount; i++)
         {
            var child = GetChildAt(i);
            MeasureChild(child, sw, sh);

            maxWidth = Math.Max(maxWidth, child.MeasuredWidth);
            maxHeight = Math.Max(maxHeight, child.MeasuredHeight);
         }

         return (maxWidth, maxHeight);
      }
   }
}
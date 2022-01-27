using System;
using System.Collections.Generic;
using System.Linq;
using FileStorageApp.UI;
using Xamarin.Forms;

namespace FileStorageApp.Controls
{
   /// <summary>
   ///    Carousel layout
   /// </summary>
   internal sealed class CarouselLayout : Layout<View>
   {
      /// <summary>
      ///    The width of the layout
      /// </summary>
      public double LayoutWidth { get; set; }

      /// <summary>
      ///    Item template
      /// </summary>
      public DataTemplate ItemTemplate { get; set; }

      /// <summary>
      ///    Item source
      /// </summary>
      public IEnumerable<object> ItemSource { get; set; }

      public object this[int index] => index < ItemSource.Count() ? ItemSource.ToList()[index] : null;

      public void SubscribeDataChanges(IObservable<DataChange> dataChanges) =>
         dataChanges.Subscribe(x => { PackViews(); });

      public IEnumerable<Rectangle> ComputeLayout()
      {
         var layout = ComputeNiaveLayout();
         return layout.SelectMany(carouselRow => carouselRow);
      }

      protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
      {
         var layout = ComputeNiaveLayout();

         var last = layout[layout.Count - 1];

         var width = last.Count > 0 ? last[0].X + last.Width : 0;
         var height = last.Count > 0 ? last[0].Y + last.Height : 0;

         return new SizeRequest(new Size(width, height));
      }

      protected override void LayoutChildren(double x, double y, double width, double height)
      {
         var layout = ComputeLayout();
         var i = 0;
         foreach (var region in layout)
         {
            var child = Children[i];
            i++;
            LayoutChildIntoBoundingRegion(child, region);
         }
      }

      private void PackViews()
      {
         Children.Clear();

         if (ItemSource != null)
         {
            foreach (var cell in ItemSource)
            {
               // create specified view from func and add to the children
               var view = (View) ItemTemplate.CreateContent();
               view.BindingContext = cell;
               Children.Add(view);
            }
         }
      }

      private List<CarouselRow> ComputeNiaveLayout()
      {
         var result = new List<CarouselRow>();
         var row = new CarouselRow();
         result.Add(row);

         const int spacing = 20;
         const double y = 0;

         foreach (var child in Children)
         {
            var request = child.Measure(double.PositiveInfinity, double.PositiveInfinity);

            if (row.Count == 0)
            {
               row.Add(new Rectangle(0, y, LayoutWidth, Height));
               row.Height = request.Request.Height;
               continue;
            }

            var last = row[row.Count - 1];
            var x = last.Right + spacing;
            var childWidth = LayoutWidth;

            row.Add(new Rectangle(x, y, childWidth, Height));
            row.Width = x + childWidth;
            row.Height = Math.Max(row.Height, Height);
         }

         return result;
      }
   }
}
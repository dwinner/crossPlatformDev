using System;
using System.Collections.Generic;
using System.Linq;
using FileStorageApp.UI;
using Xamarin.Forms;

namespace FileStorageApp.Controls
{
   /// <summary>
   ///    Tetrix grid
   /// </summary>
   public class CarouselScroll : ScrollView
   {
      public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
         "ItemSource",
         typeof(IEnumerable<object>),
         typeof(CarouselLayout),
         Enumerable.Empty<object>(),
         propertyChanged: (bindable, oldvalues, newValues) =>
         {
            ((CarouselScroll) bindable)._carouselLayout.ItemSource = (IEnumerable<object>) newValues;
         });

      public static readonly BindableProperty DataChangesProperty = BindableProperty.Create(
         "DataChanges",
         typeof(IObservable<DataChange>),
         typeof(CarouselLayout),
         null,
         propertyChanged: (bindable, oldvalue, newValue) =>
         {
            ((CarouselScroll) bindable)._carouselLayout.SubscribeDataChanges((IObservable<DataChange>) newValue);
         });

      private readonly CarouselLayout _carouselLayout; // The wrap layout

      public CarouselScroll()
      {
         Orientation = ScrollOrientation.Horizontal;
         _carouselLayout = new CarouselLayout();
         Content = _carouselLayout;
      }

      public DataTemplate ItemTemplate
      {
         set => _carouselLayout.ItemTemplate = value;
      }

      public object GetSelectedItem(int selected) => _carouselLayout[selected];

      protected override void LayoutChildren(double x, double y, double width, double height)
      {
         base.LayoutChildren(x, y, width, height);

         if (_carouselLayout != null)
         {
            if (width > _carouselLayout.LayoutWidth)
            {
               _carouselLayout.LayoutWidth = width;
            }

            _carouselLayout.ComputeLayout();
         }
      }
   }
}
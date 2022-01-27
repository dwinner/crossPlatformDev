using System.Collections.Generic;
using Xamarin.Forms;

namespace FileStorageApp.Controls
{
   internal sealed class CarouselRow : List<Rectangle>
   {
      public double Height { get; set; }

      public double Width { get; set; }
   }
}
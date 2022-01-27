using System;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Widget;

namespace Gallery.Droid.Bitmap
{
   /// <summary>
   ///    Bitmap helpers.
   /// </summary>
   public static class BitmapHelpers
   {
      /// <summary>
      ///    Calculates the size of the in sample.
      /// </summary>
      /// <returns>The in sample size.</returns>
      /// <param name="options">Options.</param>
      /// <param name="reqWidth">Req width.</param>
      /// <param name="reqHeight">Req height.</param>
      public static int CalculateInSampleSize(BitmapFactory.Options options, int reqWidth, int reqHeight)
      {
         // Raw height and width of image
         float height = options.OutHeight;
         float width = options.OutWidth;
         var inSampleSize = 1D;

         if (height > reqHeight || width > reqWidth)
         {
            var halfHeight = (int) (height / 2);
            var halfWidth = (int) (width / 2);

            // Calculate a inSampleSize that is a power of 2 - the decoder will use a value that is a power of two anyway.
            while (halfHeight / inSampleSize > reqHeight && halfWidth / inSampleSize > reqWidth)
            {
               inSampleSize *= 2;
            }
         }

         return (int) inSampleSize;
      }

      /// <summary>
      ///    Creates the bitmap.
      /// </summary>
      /// <returns>The bitmap.</returns>
      /// <param name="imageView">Image view.</param>
      /// <param name="imageData">Image data.</param>
      public static async Task CreateBitmapAsync(ImageView imageView, byte[] imageData)
      {
         try
         {
            if (imageData != null)
            {
               var bm = await BitmapFactory.DecodeByteArrayAsync(imageData, 0, imageData.Length)
                  .ConfigureAwait(false);
               if (bm != null)
               {
                  imageView.SetImageBitmap(bm);
               }
            }
         }
         catch (Exception e)
         {
            Console.WriteLine($"Bitmap creation failed: {e}");
         }
      }
   }
}
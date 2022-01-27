using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Graphics;
using Gallery.Droid.Bitmap;
using Gallery.Shared;
using static Android.Provider.MediaStore.Images.ImageColumns;
using static Android.Provider.MediaStore.Images.Media;

namespace Gallery.Droid.Utils
{
   /// <summary>
   ///    Image handler.
   /// </summary>
   public static class ImageHandler
   {
      private const int DefaultFileCount = 100;

      /// <summary>
      ///    Gets the files.
      /// </summary>
      /// <returns>The files.</returns>
      public static IEnumerable<GalleryItem> GetFiles(Context context, int fileCount = DefaultFileCount)
      {
         var contentResolver = context.ContentResolver;
         string[] columns = {Id, Title, Data, DateAdded, MimeType, Size};
         var cursor = contentResolver.Query(ExternalContentUri, columns, null, null, null);
         var columnIndex = cursor.GetColumnIndex(columns[2]);
         var index = 0;

         // create max fileCount items
         while (cursor.MoveToNext() && index < fileCount)
         {
            index++;
            var url = cursor.GetString(columnIndex);
            var imageData = CreateCompressedImageDataFromBitmap(url);

            yield return new GalleryItem
            {
               Title = cursor.GetString(1),
               Date = cursor.GetString(3),
               ImageData = imageData,
               ImageUri = url
            };
         }
      }

      /// <summary>
      ///    Creates the compressed image data from bitmap.
      /// </summary>
      /// <returns>The compressed image data from bitmap.</returns>
      /// <param name="url">URL.</param>
      private static byte[] CreateCompressedImageDataFromBitmap(string url)
      {
         // This makes sure bitmap is not loaded into memory
         var options = new BitmapFactory.Options {InJustDecodeBounds = true};

         // Then get the properties of the bitmap
         BitmapFactory.DecodeFile(url, options);

         // CalculateInSampleSize calculates the right aspect ratio for the picture and then calculate
         // the factor where it will be downsampled with.
         options.InSampleSize = BitmapHelpers.CalculateInSampleSize(options, 1600, 1200);

         // Now that we know the downsampling factor, the right sized bitmap is loaded into memory.
         // So we set the InJustDecodeBounds to false because we now know the exact dimensions.
         options.InJustDecodeBounds = false;

         // Now we are loading it with the correct options. And saving precious memory.
         var bm = BitmapFactory.DecodeFile(url, options);

         // Convert it to Base64 by first converting the bitmap to
         // a byte array. Then convert the byte array to a Base64 String.
         using (var stream = new MemoryStream())
         {
            bm.Compress(Android.Graphics.Bitmap.CompressFormat.Jpeg, 80, stream);
            return stream.ToArray();
         }
      }
   }
}
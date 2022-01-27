using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AssetsLibrary;
using Foundation;
using Gallery.Shared;
using UIKit;

namespace Gallery.iOS.Helpers
{
   /// <summary>
   ///    Image handler.
   /// </summary>
   public class ImageHandler
   {
      private const int DefaultFileCount = 100;
      private readonly ALAssetsLibrary _assetLibrary; // The asset library.
      private readonly IList<string> _assets; // The assets.

      /// <summary>
      ///    Initializes a new instance of the <see cref="ImageHandler" /> class.
      /// </summary>
      public ImageHandler()
      {
         _assetLibrary = new ALAssetsLibrary();
         _assets = new List<string>();
         _assetLibrary.Enumerate(ALAssetsGroupType.SavedPhotos, GroupEnumerator, Console.WriteLine);
      }

      /// <summary>
      ///    Occurs when assets loaded.
      /// </summary>
      public event EventHandler AssetsLoaded;

      /// <summary>
      ///    Groups the enumerator.
      /// </summary>
      /// <param name="assetGroup">Asset group.</param>
      /// <param name="shouldStop">Should stop.</param>
      private void GroupEnumerator(ALAssetsGroup assetGroup, ref bool shouldStop)
      {
         if (assetGroup == null)
         {
            shouldStop = true;
            NotifyAssetsLoaded();
            return;
         }

         if (!shouldStop)
         {
            assetGroup.Enumerate(AssetEnumerator);
         }
      }

      /// <summary>
      ///    Assets the enumerator.
      /// </summary>
      /// <param name="asset">Asset.</param>
      /// <param name="index">Index.</param>
      /// <param name="shouldStop">Should stop.</param>
      private void AssetEnumerator(ALAsset asset, nint index, ref bool shouldStop)
      {
         if (asset == null)
         {
            shouldStop = true;
            return;
         }

         if (!shouldStop)
         {
            // add asset name to list
            _assets.Add(asset.AssetUrl.ToString());
         }
      }

      /// <summary>
      ///    Notifies the assets ready.
      /// </summary>
      private void NotifyAssetsLoaded() => AssetsLoaded?.Invoke(this, EventArgs.Empty);

      /// <summary>
      ///    Gets the files.
      /// </summary>
      /// <param name="fileCount"></param>
      /// <returns>The files.</returns>
      public IEnumerable<GalleryItem> CreateGalleryItems(int fileCount = DefaultFileCount)
      {
         foreach (var file in _assets.Take(fileCount))
         {
            using (var asset = SynchronousGetAsset(file))
            {
               if (asset != null)
               {
                  var thumbnail = asset.Thumbnail;
                  var image = UIImage.FromImage(thumbnail);
                  var jpegData = image.AsJPEG().ToArray();

                  yield return new GalleryItem
                  {
                     Title = file,
                     Date = asset.Date.ToString(),
                     ImageData = jpegData,
                     ImageUri = asset.AssetUrl.ToString()
                  };
               }
            }
         }
      }

      /// <summary>
      ///    Synchronouses the get asset.
      /// </summary>
      /// <returns>The get asset.</returns>
      /// <param name="filename">Filename.</param>
      public ALAsset SynchronousGetAsset(string filename)
      {
         var waiter = new ManualResetEvent(false);
         NSError error = null;
         ALAsset result = null;

         ThreadPool.QueueUserWorkItem(state =>
            _assetLibrary.AssetForUrl(
               new NSUrl(filename),
               asset =>
               {
                  result = asset;
                  waiter.Set();
               },
               e =>
               {
                  error = e;
                  waiter.Set();
               }));


         if (!waiter.WaitOne(TimeSpan.FromSeconds(10)))
         {
            throw new Exception($"Error Getting Asset : Timeout, Asset={filename}");
         }

         if (error != null)
         {
            throw new Exception(error.Description);
         }

         return result;
      }
   }
}
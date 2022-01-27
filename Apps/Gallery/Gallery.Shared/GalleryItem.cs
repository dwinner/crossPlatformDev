namespace Gallery.Shared
{
   /// <summary>
   ///    Gallery item.
   /// </summary>
   public class GalleryItem
   {
      /// <summary>
      ///    The image data.
      /// </summary>
      public byte[] ImageData { get; set; }

      /// <summary>
      ///    The image URI.
      /// </summary>
      public string ImageUri { get; set; }

      /// <summary>
      ///    The title.
      /// </summary>
      public string Title { get; set; }

      /// <summary>
      ///    The date.
      /// </summary>
      public string Date { get; set; }
   }
}
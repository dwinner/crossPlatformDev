using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.Views;
using Android.Widget;
using Gallery.Droid.Bitmap;
using Gallery.Droid.Utils;
using Gallery.Shared;
using Java.Lang;

namespace Gallery.Droid.Adapters
{
   /// <summary>
   ///    List adapter.
   /// </summary>
   public class ListAdapter : BaseAdapter
   {
      private readonly Activity _context; // The context
      private readonly List<GalleryItem> _items; // The items

      /// <summary>
      ///    Initializes a new instance of the <see cref="ListAdapter" /> class.
      /// </summary>
      /// <param name="context">Context.</param>
      public ListAdapter(Activity context)
      {
         _context = context;
         _items = new List<GalleryItem>();

         foreach (var galleryitem in ImageHandler.GetFiles(context))
         {
            _items.Add(galleryitem);
         }
      }

      /// <summary>
      ///    Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public override int Count => _items.Count;

      /// <summary>
      ///    Gets the item.
      /// </summary>
      /// <returns>The item.</returns>
      /// <param name="position">Position.</param>
      public override Object GetItem(int position) => null;

      /// <summary>
      ///    Gets the item by position.
      /// </summary>
      /// <returns>The item by position.</returns>
      /// <param name="position">Position.</param>
      public GalleryItem GetItemByPosition(int position) => _items[position];

      /// <summary>
      ///    Gets the item identifier.
      /// </summary>
      /// <returns>The item identifier.</returns>
      /// <param name="position">Position.</param>
      public override long GetItemId(int position) => position;

      /// <summary>
      ///    Gets the view.
      /// </summary>
      /// <returns>The view.</returns>
      /// <param name="position">Position.</param>
      /// <param name="convertView">Convert view.</param>
      /// <param name="parent">Parent.</param>
      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         // re-use an existing view, if one is available, otherwise create a new one
         // TODO: Use view-holder pattern to acellerate performance
         var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.custom_cell, null);

         // set image
         var imageView = view.FindViewById<ImageView>(Resource.Id.image);
         BitmapHelpers.CreateBitmapAsync(imageView, _items[position].ImageData)
            .RunSynchronously(TaskScheduler.FromCurrentSynchronizationContext());

         // set labels
         var titleTextView = view.FindViewById<TextView>(Resource.Id.title);
         titleTextView.Text = _items[position].Title;
         var dateTextView = view.FindViewById<TextView>(Resource.Id.date);
         dateTextView.Text = _items[position].Date;

         return view;
      }
   }
}
using System;
using System.Collections.Generic;
using Foundation;
using Gallery.Shared;
using UIKit;

namespace Gallery.iOS.Sources
{
   /// <summary>
   ///    Table source.
   /// </summary>
   public class TableSource : UITableViewSource
   {
      /// <summary>
      ///    The cell identifier.
      /// </summary>
      private const string CellIdentifier = "GalleryCell";

      private const int DefaultRowHeight = 100;

      /// <summary>
      ///    The gallery items.
      /// </summary>
      private readonly List<GalleryItem> _galleryItems;

      /// <summary>
      ///    Initializes a new instance of the <see cref="TableSource"/> class.
      /// </summary>
      public TableSource() => _galleryItems = new List<GalleryItem>();

      /// <summary>
      ///    Occurs when row selected.
      /// </summary>
      public event EventHandler<GalleryItem> ItemSelected;

      /// <summary>
      ///    Updates the gallery items.
      /// </summary>
      public void UpdateGalleryItems(IEnumerable<GalleryItem> galleryList)
      {
         foreach (var galleryItem in galleryList)
         {
            _galleryItems.Add(galleryItem);
         }
      }

      /// <summary>
      ///    Numbers the of sections.
      /// </summary>
      /// <returns>The of sections.</returns>
      /// <param name="tableView">Table view.</param>
      public override nint NumberOfSections(UITableView tableView) => 1;

      /// <summary>
      ///    Called by the TableView to determine how many cells to create for that particular section.
      /// </summary>
      public override nint RowsInSection(UITableView tableview, nint section) => _galleryItems.Count;

      /// <summary>
      ///    Called when a row is touched
      /// </summary>
      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
      {
         ItemSelected?.Invoke(this, _galleryItems[indexPath.Row]);
         tableView.DeselectRow(indexPath, true);
      }

      /// <summary>
      ///    Gets the height for row.
      /// </summary>
      /// <returns>The height for row.</returns>
      /// <param name="tableView">Table view.</param>
      /// <param name="indexPath">Index path.</param>
      public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => DefaultRowHeight;

      /// <summary>
      ///    Called by the TableView to get the actual UITableViewCell to render for the particular row
      /// </summary>
      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
         // we create a new cell if this row has not been created yet
         var cell = (GalleryCell) tableView.DequeueReusableCell(CellIdentifier) ?? new GalleryCell(CellIdentifier);
         var galleryItem = _galleryItems[indexPath.Row];
         cell.UpdateCell(galleryItem);

         return cell;
      }
   }
}
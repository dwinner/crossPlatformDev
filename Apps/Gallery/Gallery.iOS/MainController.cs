using System;
using CoreGraphics;
using Gallery.iOS.Helpers;
using Gallery.iOS.Sources;
using UIKit;

namespace Gallery.iOS
{
   /// <summary>
   ///    Main controller.
   /// </summary>
   public partial class MainController : UIViewController
   {
      private readonly ImageHandler _imageHandler; // The image handler
      private readonly TableSource _source; // The source
      private UITableView _tableView; // The table

      /// <summary>
      ///    Initializes a new instance of the <see cref="MainController" /> class.
      /// </summary>
      public MainController()
         : base(nameof(MainController), null)
      {
         _source = new TableSource();
         _source.ItemSelected += (sender, e) =>
         {
            var asset = _imageHandler.SynchronousGetAsset(e.Title);
            NavigationController.PushViewController(new PhotoController(asset), true);
         };

         _imageHandler = new ImageHandler();
         _imageHandler.AssetsLoaded += HandleAssetsLoaded;
      }

      /// <summary>
      ///    Handles the assets loaded.
      /// </summary>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private void HandleAssetsLoaded(object sender, EventArgs e)
      {
         _source.UpdateGalleryItems(_imageHandler.CreateGalleryItems());
         _tableView.ReloadData();
      }

      /// <summary>
      ///    Views the did load.
      /// </summary>
      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         var width = View.Bounds.Width;
         var height = View.Bounds.Height;

         _tableView = new UITableView(new CGRect(0, 0, width, height))
         {
            AutoresizingMask = UIViewAutoresizing.All,
            Source = _source
         };

         Add(_tableView);
      }
   }
}
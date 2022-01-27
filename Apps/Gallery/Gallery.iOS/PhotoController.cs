using AssetsLibrary;
using Foundation;
using UIKit;

namespace Gallery.iOS
{
   /// <summary>
   ///    Main controller.
   /// </summary>
   public partial class PhotoController : UIViewController
   {
      private const string ImgCstr = "imageView";
      private const string TitleCstr = "titleLabel";
      private const string DateCstr = "dateLabel";
      private readonly UILabel _dateLabel; // The date label
      private readonly UIImageView _imageView; // The image view
      private readonly UILabel _titleLabel; // The title label

      /// <summary>
      ///    Initializes a new instance of the <see cref="Gallery.iOS.PhotoController" /> class.
      /// </summary>
      public PhotoController(ALAsset asset)
         : base(nameof(PhotoController), null)
      {
         _imageView = new UIImageView
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            ContentMode = UIViewContentMode.ScaleAspectFit
         };

         _titleLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         _dateLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         _imageView.Image = new UIImage(asset.DefaultRepresentation.GetFullScreenImage());
         _titleLabel.Text = asset.DefaultRepresentation.Filename;
         _dateLabel.Text = asset.Date.ToString();
      }

      /// <summary>
      ///    Views the did load.
      /// </summary>
      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         View.Add(_imageView);
         View.Add(_titleLabel);
         View.Add(_dateLabel);

         // set layout constraints for main view
         var allRelativeConstraint = NSLayoutConstraint.FromVisualFormat(
            $"V:|[{ImgCstr}]-10-[{TitleCstr}(50)]-10-[{DateCstr}(50)]|", NSLayoutFormatOptions.DirectionLeftToRight,
            null, new NSDictionary(ImgCstr, _imageView, TitleCstr, _titleLabel, DateCstr, _dateLabel));
         View.AddConstraints(allRelativeConstraint);

         var imgConstraint = NSLayoutConstraint.FromVisualFormat($"H:|[{ImgCstr}]|", NSLayoutFormatOptions.AlignAllTop,
            null, new NSDictionary(ImgCstr, _imageView));
         View.AddConstraints(imgConstraint);

         var titleConstraint = NSLayoutConstraint.FromVisualFormat($"H:|[{TitleCstr}]|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(TitleCstr, _titleLabel));
         View.AddConstraints(titleConstraint);

         var dateConstraint = NSLayoutConstraint.FromVisualFormat($"H:|[{DateCstr}]|",
            NSLayoutFormatOptions.AlignAllTop, null, new NSDictionary(DateCstr, _dateLabel));
         View.AddConstraints(dateConstraint);
      }
   }
}
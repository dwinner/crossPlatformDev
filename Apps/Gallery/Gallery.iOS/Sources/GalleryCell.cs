using Foundation;
using Gallery.Shared;
using UIKit;

namespace Gallery.iOS.Sources
{
   /// <summary>
   ///    Gallery cell.
   /// </summary>
   public sealed class GalleryCell : UITableViewCell
   {
      private const string ImageCstr = "imageView";
      private const string DateCstr = "dateLabel";
      private const string TitleCstr = "titleLabel";

      private readonly UILabel _dateLabel; // The date label
      private readonly UIImageView _imageView; // The image view
      private readonly UILabel _titleLabel; // The title label

      /// <summary>
      ///    Initializes a new instance of the <see cref="GalleryCell" /> class.
      /// </summary>
      /// <param name="cellId">Cell identifier.</param>
      public GalleryCell(string cellId)
         : base(UITableViewCellStyle.Default, cellId)
      {
         SelectionStyle = UITableViewCellSelectionStyle.Gray;

         _imageView = new UIImageView
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         _titleLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         _dateLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false
         };

         ContentView.Add(_imageView);
         ContentView.Add(_titleLabel);
         ContentView.Add(_dateLabel);
      }

      /// <summary>
      ///    Updates the cell.
      /// </summary>
      /// <returns>The cell.</returns>
      /// <param name="galleryItem">Gallery item.</param>
      public void UpdateCell(GalleryItem galleryItem)
      {
         _imageView.Image = UIImage.LoadFromData(NSData.FromArray(galleryItem.ImageData));
         _titleLabel.Text = galleryItem.Title;
         _dateLabel.Text = galleryItem.Date;
      }

      /// <summary>
      ///    Layouts the subviews.
      /// </summary>
      public override void LayoutSubviews()
      {
         base.LayoutSubviews();

         ContentView.TranslatesAutoresizingMaskIntoConstraints = false;

         // set layout constraints for main view
         var imageHeightConstraint = NSLayoutConstraint.FromVisualFormat($"V:|[{ImageCstr}(100)]|",
            NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary(ImageCstr, _imageView));
         AddConstraints(imageHeightConstraint);

         var titleHeightConstraint = NSLayoutConstraint.FromVisualFormat($"V:|[{TitleCstr}]|",
            NSLayoutFormatOptions.DirectionLeftToRight, null, new NSDictionary(TitleCstr, _titleLabel));
         AddConstraints(titleHeightConstraint);

         var imgToTitleConstraint = NSLayoutConstraint.FromVisualFormat(
            $"H:|-10-[{ImageCstr}(100)]-10-[{TitleCstr}]-10-|", NSLayoutFormatOptions.AlignAllTop, null,
            new NSDictionary(ImageCstr, _imageView, TitleCstr, _titleLabel));
         AddConstraints(imgToTitleConstraint);

         var imgToDateConstraint = NSLayoutConstraint.FromVisualFormat(
            $"H:|-10-[{ImageCstr}(100)]-10-[{DateCstr}]-10-|", NSLayoutFormatOptions.AlignAllTop, null,
            new NSDictionary(ImageCstr, _imageView, DateCstr, _dateLabel));
         AddConstraints(imgToDateConstraint);
      }
   }
}
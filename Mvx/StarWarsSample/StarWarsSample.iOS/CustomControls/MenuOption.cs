using Cirrious.FluentLayouts.Touch;
using UIKit;

namespace StarWarsSample.iOS.CustomControls
{
   public class MenuOption : BaseView
   {
      private const float Padding = 16f;
      private const float ImageSize = 22f;

      public UIImageView Image { get; set; }

      public UILabel Label { get; set; }

      public UIView Line { get; set; }

      protected override void CreateViews()
      {
         base.CreateViews();

         Image = new UIImageView();

         Label = new UILabel
         {
            TextColor = UIColor.Red,
            Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold)
         };

         Line = new UIView {BackgroundColor = UIColor.LightGray};

         AddSubviews(Image, Label, Line);
         this.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();
      }

      protected override void CreateConstraints()
      {
         base.CreateConstraints();

         this.AddConstraints(
            Image.AtLeftOf(this, Padding),
            Image.WithSameCenterY(this),
            Image.Width().EqualTo(ImageSize),
            Image.Height().EqualTo(ImageSize),
            Label.ToRightOf(Image, Padding / 2),
            Label.AtTopOf(this, Padding),
            Label.AtRightOf(this, Padding),
            Line.Below(Label, Padding),
            Line.AtLeftOf(this, Padding),
            Line.AtRightOf(this),
            Line.AtBottomOf(this),
            Line.Height().EqualTo(.5f)
         );
      }
   }
}
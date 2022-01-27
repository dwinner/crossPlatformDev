using System;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace StarWarsSample.iOS.Views.Cells
{
   public class NameTableViewCell : BaseTableViewCell
   {
      private const float Padding = 12f;

      private UILabel _lblName;

      public NameTableViewCell(IntPtr handle)
         : base(handle)
      {
      }

      protected override void CreateView()
      {
         base.CreateView();

         SelectionStyle = UITableViewCellSelectionStyle.None;
         _lblName = new UILabel
         {
            TextColor = UIColor.Red,
            Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold)
         };

         BackgroundColor = UIColor.Clear;
         ContentView.AddSubview(_lblName);
         ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

         this.DelayBind(
            () => { this.AddBindings(_lblName, "Text Name"); });
      }

      protected override void CreateConstraints()
      {
         base.CreateConstraints();

         ContentView.AddConstraints(
            _lblName.AtLeftOf(ContentView, Padding),
            _lblName.AtTopOf(ContentView, Padding),
            _lblName.AtBottomOf(ContentView, Padding),
            _lblName.AtRightOf(ContentView, Padding)
         );
      }
   }
}
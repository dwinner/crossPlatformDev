using Cirrious.FluentLayouts.Touch;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Binding.Views.Gestures;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using StarWarsSample.Core.Resources;
using StarWarsSample.Core.ViewModels;
using StarWarsSample.iOS.CustomControls;
using TZStackView;
using UIKit;

namespace StarWarsSample.iOS.Views
{
   [MvxTabPresentation(
      WrapInNavigationController = true,
      TabName = "Menu",
      TabIconName = "ic_menu")]
   public class MenuView : MvxViewController<MenuViewModel>
   {
      private const float ProfileImageSize = 80f;

      private UIImageView _imgBackground;
      private UIImageView _imgHeaderBackground, _imgHeaderProfile;
      private UILabel _lblHeaderName;
      private UIView _lineHeaderTop, _lineHeaderBottom;
      private StackView _options;

      private MenuOption _optionStatistics, _optionOther1, _optionOther2;

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         View.BackgroundColor = UIColor.Black;
         Title = Strings.Menu;
         _imgBackground = new UIImageView(UIImage.FromBundle("Background.jpg"));
         _options = new StackView
         {
            Axis = UILayoutConstraintAxis.Vertical
         };

         _lineHeaderTop = new UIView {BackgroundColor = UIColor.White};
         _lineHeaderBottom = new UIView {BackgroundColor = UIColor.White};
         _imgHeaderBackground = new UIImageView(UIImage.FromBundle("menu_header_background"))
         {
            ContentMode = UIViewContentMode.ScaleAspectFit
         };
         _imgHeaderProfile = new UIImageView(UIImage.FromBundle("profile"))
         {
            BackgroundColor = UIColor.Black,
            ContentMode = UIViewContentMode.ScaleAspectFit,
            ClipsToBounds = true
         };
         _imgHeaderProfile.Layer.CornerRadius = ProfileImageSize / 2;
         _lblHeaderName = new UILabel
         {
            Font = UIFont.SystemFontOfSize(22f, UIFontWeight.Bold),
            TextColor = UIColor.White,
            Text = "Darth Vader"
         };

         _optionStatistics = new MenuOption
         {
            Image = {Image = UIImage.FromBundle("ic_statistics")},
            Label = {Text = Strings.Statistics}
         };
         _optionOther1 = new MenuOption
         {
            Image = {Image = UIImage.FromBundle("ic_another_option")},
            Label = {Text = Strings.AnotherOption}
         };
         _optionOther2 = new MenuOption
         {
            Image = {Image = UIImage.FromBundle("ic_another_option")},
            Label = {Text = Strings.AnotherOption}
         };

         View.AddSubviews(_imgBackground, _lineHeaderTop, _imgHeaderBackground, _lineHeaderBottom, _imgHeaderProfile,
            _lblHeaderName, _options);
         View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

         _options.AddArrangedSubview(_optionStatistics);
         _options.AddArrangedSubview(_optionOther1);
         _options.AddArrangedSubview(_optionOther2);

         View.AddConstraints(
            _imgBackground.AtLeftOf(View),
            _imgBackground.AtTopOf(View),
            _imgBackground.AtBottomOf(View),
            _imgBackground.AtRightOf(View),
            _lineHeaderTop.AtLeftOf(View),
            _lineHeaderTop.AtTopOf(View),
            _lineHeaderTop.AtRightOf(View),
            _lineHeaderTop.Height().EqualTo(.5f),
            _imgHeaderBackground.Below(_lineHeaderTop),
            _imgHeaderBackground.AtLeftOf(View),
            _imgHeaderBackground.AtRightOf(View),
            _imgHeaderBackground.Height().EqualTo(180f),
            _lineHeaderBottom.Below(_imgHeaderBackground),
            _lineHeaderBottom.AtLeftOf(View),
            _lineHeaderBottom.AtRightOf(View),
            _lineHeaderBottom.Height().EqualTo(.5f),
            _imgHeaderProfile.WithSameCenterX(_imgHeaderBackground),
            _imgHeaderProfile.WithSameCenterY(_imgHeaderBackground).Minus(20f),
            _imgHeaderProfile.Width().EqualTo(ProfileImageSize),
            _imgHeaderProfile.Height().EqualTo(ProfileImageSize),
            _lblHeaderName.WithSameCenterX(_imgHeaderBackground),
            _lblHeaderName.Below(_imgHeaderProfile),
            _options.Below(_lineHeaderBottom),
            _options.AtLeftOf(View),
            _options.AtRightOf(View)
         );

         var set = this.CreateBindingSet<MenuView, MenuViewModel>();
         set.Bind(_optionStatistics.Tap()).For(g => g.Command).To(vm => vm.ShowStatusCommand);
         set.Apply();
      }
   }
}
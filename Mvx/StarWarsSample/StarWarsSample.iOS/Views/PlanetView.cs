using Airbnb.Lottie;
using Cirrious.FluentLayouts.Touch;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Color.Platforms.Ios;
using MvvmCross.ViewModels;
using StarWarsSample.Core;
using StarWarsSample.Core.MvxInteraction;
using StarWarsSample.Core.Resources;
using StarWarsSample.Core.ViewModels;
using StarWarsSample.iOS.CustomControls;
using StarWarsSample.iOS.Extensions;
using TZStackView;
using UIKit;

namespace StarWarsSample.iOS.Views
{
   [MvxChildPresentation]
   public class PlanetView : MvxViewController<PlanetViewModel>
   {
      private const float HeaderHeight = 160f;

      private LOTAnimationView _animation;
      private UIButton _btnDestroy;
      private UIView _contentView;

      private IMvxInteraction<DestructionAction> _interaction;
      private UILabel _lblName;
      private UIView _line;

      private UIScrollView _scrollView;
      private StackView _stackInfo;
      private TwitterCoverImageView _twitterCoverImageView;
      private InfoView _viewClimate, _viewDiameter, _viewGravity, _viewTerrain, _viewPopulation;

      public IMvxInteraction<DestructionAction> Interaction
      {
         get => _interaction;
         set
         {
            if (_interaction != null)
            {
               _interaction.Requested -= OnInteractionRequested;
            }

            _interaction = value;
            _interaction.Requested += OnInteractionRequested;
         }
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         Title = "Planet Target details";

         View.BackgroundColor = UIColor.Black;

         _scrollView = new UIScrollView();

         _twitterCoverImageView = new TwitterCoverImageView
         {
            CoverViewHeight = HeaderHeight,
            BackgroundColor = UIColor.Clear,
            Image = UIImage.FromBundle("Planet_Header.jpg"),
            ScrollView = _scrollView
         };

         _line = new UIView
         {
            BackgroundColor = UIColor.LightGray
         };

         _contentView = new UIView();

         _stackInfo = new StackView
         {
            Axis = UILayoutConstraintAxis.Vertical,
            Spacing = 8f
         };
         _lblName = new UILabel
         {
            Font = UIFont.SystemFontOfSize(28f, UIFontWeight.Bold),
            TextColor = AppColors.AccentColor.ToNativeColor()
         };

         _viewClimate = new InfoView {Label = {Text = Strings.Climate}};

         _viewDiameter = new InfoView {Label = {Text = Strings.Diameter}};

         _viewGravity = new InfoView {Label = {Text = Strings.Gravity}};

         _viewTerrain = new InfoView {Label = {Text = Strings.Terrain}};

         _viewPopulation = new InfoView {Label = {Text = Strings.Population}};

         _btnDestroy = new UIButton
         {
            BackgroundColor = UIColor.Red
         };
         _btnDestroy.Layer.CornerRadius = 8f;
         _btnDestroy.SetTitle(Strings.Destroy.ToUpper(), UIControlState.Normal);
         _btnDestroy.SetTitleColor(UIColor.White, UIControlState.Normal);
         _btnDestroy.SetTitleColor(UIColor.LightGray, UIControlState.Selected);
         _btnDestroy.PulseToSize(1.2f, 2f, true, true);

         _animation = LOTAnimationView.AnimationNamed("starwars");
         _animation.Hidden = true;

         _scrollView.AddSubview(_twitterCoverImageView);

         View.AddSubviews(_scrollView);
         View.AddSubview(_animation);
         View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

         _scrollView.AddSubviews(_lblName, _line, _contentView);
         _scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

         _contentView.AddSubviews(_stackInfo, _btnDestroy);
         _contentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

         _stackInfo.AddArrangedSubview(_viewClimate);
         _stackInfo.AddArrangedSubview(_viewDiameter);
         _stackInfo.AddArrangedSubview(_viewGravity);
         _stackInfo.AddArrangedSubview(_viewTerrain);
         _stackInfo.AddArrangedSubview(_viewPopulation);

         View.AddConstraints(
            _scrollView.AtLeftOf(View),
            _scrollView.AtTopOf(View),
            _scrollView.AtRightOf(View),
            _scrollView.AtBottomOf(View),
            _animation.AtLeftOf(View),
            _animation.AtTopOf(View),
            _animation.AtRightOf(View),
            _animation.AtBottomOf(View)
         );

         _scrollView.AddConstraints(
            _line.AtTopOf(_scrollView, HeaderHeight),
            _line.AtLeftOf(_scrollView),
            _line.AtRightOf(_scrollView),
            _line.Height().EqualTo(.5f),
            _lblName.Above(_line, 8f),
            _lblName.AtLeftOf(_line, 8f),
            _contentView.Below(_line),
            _contentView.AtLeftOf(_scrollView),
            _contentView.AtRightOf(_scrollView),
            _contentView.AtBottomOf(_scrollView)
         );

         View.AddConstraints(
            _contentView.WithSameWidth(View)
         );

         _contentView.AddConstraints(
            _stackInfo.AtTopOf(_contentView),
            _stackInfo.AtLeftOf(_contentView),
            _stackInfo.AtRightOf(_contentView),
            _btnDestroy.Below(_stackInfo, 20f),
            _btnDestroy.WithSameCenterX(_contentView),
            _btnDestroy.AtBottomOf(_contentView, 250f),
            _btnDestroy.Width().EqualTo(120f)
         );

         var set = this.CreateBindingSet<PlanetView, PlanetViewModel>();
         set.Bind(_lblName).To(vm => vm.Planet.Name);
         set.Bind(_viewClimate.Information).To(vm => vm.Planet.Climate);
         set.Bind(_viewDiameter.Information).To(vm => vm.Planet.Diameter);
         set.Bind(_viewGravity.Information).To(vm => vm.Planet.Gravity);
         set.Bind(_viewTerrain.Information).To(vm => vm.Planet.Terrain);
         set.Bind(_viewPopulation.Information).To(vm => vm.Planet.Population);
         set.Bind(_btnDestroy).To(vm => vm.DestroyPlanetCommand);
         set.Bind(this).For(v => v.Interaction).To(vm => vm.Interaction);
         set.Apply();
      }

      public override void ViewWillAppear(bool animated)
      {
         base.ViewWillAppear(animated);

         NavigationController.SetNavigationBarHidden(true, false);
      }

      public override void ViewWillDisappear(bool animated)
      {
         base.ViewWillDisappear(animated);

         NavigationController.SetNavigationBarHidden(false, false);
      }

      private void OnInteractionRequested(object sender, MvxValueEventArgs<DestructionAction> eventArgs)
      {
         _animation.Hidden = false;
         _animation.PlayWithCompletion(
            animationFinished => { eventArgs.Value.OnDestroyed(); });
      }
   }
}
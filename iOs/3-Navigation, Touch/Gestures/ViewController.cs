using System;
using System.Drawing;
using CoreGraphics;
using UIKit;

namespace Gestures
{
   public partial class ViewController : UIViewController
   {
      private nfloat _lastRotation;
      private nfloat _lastScale = 1.0f;
      private CGPoint _lastTranslation;
      private UIPinchGestureRecognizer _pinchGestureRecognizer;
      private UIRotationGestureRecognizer _rotationGestureRecognizer;

      private UIView _square;

      protected ViewController(IntPtr handle) : base(handle)
      {
      }

      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         AddSquare(50.0f, UIColor.Purple);
         AddPanGestureRecognizer();
         AddRotationAndPinchGestureRecognizers();
      }

      private void AddSquare(float squareSideLength, UIColor color)
      {
         _square = new UIView
         {
            BackgroundColor = color,
            Frame = new RectangleF(0, 0, squareSideLength, squareSideLength),
            Center = new CGPoint(View.Frame.Width / 2.0, View.Frame.Height / 2.0)
         };

         Add(_square);
      }

      private bool IsTouchLocationWithinSquare(UIGestureRecognizer panGestureRecognizer)
      {
         var location = panGestureRecognizer.LocationInView(View);
         return _square.Frame.Contains(location.X, location.Y);
      }

      private void UpdateSquareTransform(CGPoint translation, nfloat rotation, nfloat scale)
      {
         var transform = CGAffineTransform.MakeIdentity();

         Console.WriteLine(_lastScale);

         // Include previous translation, rotation and scale
         translation.X += _lastTranslation.X;
         translation.Y += _lastTranslation.Y;
         rotation += _lastRotation;
         scale *= _lastScale;

         // Combine translation, rotation and scale
         transform = CGAffineTransform.Translate(transform, translation.X, translation.Y);
         transform = CGAffineTransform.Rotate(transform, rotation);
         transform = CGAffineTransform.Scale(transform, scale, scale);

         _square.Transform = transform;
      }

      private void AddPanGestureRecognizer()
      {
         var panGestureRecognizer = new UIPanGestureRecognizer(TranslateSquare);
         View.AddGestureRecognizer(panGestureRecognizer);
      }

      private void TranslateSquare(UIPanGestureRecognizer sender)
      {
         if (IsTouchLocationWithinSquare(sender))
         {
            var translation = sender.TranslationInView(View);
            UpdateSquareTransform(translation, 0.0f, 1.0f);
         }

         if (sender.State == UIGestureRecognizerState.Ended)
         {
            _lastTranslation.X = _square.Transform.x0;
            _lastTranslation.Y = _square.Transform.y0;
         }
      }

      private void AddRotationAndPinchGestureRecognizers()
      {
         _rotationGestureRecognizer = new UIRotationGestureRecognizer(RotateSquare);
         _pinchGestureRecognizer = new UIPinchGestureRecognizer(ScaleSquare);

         _rotationGestureRecognizer.ShouldRecognizeSimultaneously += GestureRecognizer_ShouldRecognizeSimultaneously;
         _pinchGestureRecognizer.ShouldRecognizeSimultaneously += GestureRecognizer_ShouldRecognizeSimultaneously;

         View.AddGestureRecognizer(_pinchGestureRecognizer);
         View.AddGestureRecognizer(_rotationGestureRecognizer);
      }

      private static bool GestureRecognizer_ShouldRecognizeSimultaneously(
         UIGestureRecognizer gestureRecognizer, UIGestureRecognizer otherGestureRecognizer) =>
         true;

      private void RotateSquare(UIRotationGestureRecognizer sender)
      {
         if (sender.State == UIGestureRecognizerState.Changed)
         {
            UpdateSquareTransform(new CGPoint(), sender.Rotation,
               _pinchGestureRecognizer.State == UIGestureRecognizerState.Changed
                  ? _pinchGestureRecognizer.Scale
                  : 1.0f);
         }

         if (sender.State == UIGestureRecognizerState.Ended)
         {
            _lastRotation += sender.Rotation;
         }
      }

      private void ScaleSquare(UIPinchGestureRecognizer sender)
      {
         if (sender.State == UIGestureRecognizerState.Changed)
         {
            UpdateSquareTransform(new CGPoint(),
               _rotationGestureRecognizer.State == UIGestureRecognizerState.Changed
                  ? _rotationGestureRecognizer.Rotation
                  : 0.0f, sender.Scale);
         }

         if (sender.State == UIGestureRecognizerState.Ended)
         {
            _lastScale *= sender.Scale;
         }
      }
   }
}
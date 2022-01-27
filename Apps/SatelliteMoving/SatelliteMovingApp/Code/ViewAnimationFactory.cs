using System;
using Android.Views.Animations;
using ViewAnimation = Android.Views.Animations.Animation;

namespace SatelliteMovingApp.Code
{
   /// <summary>
   ///    Генератор объектов для анимации представлений
   /// </summary>
   public static class ViewAnimationFactory
   {
      /// <summary>
      ///    Вращение View относительно центра
      /// </summary>
      /// <param name="distance">Расстояние от центра</param>
      /// <param name="angle">Угол расположения относительно горизонтали</param>
      /// <param name="duration">Время полного оборота</param>
      /// <returns>Объект Animation</returns>
      public static ViewAnimation CreateOrbitalRotation(float distance, float angle, long duration) =>
         new OrbitalAnimationImpl(distance, angle, duration);

      private sealed class OrbitalAnimationImpl : ViewAnimation
      {
         private readonly float _angle;
         private readonly float _distance;
         private readonly long _duration;
         private float _centerX;
         private float _centerY;

         public OrbitalAnimationImpl(float distance, float angle, long duration)
         {
            _distance = distance;
            _angle = angle;
            _duration = duration;
         }

         public override void Initialize(int width, int height, int parentWidth, int parentHeight)
         {
            base.Initialize(width, height, parentWidth, parentHeight);
            _centerX = (float) (_distance * Math.Cos(_angle)) + width / 2.0f;
            _centerY = (float) (_distance * Math.Sin(_angle)) + height / 2.0f;
            Duration = _duration;
            RepeatMode = RepeatMode.Restart;
            RepeatCount = Infinite;
            Interpolator = new LinearInterpolator();
         }

         protected override void ApplyTransformation(float interpolatedTime, Transformation t)
         {
            var matrix = t.Matrix;
            matrix.SetRotate(interpolatedTime * 360);
            matrix.PreTranslate(-_centerX, -_centerY);
            matrix.PostTranslate(_centerX, _centerY);
         }
      }
   }
}
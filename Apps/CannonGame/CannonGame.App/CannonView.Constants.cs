using System;
using Android.OS;

namespace AppDevUnited.CannonGame.App
{
   public sealed partial class CannonView
   {
      private const string ErrorTag = nameof(CannonView);

      // Игровые константы
      private const int MissPenalty = 2; // Штраф при промахе
      private const int HitReward = 3; // Прибавка при попадании

      // Константы для рисования пушки
      private const double CannonBaseRadiusPercent = 0.075D;
      private const double CannonBarrelWidthPercent = 0.075D;
      private const double CannonBarrelLengthPercent = 0.1D;

      // Константы для рисования ядра
      internal const double CannonBallSpeedPercent = 1.5D;
      internal const double CannonBallRadiusPercent = 0.0375D;

      // Константы для рисования мишеней
      private const double TargetWidthPercent = 0.025D;
      private const double TargetLengthPercent = 0.15D;
      private const double TargetFirstXPercent = 0.6D;
      private const double TargetSpacingPercent = 0.0166666666666667D;
      private const double TargetPieces = 9.0D;
      private const double TargetMinSpeedPercent = 0.75D;
      private const double TargetMaxSpeedPercent = 1.5D;

      // Константы для рисования блока
      private const double BlockerWidthPercent = 0.025D;
      private const double BlockerLengthPercent = 0.25D;
      private const double BlockerXPercent = 0.5D;
      private const double BlockerSpeedPercent = 1.0D;

      // Размер текста составляет 1/18 ширины экрана
      private const double TextSizePercent = 0.0555555555555556D;

      // Константы и переменные для управления звуком
      internal const int BlockerSoundId = 2;
      internal const int TargetSoundId = 0;
      internal const int CannonSoundId = 1;
      private const string ResultsTag = "results";
      private const int CountDownSeconds = 1000; // Обратный отсчет для игры

      private static readonly Func<bool> _MoreOrEqualThanKitKatFunc =
         () => Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat;
   }
}
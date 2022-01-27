namespace Xamanimation.Sample.Views
{
   public partial class AnimationsShell
   {
      public AnimationsShell()
      {
         InitializeComponent();

         animationExtensionButton.Clicked += async (sender, args) =>
            await animationBox.Animate(new HeartAnimation()).ConfigureAwait(true);
      }
   }
}
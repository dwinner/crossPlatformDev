using MvvmCross.Core.ViewModels;
using MvvmCross.iOS.Platform;
using MvvmCross.iOS.Views.Presenters;

namespace PictureTaking.Touch
{
   public class Setup : MvxIosSetup
   {
      public Setup(MvxApplicationDelegate applicationDelegate, IMvxIosViewPresenter presenter)
         : base(applicationDelegate, presenter)
      {
      }

      protected override IMvxApplication CreateApp() => new Core.App();
   }
}
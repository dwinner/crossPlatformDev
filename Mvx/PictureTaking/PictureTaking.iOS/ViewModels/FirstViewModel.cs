using MvvmCross.Core.ViewModels;

namespace PictureTaking.Touch.ViewModels
{
   public class FirstViewModel
      : MvxViewModel
   {
      private string _hello = "Hello MvvmCross";

      public string Hello
      {
         get => _hello;
         set
         {
            _hello = value;
            RaisePropertyChanged(() => Hello);
         }
      }
   }
}
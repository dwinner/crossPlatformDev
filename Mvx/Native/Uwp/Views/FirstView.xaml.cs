using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.ViewModels;

namespace MvvmCrossDemo.Uwp.Views
{
   [MvxViewFor(typeof(FirstViewModel))]
   public sealed partial class FirstView
   {
      public FirstView()
      {
         InitializeComponent();
      }
   }
}
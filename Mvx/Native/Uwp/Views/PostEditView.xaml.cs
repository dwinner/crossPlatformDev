using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.ViewModels;

namespace MvvmCrossDemo.Uwp.Views
{
   [MvxViewFor(typeof(PostEditViewModel))]
   public sealed partial class PostEditView
   {
      public PostEditView()
      {
         InitializeComponent();
      }
   }
}
using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.ViewModels;

namespace MvvmCrossDemo.Uwp.Views
{
   [MvxViewFor(typeof(PostDetailViewModel))]
   public sealed partial class PostDetailView
   {
      public PostDetailView()
      {
         InitializeComponent();
      }
   }
}
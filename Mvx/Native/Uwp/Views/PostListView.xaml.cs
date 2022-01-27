using MvvmCross.ViewModels;
using MvvmCrossDemo.Core.ViewModels;

namespace MvvmCrossDemo.Uwp.Views
{
   [MvxViewFor(typeof(PostListViewModel))]
   public sealed partial class PostListView
   {
      public PostListView()
      {
         InitializeComponent();
      }
   }
}
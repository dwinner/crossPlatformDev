using MvvmCross.ViewModels;

namespace MvvmCrossDemo.Core.ViewModels
{
   public class PostViewModel : MvxViewModel
   {
      private string _body;
      private int _id;
      private string _title;
      private int _userId;

      public int UserId
      {
         get => _userId;
         set => SetProperty(ref _userId, value);
      }

      public int Id
      {
         get => _id;
         set => SetProperty(ref _id, value);
      }

      public string Title
      {
         get => _title;
         set => SetProperty(ref _title, value);
      }

      public string Body
      {
         get => _body;
         set => SetProperty(ref _body, value);
      }
   }
}
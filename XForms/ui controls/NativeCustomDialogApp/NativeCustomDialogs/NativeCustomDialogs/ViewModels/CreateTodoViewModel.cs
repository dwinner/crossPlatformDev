using NativeCustomDialogs.Models;
using ReactiveUI;
using Xamarin.Forms;

namespace NativeCustomDialogs.ViewModels
{
   public class CreateTodoViewModel : ReactiveObject
   {
      private string _title;

      public CreateTodoViewModel()
      {
         CreateTodo = ReactiveCommand.Create(() =>
         {
            MessagingCenter.Send<object, Todo>(this, "ItemCreated", new Todo {Title = Title, IsDone = false});
         });
      }

      public ReactiveCommand CreateTodo { get; set; }

      public string Title
      {
         get => _title;
         set => this.RaiseAndSetIfChanged(ref _title, value);
      }
   }
}
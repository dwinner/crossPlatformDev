using ReactiveUI;

namespace NativeCustomDialogs.Models
{
   public class Todo : ReactiveObject
   {
      private bool _isDone;
      public string Title { get; set; }

      public bool IsDone
      {
         get => _isDone;
         set => this.RaiseAndSetIfChanged(ref _isDone, value);
      }

      public bool IsEnabled => !IsDone;
   }
}
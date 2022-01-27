using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmSample.Model
{
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      public virtual event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this,
            new PropertyChangedEventArgs(propertyName));
      }
   }
}
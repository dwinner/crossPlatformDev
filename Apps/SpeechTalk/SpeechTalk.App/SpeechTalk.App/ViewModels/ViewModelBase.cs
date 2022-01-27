using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SpeechTalk.App.ViewModels
{
   /// <summary>
   ///    The base class of all view models
   /// </summary>
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      /// <summary>
      ///    The property changed.
      /// </summary>
      public event PropertyChangedEventHandler PropertyChanged;

      /// <summary>
      ///    Raises the property changed event.
      /// </summary>
      /// <param name="propertyName">Property name.</param>
      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}
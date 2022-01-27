﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using MvvmSampleApp.Annotations;

namespace MvvmSampleApp.ViewModels
{
   public abstract class ViewModelBase : INotifyPropertyChanged
   {
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}
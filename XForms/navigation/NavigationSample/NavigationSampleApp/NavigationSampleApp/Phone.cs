using System.ComponentModel;
using System.Runtime.CompilerServices;
using NavigationSampleApp.Annotations;

namespace NavigationSampleApp
{
   public sealed class Phone : INotifyPropertyChanged
   {
      private string _company;
      private string _name;
      private int _price;

      public string Name
      {
         get => _name;
         set
         {
            if (_name != value)
            {
               _name = value;
               OnPropertyChanged();
            }
         }
      }

      public string Company
      {
         get => _company;
         set
         {
            if (_company != value)
            {
               _company = value;
               OnPropertyChanged();
            }
         }
      }

      public int Price
      {
         get => _price;
         set
         {
            if (_price != value)
            {
               _price = value;
               OnPropertyChanged();
            }
         }
      }

      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
   }
}
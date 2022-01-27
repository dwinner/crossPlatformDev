using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SQLiteExample.Interfaces;
using SQLiteExample.Models;

namespace SQLiteExample.ViewModel
{
   public class AddPetViewModel : ViewModelBase
   {
      private int _age;
      private string _breed;
      private RelayCommand _btnCommit;
      private bool _isDog;
      private string _name;
      private int _parent;
      private readonly IRepository _sqlService;

      public AddPetViewModel(IRepository repo) => _sqlService = repo;

      public int Parent
      {
         get => _parent;
         set { Set(() => Parent, ref _parent, value, true); }
      }

      public string Name
      {
         get => _name;
         set { Set(() => Name, ref _name, value, true); }
      }

      public string Breed
      {
         get => _breed;
         set { Set(() => Breed, ref _breed, value, true); }
      }

      public int Age
      {
         get => _age;
         set { Set(() => Age, ref _age, value, true); }
      }

      public bool IsDog
      {
         get => _isDog;
         set { Set(() => IsDog, ref _isDog, value, true); }
      }

      public RelayCommand BtnCommit
      {
         get
         {
            return _btnCommit ??
                   (_btnCommit = new RelayCommand(
                      () =>
                      {
                         var pet = new Pets
                         {
                            ParentId = Parent,
                            PetsName = Name,
                            Breed = Breed,
                            PetsAge = Age,
                            IsDog = IsDog
                         };
                         _sqlService.SaveData(pet);
                      }));
         }
      }
   }
}
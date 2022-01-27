using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SQLiteExample.Interfaces;
using SQLiteExample.Models;

namespace SQLiteExample.ViewModel
{
   public class AddHobbiesViewModel : ViewModelBase
   {
      private readonly IRepository _sqlService;
      private RelayCommand _btnCommit;
      private double _cost;
      private string _description;
      private string _frequency;
      private string _hobbyName;
      private int _parentId;

      public AddHobbiesViewModel(IRepository repo) => _sqlService = repo;

      public int ParentId
      {
         get => _parentId;
         set { Set(() => ParentId, ref _parentId, value, true); }
      }

      public string HobbyName
      {
         get => _hobbyName;
         set { Set(() => HobbyName, ref _hobbyName, value, true); }
      }

      public string HobbyDesc
      {
         get => _description;
         set { Set(() => HobbyDesc, ref _description, value, true); }
      }

      public double HobbyCost
      {
         get => _cost;
         set { Set(() => HobbyCost, ref _cost, value, true); }
      }

      public List<string> GetFrequencies => new List<string>
      {
         "Daily",
         "A few times a week",
         "Weekly",
         "A few times a month",
         "Monthly",
         "A few times a year",
         "Year",
         "When weather allows"
      };

      public string Frequency
      {
         get => _frequency;
         set { Set(() => Frequency, ref _frequency, value, true); }
      }

      public RelayCommand BtnCommit
      {
         get
         {
            return _btnCommit ??
                   (_btnCommit = new RelayCommand(
                      () =>
                      {
                         var pet = new Hobbies
                         {
                            ParentId = ParentId,
                            HobbyName = HobbyName,
                            Description = HobbyDesc,
                            Cost = HobbyCost,
                            FreqencyAttended = Frequency
                         };
                         _sqlService.SaveData(pet);
                      }));
         }
      }
   }
}
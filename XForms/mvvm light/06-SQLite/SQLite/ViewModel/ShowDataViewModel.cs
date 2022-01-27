using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;
using SQLiteExample.Interfaces;
using SQLiteExample.Models;

namespace SQLiteExample.ViewModel
{
   public class ShowDataViewModel : ViewModelBase
   {
      private readonly IRepository _sqlService;
      private int _parentId;

      public ShowDataViewModel(IRepository repo) => _sqlService = repo;

      public List<PersonalInfo> GetPersonInfo => _sqlService.GetList<PersonalInfo>();

      public int ParentId
      {
         get => _parentId;
         set { Set(() => ParentId, ref _parentId, value, true); }
      }

      public bool GetHasPet => _sqlService.GetList<Pets>().Count(t => t.ParentId == ParentId) != 0;

      public bool GetHasHobbies => _sqlService.GetList<Hobbies>().Count(t => t.ParentId == ParentId) != 0;
   }
}
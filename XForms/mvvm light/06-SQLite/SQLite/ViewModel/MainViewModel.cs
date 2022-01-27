using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SQLiteExample.Interfaces;
using SQLiteExample.Models;

namespace SQLiteExample.ViewModel
{
   public class MainViewModel : ViewModelBase
   {
      public static int Parent;
      private readonly IRepository _sqliteService;
      private string _addressOne;
      private string _addressThree;
      private string _addressTwo;
      private RelayCommand _btnCommit;
      private string _email;
      private string _mobileNumber;
      private string _name;
      private string _postcode;

      public MainViewModel(IRepository sql) => _sqliteService = sql;

      public int GetCurrentId => _sqliteService.GetId<PersonalInfo>();

      public int GetTotal => _sqliteService.GetList<PersonalInfo>().Count;

      public int SetParentId
      {
         get => Parent;
         set { Set(() => SetParentId, ref Parent, value); }
      }

      public string UserName
      {
         get => _name;
         set { Set(() => UserName, ref _name, value, true); }
      }

      public string AddressOne
      {
         get => _addressOne;
         set { Set(() => AddressOne, ref _addressOne, value, true); }
      }

      public string AddressTwo
      {
         get => _addressTwo;
         set { Set(() => AddressTwo, ref _addressTwo, value, true); }
      }

      public string AddressThree
      {
         get => _addressThree;
         set { Set(() => AddressThree, ref _addressThree, value, true); }
      }

      public string Postcode
      {
         get => _postcode;
         set { Set(() => Postcode, ref _postcode, value, true); }
      }

      public string Email
      {
         get => _email;
         set { Set(() => Email, ref _email, value, true); }
      }

      public string MobileNumber
      {
         get => _mobileNumber;
         set { Set(() => MobileNumber, ref _mobileNumber, value, true); }
      }

      public RelayCommand BtnCommit =>
         _btnCommit ??
         (_btnCommit = new RelayCommand(
            () =>
            {
               var person = new PersonalInfo
               {
                  Name = UserName,
                  AddressLineOne = AddressOne,
                  AddressLineTwo = AddressTwo,
                  AddressLineThree = AddressThree,
                  PostCode = Postcode,
                  EmailAddress = Email,
                  MobilePhone = MobileNumber
               };
               _sqliteService.SaveData(person);
            }));
   }
}
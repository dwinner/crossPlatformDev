using System;
using SQLite.Net.Attributes;
using SQLiteExample.Interfaces;

namespace SQLiteExample.Models
{
   public class PersonalInfo : IInterface
   {
      public string Name { get; set; }

      public string EmailAddress { get; set; }

      public string MobilePhone { get; set; }

      public string AddressLineOne { get; set; }

      public string AddressLineTwo { get; set; }

      public string AddressLineThree { get; set; }

      public string PostCode { get; set; }

      public DateTime LastUpdated { get; } = DateTime.Now;

      [PrimaryKey]
      [AutoIncrement]
      public int Id => 0;
   }
}
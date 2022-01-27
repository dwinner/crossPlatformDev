using Realms;

namespace RealmSampleApp
{
   public class Friend : RealmObject
   {
      [PrimaryKey]
      public string Id { get; set; }

      public string Name { get; set; }

      public string Email { get; set; }

      public string Phone { get; set; }
   }
}
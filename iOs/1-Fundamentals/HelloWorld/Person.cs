using System.IO;
using System.Text;
using Foundation;

namespace HelloWorld
{
   public class Person : NSCoding
   {
      private const string FirstNameArchiveKey = "FirstName";
      private const string LastNameArchiveKey = "LastName";
      private readonly string _archiveLocation = Path.Combine(Path.GetTempPath(), "person");

      public Person()
      {
      }

      [Export("initWithCoder:")]
      public Person(NSCoder coder)
      {
         FirstName = DecodeString(coder, FirstNameArchiveKey);
         LastName = DecodeString(coder, LastNameArchiveKey);
      }

      public string FirstName { get; set; } = string.Empty;

      public string LastName { get; set; } = string.Empty;

      public override void EncodeTo(NSCoder encoder)
      {
         EncodeString(encoder, FirstName, FirstNameArchiveKey);
         EncodeString(encoder, LastName, LastNameArchiveKey);
      }

      public void StoreValues() => NSKeyedArchiver.ArchiveRootObjectToFile(this, _archiveLocation);

      public void RestoreValues()
      {
         if (NSKeyedUnarchiver.UnarchiveFile(_archiveLocation) is Person retrievedPersonData)
         {
            FirstName = retrievedPersonData.FirstName;
            LastName = retrievedPersonData.LastName;
         }
      }

      private static void EncodeString(NSCoder encoder, string property, string propertyKey)
      {
         var buffer = Encoding.UTF8.GetBytes(property);
         encoder.Encode(buffer, propertyKey);
      }

      private static string DecodeString(NSCoder coder, string propertyKey)
      {
         var result = string.Empty;
         var bytes = coder.DecodeBytes(propertyKey);
         if (bytes != null)
         {
            result = Encoding.UTF8.GetString(bytes);
         }

         return result;
      }
   }
}
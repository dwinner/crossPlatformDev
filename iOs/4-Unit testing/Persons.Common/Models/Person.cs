namespace Persons.Common.Models
{
   public class Person
   {
      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string Email { get; set; }

      public int Age { get; set; }

      public int Id { get; set; }

      public string FullName() => $"{FirstName} {LastName}";

      public static bool IsEmailValid(string email) => email.Contains("@");

      public static Person Default =>
         new Person
         {
            FirstName = "Dawid",
            LastName = "Borycki",
            Age = 34,
            Email = "dawid@borycki.com.pl"
         };
   }
}
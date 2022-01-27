using System;

namespace MobileClient
{
   public class Friend : IEquatable<Friend>
   {
      public Friend()
         : this(0, "", "", "")
      {
      }

      public Friend(int id, string name, string email, string phone)
      {
         Id = id;
         Name = name;
         Email = email;
         Phone = phone;
      }

      public int Id { get; }

      public string Name { get; }

      public string Email { get; }

      public string Phone { get; }

      public bool Equals(Friend other) =>
         !ReferenceEquals(null, other) &&
         (ReferenceEquals(this, other) || Id == other.Id && string.Equals(Name, other.Name) &&
          string.Equals(Email, other.Email) && string.Equals(Phone, other.Phone));

      public override bool Equals(object obj) =>
         !ReferenceEquals(null, obj) &&
         (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((Friend) obj));

      public override int GetHashCode()
      {
         unchecked
         {
            var hashCode = Id;
            hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Email != null ? Email.GetHashCode() : 0);
            hashCode = (hashCode * 397) ^ (Phone != null ? Phone.GetHashCode() : 0);
            return hashCode;
         }
      }

      public static bool operator ==(Friend left, Friend right) => Equals(left, right);

      public static bool operator !=(Friend left, Friend right) => !Equals(left, right);
   }
}
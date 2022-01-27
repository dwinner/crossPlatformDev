using System;
using JetBrains.Annotations;
using Persons.Common.Models;
using UIKit;

namespace Persons
{
   public partial class ViewController : UIViewController
   {
      protected ViewController(IntPtr handle) : base(handle)
      {
         // Note: this .ctor should not contain any initialization logic.
      }

      [UsedImplicitly]
      partial void ButtonDisplayPersonData_TouchUpInside(UIButton sender)
      {
         var defaultPerson = Person.Default;

         TextFieldFirstName.Text = defaultPerson.FirstName;
         TextFieldLastName.Text = defaultPerson.LastName;
         TextFieldAge.Text = defaultPerson.Age.ToString();
         TextFieldEmail.Text = defaultPerson.Email;
      }
   }
}
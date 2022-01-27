using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MvvmSample.Model;
using Xamarin.Forms;

namespace MvvmSample.ViewModel
{
   public class PersonViewModel
   {
      public PersonViewModel()
      {
         People = new ObservableCollection<Person>();

         // sample data
         var person1 =
            new Person
            {
               FullName = "Alessandro",
               Address = "Italy",
               DateOfBirth = new DateTime(1977, 5, 10)
            };
         var person2 =
            new Person
            {
               FullName = "James",
               Address = "United States",
               DateOfBirth = new DateTime(1960, 2, 1)
            };
         var person3 =
            new Person
            {
               FullName = "Jacqueline",
               Address = "France",
               DateOfBirth = new DateTime(1980, 4, 2)
            };

         People.Add(person1);
         People.Add(person2);
         People.Add(person3);

         AddPerson =
            new Command(() => People.Add(new Person()));

         DeletePerson =
            new Command<Person>(person => People.Remove(person));
      }

      public ObservableCollection<Person> People { get; set; }
      public Person SelectedPerson { get; set; }

      public ICommand AddPerson { get; set; }
      public ICommand DeletePerson { get; set; }
   }
}
﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.BehaviorsPack;

namespace BehaviorsSampleApp.ViewModels
{
   public class SelectedItemAttachedPropertyPageViewModel
   {
      public SelectedItemAttachedPropertyPageViewModel()
      {
         Fruits.Add(new Fruit {Name = "Apple", Color = Color.Red});
         Fruits.Add(new Fruit {Name = "Orange", Color = Color.Orange});
         Fruits.Add(new Fruit {Name = "Pineapple", Color = Color.Yellow});
         Fruits.Add(new Fruit {Name = "Banana", Color = Color.Yellow});
         Fruits.Add(new Fruit {Name = "Peach", Color = Color.Pink});
         Fruits.Add(new Fruit {Name = "Mango", Color = Color.Yellow});
         Fruits.Add(new Fruit {Name = "Melon", Color = Color.Green});
         Fruits.Add(new Fruit {Name = "Grape", Color = Color.Purple});
         Fruits.Add(new Fruit {Name = "Strawberry", Color = Color.Red});
      }

      public IList<Fruit> Fruits { get; } = new List<Fruit>();

      public ICommand SelectedFruitCommand => new Command<Fruit>(fruit =>
      {
         NavigateNextPageRequest.Raise(new SelectedFruitEventArgs {SelectedFruit = fruit});
      });

      public ICommand AppearingCommand => new Command(() => Debug.WriteLine($"{GetType().Name}#AppearingCommand"));

      public ICommand DisappearingCommand =>
         new Command(() => Debug.WriteLine($"{GetType().Name}#DisappearingCommand"));

      public NotificationRequest NavigateNextPageRequest { get; } = new NotificationRequest();
   }
}
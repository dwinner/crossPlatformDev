﻿using System;
using Xamarin.Forms;

namespace Todo
{
   public partial class TodoListPage : ContentPage
   {
      public TodoListPage()
      {
         InitializeComponent();
      }

      protected override async void OnAppearing()
      {
         base.OnAppearing();

         listView.ItemsSource = await App.Database.GetItemsAsync();
      }

      private async void OnItemAdded(object sender, EventArgs e)
      {
         await Navigation.PushAsync(new TodoItemPage
         {
            BindingContext = new TodoItem()
         });
      }

      private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
      {
         if (e.SelectedItem != null)
         {
            await Navigation.PushAsync(new TodoItemPage
            {
               BindingContext = e.SelectedItem as TodoItem
            });
         }
      }
   }
}
using System;

namespace Todo
{
   public partial class TodoItemPage
   {
      public TodoItemPage()
      {
         InitializeComponent();
      }

      private async void OnSaveClicked(object sender, EventArgs e)
      {
         var todoItem = (TodoItem) BindingContext;
         await App.Database.SaveItemAsync(todoItem);
         await Navigation.PopAsync();
      }

      private async void OnDeleteClicked(object sender, EventArgs e)
      {
         var todoItem = (TodoItem) BindingContext;
         await App.Database.DeleteItemAsync(todoItem);
         await Navigation.PopAsync();
      }

      private async void OnCancelClicked(object sender, EventArgs e)
      {
         await Navigation.PopAsync();
      }
   }
}
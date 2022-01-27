using System.Windows.Input;

namespace Xamarin.Forms.BehaviorsPack
{
   public static partial class ListViews
   {
      #region ClearSelectedItem

      public static readonly BindableProperty ItemSelectedClearSelectedItemProperty =
         BindableProperty.CreateAttached("ItemSelectedClearSelectedItem", typeof(ICommand), typeof(ListViews), null,
            propertyChanged: OnItemSelectedClearSelectedItemChanged);

      public static ICommand GetClearSelectedItem(BindableObject bindableObject) =>
         (ICommand) bindableObject.GetValue(ItemSelectedClearSelectedItemProperty);

      private static void OnItemSelectedClearSelectedItemChanged(BindableObject bindable, object oldValue,
         object newValue)
      {
         if (bindable is ListView target)
         {
            if (oldValue == null && newValue != null)
            {
               target.ItemSelected += OnItemSelectedClearSelectedItem;
            }
            else if (oldValue != null && newValue == null)
            {
               target.ItemSelected -= OnItemSelectedClearSelectedItem;
            }
         }
      }

      private static void OnItemSelectedClearSelectedItem(object o, SelectedItemChangedEventArgs eventArgs)
      {
         if (o is ListView listView && eventArgs.SelectedItem != null)
         {
            var command = GetClearSelectedItem(listView);
            if (command.CanExecute(eventArgs.SelectedItem))
            {
               command.Execute(eventArgs.SelectedItem);
               listView.SelectedItem = null;
            }
         }
      }

      #endregion
   }
}
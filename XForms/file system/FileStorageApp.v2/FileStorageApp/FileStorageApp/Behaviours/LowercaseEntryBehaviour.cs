using Xamarin.Forms;

namespace FileStorageApp.Behaviours
{
   public class LowercaseEntryBehaviour : Behavior<Entry>
   {
      protected override void OnAttachedTo(Entry bindable)
      {
         bindable.TextChanged += OnEntryTextChanged;
         base.OnAttachedTo(bindable);
      }

      protected override void OnDetachingFrom(Entry bindable)
      {
         bindable.TextChanged -= OnEntryTextChanged;
         base.OnDetachingFrom(bindable);
      }

      private static void OnEntryTextChanged(object sender, TextChangedEventArgs e)
      {
         if (sender != null && sender is Entry textEntry)
         {
            textEntry.Text = e.NewTextValue.ToLower();
         }
      }
   }
}
using System;
using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MvvxSandboxApp.UI.Behaviors
{
   public class InfiniteScrollBehavior : Behavior<ListView>
   {
      public static readonly BindableProperty LoadMoreCommandProperty =
         BindableProperty.Create(nameof(LoadMoreCommand), typeof(ICommand), typeof(InfiniteScrollBehavior));

      private ListView _associatedObject;

      public ICommand LoadMoreCommand
      {
         get => (ICommand) GetValue(LoadMoreCommandProperty);
         set => SetValue(LoadMoreCommandProperty, value);
      }

      protected override void OnAttachedTo(ListView bindable)
      {
         base.OnAttachedTo(bindable);
         _associatedObject = bindable;
         bindable.BindingContextChanged += OnBindingContextChanged;
         bindable.ItemAppearing += OnItemAppearing;
      }

      protected override void OnDetachingFrom(ListView bindable)
      {
         base.OnDetachingFrom(bindable);
         bindable.BindingContextChanged -= OnBindingContextChanged;
         bindable.ItemAppearing -= OnItemAppearing;
      }

      protected override void OnBindingContextChanged()
      {
         base.OnBindingContextChanged();
         BindingContext = _associatedObject.BindingContext;
      }

      private void OnBindingContextChanged(object sender, EventArgs e) => OnBindingContextChanged();

      private void OnItemAppearing(object sender, ItemVisibilityEventArgs e)
      {
         if (_associatedObject.ItemsSource is IList items
             && e.Item == items[items.Count - 1]
             && LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
         {
            LoadMoreCommand.Execute(null);
         }
      }
   }
}
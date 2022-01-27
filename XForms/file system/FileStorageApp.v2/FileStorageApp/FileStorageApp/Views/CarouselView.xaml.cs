using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FileStorageApp.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class CarouselView
   {
      public static readonly BindableProperty SelectedCommandProperty = BindableProperty.Create(
         nameof(SelectedCommand),
         typeof(ICommand),
         typeof(CarouselView),
         default(ICommand),
         propertyChanged: (bindable, oldValue, newValue) => { });

      private bool _animating;

      public CarouselView()
      {
         InitializeComponent();

         gestureView.SwipeLeft += HandleSwipeLeft;
         gestureView.SwipeRight += HandleSwipeRight;
         gestureView.Touch += HandleTouch;
      }

      public int SelectedIndex { get; set; }

      public ICommand SelectedCommand
      {
         get => (ICommand) GetValue(SelectedCommandProperty);
         set => SetValue(SelectedCommandProperty, value);
      }

      private void HandleTouch(object sender, EventArgs e)
      {
         if (SelectedCommand != null && !_animating)
         {
            var selectedItem = carouselScroll.GetSelectedItem(SelectedIndex);
            SelectedCommand.Execute(selectedItem);
         }
      }

      private async void HandleSwipeRight(object sender, EventArgs e)
      {
         if (carouselScroll.ScrollX > 0 && !_animating)
         {
            _animating = true;

            SelectedIndex--;
            await carouselScroll.ScrollToAsync(carouselScroll.ScrollX - Width - 20, 0, true)
               .ConfigureAwait(true);

            _animating = false;
         }
      }

      private async void HandleSwipeLeft(object sender, EventArgs e)
      {
         if (carouselScroll.ScrollX + carouselScroll.Width < carouselScroll.Content.Width - carouselScroll.Width
             && !_animating)
         {
            _animating = true;
            SelectedIndex++;
            await carouselScroll.ScrollToAsync(carouselScroll.ScrollX + Width + 20, 0, true)
               .ConfigureAwait(true);
            _animating = false;
         }
      }
   }
}
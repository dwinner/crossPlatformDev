using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using NativeCustomDialogs.ViewModels;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace NativeCustomDialogs.UWP.Dialogs
{
   public sealed partial class CreateTodoDialog : ContentDialog
   {
      private bool _canClose;

      public CreateTodoDialog(CreateTodoViewModel vm)
      {
         InitializeComponent();
         DataContext = vm;
         Closing += CreateCategoryDialog_Closing;
      }

      private CreateTodoViewModel ViewModel => DataContext as CreateTodoViewModel;

      private void CreateCategoryDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
      {
         if (!_canClose)
         {
            args.Cancel = true;
         }
      }

      private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
         //Perform slight validation in case the Title was input
         if (string.IsNullOrEmpty(TitleDialog.Text))
         {
            TitleDialog.BorderBrush = new SolidColorBrush(Colors.Red);
         }
         else
         {
            if ((ViewModel.CreateTodo as ICommand).CanExecute(null))
            {
               (ViewModel.CreateTodo as ICommand).Execute(null);
            }

            _canClose = true;
            Hide();
         }
      }

      private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
      {
         _canClose = true;
         Hide();
      }
   }
}
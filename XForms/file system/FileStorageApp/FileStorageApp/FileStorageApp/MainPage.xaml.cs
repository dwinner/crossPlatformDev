using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
// ReSharper disable AvoidAsyncVoid
// ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting

namespace FileStorageApp
{
   // Learn more about making custom code visible in the Xamarin.Forms previewer
   // by visiting https://aka.ms/xamarinforms-previewer
   [DesignTimeVisible(false)]
   public partial class MainPage
   {
      public MainPage()
      {
         InitializeComponent();
      }

      protected override async void OnAppearing()
      {
         base.OnAppearing();
         await UpdateFileListAsync().ConfigureAwait(true);
      }

      private async void OnSave(object sender, EventArgs e)
      {
         var fileName = fileNameEntry.Text;
         if (string.IsNullOrEmpty(fileName))
         {
            return;
         }

         var fileWorkerImpl = DependencyService.Get<IFileWorker>();
         if (await fileWorkerImpl.ExistsAsync(fileName).ConfigureAwait(true))
         {
            var isRewrited = await DisplayAlert("Confirm", "File already exists, rewrite it?", "Yes", "No")
               .ConfigureAwait(true);
            if (!isRewrited)
            {
               return;
            }
         }

         await fileWorkerImpl.SaveTextAsync(fileNameEntry.Text, textEditor.Text).ConfigureAwait(true);
         await UpdateFileListAsync().ConfigureAwait(true);
      }

      private async void OnFileSelected(object sender, SelectedItemChangedEventArgs e)
      {
         var selectedItem = e.SelectedItem;
         if (selectedItem == null)
         {
            return;
         }

         var fileName = (string) selectedItem;
         textEditor.Text = await DependencyService.Get<IFileWorker>().LoadTextAsync(fileName).ConfigureAwait(true);
         fileNameEntry.Text = fileName;
         filesList.SelectedItem = null;
      }

      private async void OnFileDeleted(object sender, EventArgs e)
      {
         var fileName = (string) ((MenuItem) sender).BindingContext;
         await DependencyService.Get<IFileWorker>().DeleteAsync(fileName).ConfigureAwait(true);
         await UpdateFileListAsync().ConfigureAwait(true);
      }

      private async Task UpdateFileListAsync()
      {
         filesList.ItemsSource = await DependencyService.Get<IFileWorker>().GetFilesAsync().ConfigureAwait(true);
         filesList.SelectedItem = null;
      }
   }
}
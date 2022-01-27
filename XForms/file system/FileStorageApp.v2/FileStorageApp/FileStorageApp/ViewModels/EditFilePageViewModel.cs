using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FileStorageApp.DataAccess.Storable;
using FileStorageApp.DataAccess.Storage;
using FileStorageApp.Methods;
using FileStorageApp.UI;

namespace FileStorageApp.ViewModels
{
   /// <summary>
   ///    Edit file page view model
   /// </summary>
   public class EditFilePageViewModel : ViewModelBase
   {
      private string _content;
      private ICommand _deleteFileCommand;
      private string _fileName;

      private ICommand _saveFileCommand;

      /// <summary>
      ///    Initializes a new instance of the <see cref="EditFilePageViewModel" /> class
      /// </summary>
      /// <param name="navigation">Navigation.</param>
      /// <param name="commandFactory">Command factory.</param>
      /// <param name="methods">Methods</param>
      /// <param name="storage">Storage</param>
      public EditFilePageViewModel(INavigationService navigation, Func<Action, ICommand> commandFactory,
         IMethods methods, ISqLiteStorage storage)
         : base(navigation, methods)
      {
         _saveFileCommand = commandFactory(async () =>
         {
            await storage.InsertObjectAsync(new FileStorable
            {
               Key = FileName,
               Content = Content
            }).ConfigureAwait(true);

            NotifyAlert("File saved.");
         });

         _deleteFileCommand = commandFactory(async () =>
         {
            await storage.DeleteObjectByKeyAsync<FileStorable>(FileName).ConfigureAwait(true);
            await Navigation.PopAsync().ConfigureAwait(true);
         });
      }

      public ICommand SaveFileCommand
      {
         get => _saveFileCommand;
         set => SetProperty(ref _saveFileCommand, value);
      }

      public ICommand DeleteFileCommand
      {
         get => _deleteFileCommand;
         set => SetProperty(ref _deleteFileCommand, value);
      }

      public string Content
      {
         get => _content;
         set => SetProperty(ref _content, value);
      }

      public string FileName
      {
         get => _fileName;
         set => SetProperty(ref _fileName, value);
      }

      public void OnAppear()
      {
      }

      public void OnDisppear()
      {
         FileName = string.Empty;
         Content = string.Empty;
      }

      /// <summary>
      ///    Loads the async.
      /// </summary>
      /// <returns>The async.</returns>
      /// <param name="parameters">Parameters.</param>
      protected override Task LoadAsync(IDictionary<string, object> parameters)
      {
         if (parameters.ContainsKey("filename"))
         {
            FileName = (parameters["filename"] as string).ToLower();
         }

         if (parameters.ContainsKey("contents"))
         {
            Content = parameters["contents"] as string;
         }

         return Task.FromResult(true);
      }
   }
}
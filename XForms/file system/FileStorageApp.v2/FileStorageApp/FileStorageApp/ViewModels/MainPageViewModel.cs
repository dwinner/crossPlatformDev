using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using FileStorageApp.DataAccess.Storable;
using FileStorageApp.DataAccess.Storage;
using FileStorageApp.Enums;
using FileStorageApp.Methods;
using FileStorageApp.UI;

namespace FileStorageApp.ViewModels
{
   /// <summary>
   ///    Main page view model
   /// </summary>
   public class MainPageViewModel : ViewModelBase
   {
      private readonly ISqLiteStorage _storage; // The storage
      private string _descriptionMessage = "Welcome to the Filing Room"; // The description message
      private ICommand _exitCommand; // The exit command
      private string _exitTitle = "Exit"; // The exit title
      private string _filesTitle = "Files"; // The location title
      private ICommand _locationCommand; // The location command

      /// <summary>
      ///    Initializes a new instance of the <see cref="MainPageViewModel" />
      /// </summary>
      /// <param name="navigation">Navigation</param>
      /// <param name="commandFactory">Command factory</param>
      /// <param name="methods">Methods</param>
      /// <param name="storage">Db-storage</param>
      public MainPageViewModel(INavigationService navigation, Func<Action, ICommand> commandFactory, IMethods methods,
         ISqLiteStorage storage)
         : base(navigation, methods)
      {
         _exitCommand = commandFactory(methods.Exit);
         _locationCommand = commandFactory(() => Navigation.NavigateAsync(PageNames.FilesPage, null));
         _storage = storage;
         SetupSqLiteAsync().ConfigureAwait(false);
      }

      public string DescriptionMessage
      {
         get => _descriptionMessage;
         set => SetProperty(ref _descriptionMessage, value);
      }

      public string FilesTitle
      {
         get => _filesTitle;
         set => SetProperty(ref _filesTitle, value);
      }

      public string ExitTitle
      {
         get => _exitTitle;
         set => SetProperty(ref _exitTitle, value);
      }

      public ICommand LocationCommand
      {
         get => _locationCommand;
         set => SetProperty(ref _locationCommand, value);
      }

      public ICommand ExitCommand
      {
         get => _exitCommand;
         set => SetProperty(ref _exitCommand, value);
      }

      /// <summary>
      ///    Setups the SQLite
      /// </summary>
      /// <returns>The SQLite</returns>
      private Task SetupSqLiteAsync()
      {
         _storage.CreateSqLiteAsyncConnection(); // create Sqlite connection
         return _storage.CreateTableAsync<FileStorable>(); // create DB tables
      }

      protected override Task LoadAsync(IDictionary<string, object> parameters) => Task.FromResult(true);
   }
}
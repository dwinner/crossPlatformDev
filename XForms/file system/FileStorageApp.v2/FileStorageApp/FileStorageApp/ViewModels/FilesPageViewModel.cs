using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
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
   ///    Files page view model
   /// </summary>
   public class FilesPageViewModel : ViewModelBase
   {
      private readonly SynchronizationContext _context; // The context
      private readonly Func<FileItemViewModel> _fileFactory; // The file factory
      private readonly ISqLiteStorage _storage; // The storage
      private ICommand _createFileCommand; // The create file command
      private ICommand _editFileCommand; // The create command
      private bool _noFiles; // The no files

      /// <summary>
      ///    Initializes a new instance of the <see cref="FilesPageViewModel" /> class.
      /// </summary>
      /// <param name="navigation">Navigation.</param>
      /// <param name="commandFactory">Command factory.</param>
      /// <param name="methods">Methods</param>
      /// <param name="storage">Storage</param>
      /// <param name="fileFactory">File factory</param>
      public FilesPageViewModel(INavigationService navigation, Func<Action<object>, ICommand> commandFactory,
         IMethods methods, ISqLiteStorage storage, Func<FileItemViewModel> fileFactory)
         : base(navigation, methods)
      {
         DataChanges = new Subject<DataChange>();
         _context = SynchronizationContext.Current; // retrieve main thread context
         _storage = storage;
         _fileFactory = fileFactory;
         Cells = new ObservableCollection<FileItemViewModel>();
         _editFileCommand = commandFactory(file =>
         {
            Navigation.NavigateAsync(PageNames.EditFilePage, new Dictionary<string, object>
            {
               {"filename", (file as FileItemViewModel).FileName},
               {"contents", (file as FileItemViewModel).Content}
            });
         });

         _createFileCommand = commandFactory(async obj =>
         {
            var fileName = await ShowEntryAlertAsync("Enter file name:").ConfigureAwait(true);
            if (!string.IsNullOrEmpty(fileName))
            {
               await Navigation.NavigateAsync(PageNames.EditFilePage, new Dictionary<string, object>
               {
                  {"filename", fileName}
               }).ConfigureAwait(true);
            }
         });
      }

      /// <summary>
      ///    Gets the data changes
      /// </summary>
      /// <value>The data changes</value>
      public Subject<DataChange> DataChanges { get; }

      public ICommand EditFileCommand
      {
         get => _editFileCommand;
         set => SetProperty(ref _editFileCommand, value);
      }

      public ICommand CreateFileCommand
      {
         get => _createFileCommand;
         set => SetProperty(ref _createFileCommand, value);
      }

      public bool AnyFiles
      {
         get => _noFiles;
         set => SetProperty(ref _noFiles, value);
      }

      /// <summary>
      ///    Gets or sets the files
      /// </summary>
      /// <value>The files</value>
      public ObservableCollection<FileItemViewModel> Cells { get; set; }

      /// <summary>
      ///    Updates the files.
      /// </summary>
      /// <returns>The files.</returns>
      private void UpdateFiles()
      {
         _context.Post(async obj =>
         {
            Cells.Clear();
            var files = await _storage.GetTableAsync<FileStorable>().ConfigureAwait(true);

            foreach (var file in files)
            {
               var fileModel = _fileFactory();
               fileModel.Apply(file);
               Cells.Add(fileModel);
            }

            AnyFiles = Cells.Any();
            DataChanges.OnNext(new DataChange
            {
               SizeChanged = true
            });
         }, null);
      }

      /// <summary>
      ///    On the appear
      /// </summary>
      /// <returns>The appear</returns>
      public void OnAppear() => UpdateFiles();

      /// <summary>
      ///    On the disappear.
      /// </summary>
      public void OnDisappear()
      {
      }

      /// <summary>
      ///    Loads the async
      /// </summary>
      /// <returns>The async</returns>
      /// <param name="parameters">Parameters</param>
      protected override Task LoadAsync(IDictionary<string, object> parameters) => Task.FromResult(true);
   }
}
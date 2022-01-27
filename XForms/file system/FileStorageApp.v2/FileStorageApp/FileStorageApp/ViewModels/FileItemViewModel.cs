using System.Collections.Generic;
using System.Threading.Tasks;
using FileStorageApp.DataAccess.Storable;
using FileStorageApp.Methods;
using FileStorageApp.UI;

namespace FileStorageApp.ViewModels
{
   /// <summary>
   ///    File item view model
   /// </summary>
   public class FileItemViewModel : ViewModelBase
   {
      private string _content; // The file content
      private string _fileName; // The file name

      /// <summary>
      ///    Initializes a new instance of the <see cref="FileItemViewModel" /> class
      /// </summary>
      /// <param name="navigation">Navigation.</param>
      /// <param name="methods">Methods</param>
      public FileItemViewModel(INavigationService navigation, IMethods methods)
         : base(navigation, methods)
      {
      }

      public string FileName
      {
         get => _fileName;
         set => SetProperty(ref _fileName, value);
      }

      public string Content
      {
         get => _content;
         set => SetProperty(ref _content, value);
      }

      /// <summary>
      ///    Apply the specified file.
      /// </summary>
      /// <param name="file">File.</param>
      public void Apply(FileStorable file)
      {
         FileName = file.Key ?? string.Empty;
         Content = file.Content ?? string.Empty;
      }

      protected override Task LoadAsync(IDictionary<string, object> parameters) => Task.FromResult(true);
   }
}
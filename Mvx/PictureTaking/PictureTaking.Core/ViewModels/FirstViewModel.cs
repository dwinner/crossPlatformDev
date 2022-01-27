using System.IO;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.PictureChooser;

namespace PictureTaking.Core.ViewModels
{
   public class FirstViewModel
      : MvxViewModel
   {
      private readonly IMvxPictureChooserTask _pictureChooserTask;

      private byte[] _bytes;

      private MvxCommand _choosePictureCommand;

      private MvxCommand _takePictureCommand;

      public FirstViewModel(IMvxPictureChooserTask pictureChooserTask) => _pictureChooserTask = pictureChooserTask;

      public ICommand TakePictureCommand
      {
         get
         {
            _takePictureCommand = _takePictureCommand ?? new MvxCommand(DoTakePicture);
            return _takePictureCommand;
         }
      }

      public ICommand ChoosePictureCommand
      {
         get
         {
            _choosePictureCommand = _choosePictureCommand ?? new MvxCommand(DoChoosePicture);
            return _choosePictureCommand;
         }
      }

      public byte[] Bytes
      {
         get => _bytes;
         set
         {
            _bytes = value;
            RaisePropertyChanged(() => Bytes);
         }
      }

      private void DoTakePicture()
      {
         _pictureChooserTask.TakePicture(400, 95, OnPicture, () => { });
      }

      private void DoChoosePicture()
      {
         _pictureChooserTask.ChoosePictureFromLibrary(400, 95, OnPicture, () => { });
      }

      private void OnPicture(Stream pictureStream)
      {
         var memoryStream = new MemoryStream();
         pictureStream.CopyTo(memoryStream);
         Bytes = memoryStream.ToArray();
      }
   }
}
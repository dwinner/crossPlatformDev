using System;
using System.Threading.Tasks;
using NativeCustomDialogs.UWP;
using NativeCustomDialogs.UWP.Dialogs;
using NativeCustomDialogs.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(
   typeof(CallDialog))]

namespace NativeCustomDialogs.UWP
{
   public class CallDialog : ICallDialog
   {
      async Task ICallDialog.CallDialog(object viewModel)
      {
         await new CreateTodoDialog(viewModel as CreateTodoViewModel).ShowAsync();
      }
   }
}
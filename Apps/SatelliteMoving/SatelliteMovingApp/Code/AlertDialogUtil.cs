using System;
using Android.App;
using Android.Content;
using AdrResource = Android.Resource;

namespace SatelliteMovingApp.Code
{
   /// <summary>
   ///    Вспомогательный класс для генерации простых диалоговых окон
   /// </summary>
   public static class AlertDialogUtil
   {
      /// <summary>
      ///    Вывод простого диалогового окна с сообщением
      /// </summary>
      /// <param name="context">Активность-собственник</param>
      /// <param name="message">Сообщение</param>
      /// <param name="onClick">On click action</param>
      public static void ShowSimpleDialog(Context context, string message,
         EventHandler<DialogClickEventArgs> onClick = null)
         => new AlertDialog.Builder(context)
            .SetMessage(message)
            .SetPositiveButton(context.Resources.GetString(AdrResource.String.Ok), onClick)
            .Create()
            .Show();
   }
}
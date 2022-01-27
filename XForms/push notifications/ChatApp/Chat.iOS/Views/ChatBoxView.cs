using System.Linq;
using Chat.iOS.Extras;
using UIKit;

namespace Chat.iOS.Views
{
   /// <summary>
   ///    Chat box view.
   /// </summary>
   public sealed class ChatBoxView : UIView
   {
      /// <summary>
      ///    Initializes a new instance of the <see cref="ChatBoxView" /> class.
      /// </summary>
      /// <param name="message">Message.</param>
      public ChatBoxView(string message)
      {
         Layer.CornerRadius = 10;

         var messageLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            Text = message
         };

         Add(messageLabel);

         var views = new DictionaryViews { {"messageLabel", messageLabel} };

         AddConstraints(NSLayoutConstraint
            .FromVisualFormat("V:|[messageLabel]|", NSLayoutFormatOptions.AlignAllTop, null, views)
            .Concat(NSLayoutConstraint.FromVisualFormat("H:|-5-[messageLabel]-5-|", NSLayoutFormatOptions.AlignAllTop,
               null, views))
            .ToArray());
      }
   }
}
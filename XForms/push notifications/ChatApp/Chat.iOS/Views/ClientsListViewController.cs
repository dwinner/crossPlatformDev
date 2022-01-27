using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chat.Common.Events;
using Chat.Common.Model;
using Chat.Common.Presenter;
using Chat.iOS.Extras;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Chat.iOS.Views
{
   /// <summary>
   ///    Clients list view controller.
   /// </summary>
   public class ClientsListViewController : UIViewController, ClientsListPresenter.IClientsListView
   {
      /// <summary>
      ///    The presenter.
      /// </summary>
      private readonly ClientsListPresenter _presenter;

      /// <summary>
      ///    The source.
      /// </summary>
      private readonly ClientsTableSource _source;

      /// <summary>
      ///    The table view.
      /// </summary>
      private UITableView _tableView;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ClientsListViewController" /> class.
      /// </summary>
      /// <param name="presenter">Presenter.</param>
      public ClientsListViewController(ClientsListPresenter presenter)
      {
         _presenter = presenter;
         _source = new ClientsTableSource();
         _source.ItemSelected += (sender, e) => ClientSelected?.Invoke(this, new ClientSelectedEventArgs(e));
      }

      /// <summary>
      ///    Occurs when client selected.
      /// </summary>
      public event EventHandler<ClientSelectedEventArgs> ClientSelected;

      /// <summary>
      ///    Notifies the connected clients updated.
      /// </summary>
      /// <returns>The connected clients updated.</returns>
      /// <param name="clients">Clients.</param>
      public void NotifyConnectedClientsUpdated(IEnumerable<Client> clients) =>
         InvokeOnMainThread(() =>
         {
            try
            {
               IsInProgress = true;
               _source.UpdateClients(clients);
               _tableView.ReloadData();
            }
            finally
            {
               IsInProgress = false;
            }
         });

      /// <summary>
      ///    Sets the error message.
      /// </summary>
      /// <returns>The error message.</returns>
      /// <param name="message">Message.</param>
      public void SetErrorMessage(string message)
      {
         var alert = new UIAlertView
         {
            Title = "Chat",
            Message = message
         };
         alert.AddButton("OK");
         alert.Show();
      }

      /// <summary>
      ///    Gets or sets the is in progress.
      /// </summary>
      /// <value>The is in progress.</value>
      public bool IsInProgress { get; set; }

      /// <summary>
      ///    Views the did load.
      /// </summary>
      /// <returns>The did load.</returns>
      public override void ViewDidLoad()
      {
         base.ViewDidLoad();

         // Perform any additional setup after loading the view, typically from a nib.
         var backButton = new UIBarButtonItem("< Back", UIBarButtonItemStyle.Bordered, HandleSignout);
         NavigationItem.SetLeftBarButtonItem(backButton, false);

         View.BackgroundColor = UIColor.White;

         _presenter.SetView(this);

         var width = View.Bounds.Width;
         var height = View.Bounds.Height;

         Title = "Clients";

         var titleLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            Text = "Connected Clients",
            Font = UIFont.FromName("Helvetica-Bold", 22),
            TextAlignment = UITextAlignment.Center
         };

         var descriptionLabel = new UILabel
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            Text = "Select a client you would like to chat with",
            Font = UIFont.FromName("Helvetica", 18),
            TextAlignment = UITextAlignment.Center
         };

         _tableView = new UITableView(new CGRect(0, 0, width, height))
         {
            TranslatesAutoresizingMaskIntoConstraints = false,
            AutoresizingMask = UIViewAutoresizing.All,
            Source = _source
         };

         Add(titleLabel);
         Add(descriptionLabel);
         Add(_tableView);

         var views = new DictionaryViews
         {
            {"titleLabel", titleLabel},
            {"descriptionLabel", descriptionLabel},
            {"tableView", _tableView}
         };

         View.AddConstraints(
            NSLayoutConstraint.FromVisualFormat("V:|-100-[titleLabel(30)]-[descriptionLabel(30)]-[tableView]|",
                  NSLayoutFormatOptions.DirectionLeftToRight, null, views)
               .Concat(NSLayoutConstraint.FromVisualFormat("H:|[tableView]|", NSLayoutFormatOptions.AlignAllTop, null,
                  views))
               .Concat(NSLayoutConstraint.FromVisualFormat("H:|-10-[titleLabel]-10-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .Concat(NSLayoutConstraint.FromVisualFormat("H:|-10-[descriptionLabel]-10-|",
                  NSLayoutFormatOptions.AlignAllTop, null, views))
               .ToArray());
      }

      /// <summary>
      ///    Called when view is disposed.
      /// </summary>
      /// <returns>The did unload.</returns>
      public override void ViewDidUnload()
      {
         base.ViewDidUnload();

         _presenter.ReleaseView();
      }

      /// <summary>
      ///    Handles the signout.
      /// </summary>
      /// <returns>The signout.</returns>
      /// <param name="sender">Sender.</param>
      /// <param name="e">E.</param>
      private async void HandleSignout(object sender, EventArgs e)
      {
         var accepted = await ShowAlertAsync("Chat", "Would you like to signout?").ConfigureAwait(true);

         if (accepted)
         {
            _presenter.Signout();
         }
      }

      /// <summary>
      ///    Shows the alert.
      /// </summary>
      /// <returns>The alert.</returns>
      /// <param name="title">Title.</param>
      /// <param name="message">Message.</param>
      private static Task<bool> ShowAlertAsync(string title, string message)
      {
         var tcs = new TaskCompletionSource<bool>();

         UIApplication.SharedApplication.InvokeOnMainThread(() =>
         {
            var cancelMsg = NSBundle.MainBundle.GetLocalizedString("Cancel", "Cancel");
            var okMsg = NSBundle.MainBundle.GetLocalizedString("OK", "OK");


            IUIAlertViewDelegate del = new UIAlertViewDelegate();
            var alert = new UIAlertView(title, message, del, cancelMsg, okMsg);
            alert.Clicked += (sender, buttonArgs) => tcs.SetResult(buttonArgs.ButtonIndex != alert.CancelButtonIndex);
            alert.Show();
         });

         return tcs.Task;
      }
   }
}
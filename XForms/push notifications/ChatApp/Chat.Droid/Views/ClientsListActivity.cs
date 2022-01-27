using System;
using System.Collections.Generic;
using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views;
using Android.Widget;
using Chat.Common.Events;
using Chat.Common.Model;
using Chat.Common.Presenter;

namespace Chat.Droid.Views
{
   /// <summary>
   ///    Clients list activity.
   /// </summary>
   [Activity(
      Label = "Chat Room",
      Icon = "@drawable/icon",
      ScreenOrientation = ScreenOrientation.Portrait)]
   public class ClientsListActivity : ListActivity, ClientsListPresenter.IClientsListView
   {
      /// <summary>
      ///    The presenter.
      /// </summary>
      private ClientsListPresenter _presenter;

      /// <summary>
      ///    The adapter.
      /// </summary>
      private ClientsListAdapter _adapter;

      /// <summary>
      ///    The dialog shown.
      /// </summary>
      private bool _dialogShown;

      /// <summary>
      ///    Occurs when client selected.
      /// </summary>
      public event EventHandler<ClientSelectedEventArgs> ClientSelected;

      /// <summary>
      ///    Notifies the connected clients updated.
      /// </summary>
      /// <returns>The connected clients updated.</returns>
      /// <param name="clients">Clients.</param>
      public void NotifyConnectedClientsUpdated(IEnumerable<Client> clients)
      {
         if (_adapter != null)
         {
            _adapter.UpdateClients(clients);

            // perform action on UI thread
            Application.SynchronizationContext.Post(state => { _adapter.NotifyDataSetChanged(); }, null);
         }
      }

      /// <summary>
      ///    Sets the error message.
      /// </summary>
      /// <returns>The error message.</returns>
      /// <param name="message">Message.</param>
      public void SetErrorMessage(string message)
      {
         if (!_dialogShown)
         {
            _dialogShown = true;

            var builder = new AlertDialog.Builder(this);
            builder
               .SetTitle("Chat")
               .SetMessage(message)
               .SetNeutralButton("Ok", (sender, e) => { _dialogShown = false; })
               .Show();
         }
      }

      /// <summary>
      ///    Gets or sets the is in progress.
      /// </summary>
      /// <value>The is in progress.</value>
      public bool IsInProgress { get; set; }

      /// <summary>
      ///    Ons the create.
      /// </summary>
      /// <returns>The create.</returns>
      /// <param name="bundle">Bundle.</param>
      protected override void OnCreate(Bundle bundle)
      {
         base.OnCreate(bundle);

         ListView.SetBackgroundColor(Color.White);

         var app = ChatApplication.GetApplication(this);
         app.CurrentActivity = this;

         _presenter = app.Presenter as ClientsListPresenter;
         _presenter.SetView(this);

         _adapter = new ClientsListAdapter(this);
         ListAdapter = _adapter;
      }

      /// <summary>
      ///    Ons the back pressed.
      /// </summary>
      /// <returns>The back pressed.</returns>
      public override void OnBackPressed()
      {
         // Put up the Yes/No message box
         var builder = new AlertDialog.Builder(this);
         builder
            .SetTitle("Chat")
            .SetMessage("Would you like to signout?")
            .SetNegativeButton("No", (sender, e) => { })
            .SetPositiveButton("Yes", (sender, e) => { _presenter.Signout(); })
            .Show();
      }

      /// <summary>
      ///    Ons the resume.
      /// </summary>
      /// <returns>The resume.</returns>
      protected override void OnResume()
      {
         base.OnResume();

         var app = ChatApplication.GetApplication(this);
         app.CurrentActivity = this;
         _presenter?.SetView(this);
      }

      /// <summary>
      ///    Ons the pause.
      /// </summary>
      /// <returns>The pause.</returns>
      protected override void OnPause()
      {
         base.OnPause();
         _presenter?.ReleaseView();
      }

      /// <summary>
      ///    Ons the list item click.
      /// </summary>
      /// <returns>The list item click.</returns>
      /// <param name="listView">L.</param>
      /// <param name="view">V.</param>
      /// <param name="position">Position.</param>
      /// <param name="id">Identifier.</param>
      protected override void OnListItemClick(ListView listView, View view, int position, long id)
      {
         var item = _adapter[position];
         ClientSelected?.Invoke(this, new ClientSelectedEventArgs(item));
      }
   }
}
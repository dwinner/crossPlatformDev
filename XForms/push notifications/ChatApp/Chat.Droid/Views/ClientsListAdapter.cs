using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using Chat.Common.Model;
using Java.Lang;

namespace Chat.Droid.Views
{
   /// <summary>
   ///    Clients list adapter.
   /// </summary>
   public class ClientsListAdapter : BaseAdapter<Client>
   {
      /// <summary>
      ///    The clients.
      /// </summary>
      private readonly List<Client> _clients;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ClientsListAdapter" /> class.
      /// </summary>
      private readonly Activity _context;

      /// <summary>
      ///    Initializes a new instance of the <see cref="ClientsListAdapter" /> class.
      /// </summary>
      /// <param name="context">Context.</param>
      public ClientsListAdapter(Activity context)
      {
         _context = context;
         _clients = new List<Client>();
      }

      /// <summary>
      ///    Gets the <see cref="ClientsListAdapter" /> with the specified position.
      /// </summary>
      /// <param name="position">Position.</param>
      public override Client this[int position] => _clients[position];

      /// <summary>
      ///    Gets the count.
      /// </summary>
      /// <value>The count.</value>
      public override int Count => _clients.Count;

      /// <summary>
      ///    Gets the item.
      /// </summary>
      /// <returns>The item.</returns>
      /// <param name="position">Position.</param>
      public override Object GetItem(int position) => null;

      /// <summary>
      ///    Gets the item identifier.
      /// </summary>
      /// <returns>The item identifier.</returns>
      /// <param name="position">Position.</param>
      public override long GetItemId(int position) => position;

      /// <summary>
      ///    Updates the clients.
      /// </summary>
      /// <returns>The clients.</returns>
      /// <param name="clients">Clients.</param>
      public void UpdateClients(IEnumerable<Client> clients)
      {
         _clients.Clear();

         foreach (var client in clients)
         {
            _clients.Add(client);
         }
      }

      /// <summary>
      ///    Gets the view.
      /// </summary>
      /// <returns>The view.</returns>
      /// <param name="position">Position.</param>
      /// <param name="convertView">Convert view.</param>
      /// <param name="parent">Parent.</param>
      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         // re-use an existing view, if one is available otherwise create a new one
         var view = convertView ?? _context.LayoutInflater.Inflate(Resource.Layout.CustomCell, null);

         // set labels
         var connectionIdTextView = view.FindViewById<TextView>(Resource.Id.username);
         connectionIdTextView.Text = _clients[position].Username;

         return view;
      }
   }
}
using System;
using System.Collections.Generic;
using Chat.Common.Model;
using Foundation;
using UIKit;

namespace Chat.iOS
{
   /// <summary>
   ///    Clients table source.
   /// </summary>
   public class ClientsTableSource : UITableViewSource
   {
      /// <summary>
      ///    The clients.
      /// </summary>
      private readonly List<Client> _clients;

      /// <summary>
      ///    The cell identifier.
      /// </summary>
      private const string CellIdentifier = "ClientCell";

      /// <summary>
      ///    Initializes a new instance of the <see cref="ClientsTableSource" /> class.
      /// </summary>
      public ClientsTableSource() => _clients = new List<Client>();

      /// <summary>
      ///    Occurs when item selected.
      /// </summary>
      public event EventHandler<Client> ItemSelected;

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
      ///    Numbers the of sections.
      /// </summary>
      /// <returns>The of sections.</returns>
      /// <param name="tableView">Table view.</param>
      public override nint NumberOfSections(UITableView tableView) => 1;

      /// <summary>
      ///    Rowses the in section.
      /// </summary>
      /// <returns>The in section.</returns>
      /// <param name="tableview">Tableview.</param>
      /// <param name="section">Section.</param>
      public override nint RowsInSection(UITableView tableview, nint section) => _clients.Count;

      /// <summary>
      ///    Rows the selected.
      /// </summary>
      /// <returns>The selected.</returns>
      /// <param name="tableView">Table view.</param>
      /// <param name="indexPath">Index path.</param>
      public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
      {
         ItemSelected?.Invoke(this, _clients[indexPath.Row]);
         tableView.DeselectRow(indexPath, true);
      }

      /// <summary>
      ///    Gets the height for row.
      /// </summary>
      /// <returns>The height for row.</returns>
      /// <param name="tableView">Table view.</param>
      /// <param name="indexPath">Index path.</param>
      public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath) => 80;

      /// <summary>
      ///    Gets the cell.
      /// </summary>
      /// <returns>The cell.</returns>
      /// <param name="tableView">Table view.</param>
      /// <param name="indexPath">Index path.</param>
      public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
      {
         var cell = tableView.DequeueReusableCell(CellIdentifier);

         if (indexPath.Row < _clients.Count)
         {
            var client = _clients[indexPath.Row];

            if (cell == null)
            {
               cell = new UITableViewCell(UITableViewCellStyle.Default, CellIdentifier);
            }

            cell.TextLabel.Text = client.Username;
         }

         return cell;
      }
   }
}
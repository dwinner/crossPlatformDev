using System;
using System.Collections.Generic;
using System.Windows.Input;
using Foundation;
using MvvmCross.Binding.Extensions;
using MvvmCross.Exceptions;
using MvvmCross.Platforms.Ios.Binding.Views;
using StarWarsSample.Core.Models;
using StarWarsSample.iOS.Views.Cells;
using UIKit;

namespace StarWarsSample.iOS.Sources
{
   public class PlanetsTableViewSource : MvxTableViewSource
   {
      private readonly Dictionary<Type, Type> _itemsTypeDictionary = new Dictionary<Type, Type>
      {
         [typeof(Planet)] = typeof(NameTableViewCell),
         [typeof(Planet2)] = typeof(WhiteNameTableViewCell)
      };

      public PlanetsTableViewSource(UITableView tableView) : base(tableView)
      {
         RegisterCellsForReuse();
         DeselectAutomatically = true;
      }

      public ICommand FetchCommand { get; set; }

      protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
      {
         if (!_itemsTypeDictionary.TryGetValue(item.GetType(), out var cellType))
         {
            throw new MvxException(
               $"Type {item.GetType().Name} is not registered as cell. Please override RegisterCellsForReuse");
         }

         var cell = TableView.DequeueReusableCell(cellType.Name, indexPath) as BaseTableViewCell;

         if (indexPath.Item == ItemsSource.Count() - 5)
         {
            FetchCommand?.Execute(null);
         }

         return cell;
      }

      private void RegisterCellsForReuse()
      {
         foreach (var type in _itemsTypeDictionary.Values)
         {
            TableView.RegisterClassForCellReuse(type, type.Name);
         }
      }
   }
}
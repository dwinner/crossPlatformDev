﻿using TabbedAppSample.Models;

namespace TabbedAppSample.ViewModels
{
   public class ItemDetailViewModel : BaseViewModel
   {
      public ItemDetailViewModel(Item item = null)
      {
         Title = item?.Text;
         Item = item;
      }

      public Item Item { get; set; }
   }
}
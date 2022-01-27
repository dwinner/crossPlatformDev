﻿using System;
using System.Collections.Generic;
using MvvmCross.DroidX.RecyclerView.ItemTemplates;
using StarWarsSample.Core.Models;

namespace StarWarsSample.Droid.TemplateSelectors
{
   public class PlanetsTemplateSelector : IMvxTemplateSelector
   {
      private readonly Dictionary<Type, int> _itemsTypeDictionary = new Dictionary<Type, int>
      {
         [typeof(Planet)] = Resource.Layout.item_name,
         [typeof(Planet2)] = Resource.Layout.item_name_white
      };

      public int ItemTemplateId { get; set; }

      public int GetItemLayoutId(int fromViewType) => fromViewType;

      public int GetItemViewType(object forItemObject) => _itemsTypeDictionary[forItemObject.GetType()];
   }
}
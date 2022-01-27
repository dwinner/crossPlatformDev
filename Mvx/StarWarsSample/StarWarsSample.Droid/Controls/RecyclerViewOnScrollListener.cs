﻿using System;
using AndroidX.RecyclerView.Widget;

namespace StarWarsSample.Droid.Controls
{
   public class RecyclerViewOnScrollListener : RecyclerView.OnScrollListener
   {
      public delegate void LoadMoreEventHandler(object sender, EventArgs e);

      private readonly LinearLayoutManager _layoutManager;

      public RecyclerViewOnScrollListener(LinearLayoutManager layoutManager) => _layoutManager = layoutManager;

      public int RemainingItemsToTriggerFetch { get; set; } = 8;

      public event LoadMoreEventHandler LoadMoreEvent;

      public override void OnScrolled(RecyclerView recyclerView, int dx, int dy)
      {
         base.OnScrolled(recyclerView, dx, dy);

         var visibleItemCount = recyclerView.ChildCount;
         var totalItemCount = recyclerView.GetAdapter().ItemCount;
         var pastVisiblesItems = _layoutManager.FindFirstVisibleItemPosition();

         if (totalItemCount != 0
             //&& pastVisiblesItems > 0
             &&
             (
                RemainingItemsToTriggerFetch >= totalItemCount
                || visibleItemCount + pastVisiblesItems + RemainingItemsToTriggerFetch >= totalItemCount
             ))
         {
            LoadMoreEvent?.Invoke(this, null);
         }
      }
   }
}
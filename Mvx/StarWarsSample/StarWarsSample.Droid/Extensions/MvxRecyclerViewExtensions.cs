﻿using System;
using System.Windows.Input;
using AndroidX.RecyclerView.Widget;
using MvvmCross.DroidX.RecyclerView;
using MvvmCross.ViewModels;
using StarWarsSample.Droid.Controls;

namespace StarWarsSample.Droid.Extensions
{
   public static class MvxRecyclerViewExtensions
   {
      public static void AddOnScrollFetchItemsListener(this MvxRecyclerView recyclerView,
         LinearLayoutManager linearLayoutManager, Func<MvxNotifyTask> fetchItemsTaskCompletionFunc,
         Func<ICommand> fetchItemsCommandFunc)
      {
         var onScrollListener = new RecyclerViewOnScrollListener(linearLayoutManager);
         onScrollListener.LoadMoreEvent += (sender, e) =>
         {
            var fetchItemsTaskCompletion = fetchItemsTaskCompletionFunc.Invoke();
            if (fetchItemsTaskCompletion == null || !fetchItemsTaskCompletion.IsNotCompleted)
            {
               fetchItemsCommandFunc.Invoke().Execute(null);
            }
         };

         recyclerView.AddOnScrollListener(onScrollListener);
      }
   }
}
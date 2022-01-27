﻿using System;
using System.ComponentModel;
using AndroidX.SwipeRefreshLayout.Widget;
using MvvmCross.Binding;
using MvvmCross.Logging;
using MvvmCross.Platforms.Android.Binding.Target;
using MvvmCross.ViewModels;
using MvvmCross.WeakSubscription;
using StarWarsSample.Core;

namespace StarWarsSample.Droid.MvxBindings
{
   public class SwipeRefreshLayoutIsRefreshingTargetBinding : MvxAndroidTargetBinding
   {
      public SwipeRefreshLayoutIsRefreshingTargetBinding(SwipeRefreshLayout swipeRefreshLayout)
         : base(swipeRefreshLayout)
      {
      }

      protected SwipeRefreshLayout SwipeRefreshLayout => (SwipeRefreshLayout) Target;

      public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

      public override Type TargetType => typeof(MvxNotifyTask);

      protected override void SetValueImpl(object target, object value)
      {
         if (!(value is MvxNotifyTask taskCompletion))
         {
            Logs.Instance.Trace("Value '{0}' could not be parsed as a valid INotifyTaskCompletion", value);
            return;
         }

         taskCompletion.WeakSubscribe(HandlePropertyChanged);

         SwipeRefreshLayout.Post(() => SwipeRefreshLayout.Refreshing = taskCompletion.IsNotCompleted);
      }

      private void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
      {
         if (e.PropertyName == nameof(MvxNotifyTask.IsNotCompleted))
         {
            SwipeRefreshLayout.Post(() => SwipeRefreshLayout.Refreshing = ((MvxNotifyTask) sender).IsNotCompleted);
         }
      }
   }
}
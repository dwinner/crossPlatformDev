using System;
using System.Collections.Generic;
using System.Linq;
using MvvmCross.Binding;
using MvvmCross.Binding.Bindings.Target;
using UIKit;

namespace StarWarsSample.iOS.MvxBindings
{
   public class NetworkIndicatorTargetBinding : MvxTargetBinding
   {
      private static List<NetworkIndicatorTargetBinding> _currentBindings;

      public NetworkIndicatorTargetBinding(object target)
         : base(target)
      {
      }

      public override MvxBindingMode DefaultMode => MvxBindingMode.OneWay;

      public override Type TargetType => typeof(UIViewController);

      public override void SetValue(object value)
      {
         var visible = (bool) value;

         if (_currentBindings == null)
         {
            _currentBindings = new List<NetworkIndicatorTargetBinding>();
         }

         if (visible)
         {
            _currentBindings.Add(this);
            UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
         }
         else
         {
            if (_currentBindings.Contains(this))
            {
               _currentBindings.Remove(this);
            }

            if (!_currentBindings.Any())
            {
               UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
               _currentBindings = null;
            }
         }
      }
   }
}
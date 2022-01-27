using System;
using MvvmCross.Plugin.Messenger;

namespace MvvmCrossDemo.Core.Infrastructure.Messages
{
   public class LaunchTimeMessage : MvxMessage
   {
      public LaunchTimeMessage(object sender, TimeSpan timeSpan)
         : base(sender) => TimeSpan = timeSpan;

      public TimeSpan TimeSpan { get; }
   }
}
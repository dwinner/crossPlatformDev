using System;
using System.Reflection;
using System.Windows.Input;
using PaperBoy.Common;
using Xamarin.Forms;

namespace PaperBoy.Behaviors
{
   public class EventToCommandBehavior : BaseBehavior<View>
   {
      public static readonly BindableProperty EventNameProperty = BindableProperty.Create("EventName", typeof(string),
         typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);

      public static readonly BindableProperty CommandProperty =
         BindableProperty.Create("Command", typeof(ICommand), typeof(EventToCommandBehavior));

      public static readonly BindableProperty CommandParameterProperty =
         BindableProperty.Create("CommandParameter", typeof(object), typeof(EventToCommandBehavior));

      public static readonly BindableProperty InputConverterProperty =
         BindableProperty.Create("Converter", typeof(IValueConverter), typeof(EventToCommandBehavior));

      private Delegate _eventHandler;

      public string EventName
      {
         get => (string) GetValue(EventNameProperty);
         set => SetValue(EventNameProperty, value);
      }

      public ICommand Command
      {
         get => (ICommand) GetValue(CommandProperty);
         set => SetValue(CommandProperty, value);
      }

      public object CommandParameter
      {
         get => GetValue(CommandParameterProperty);
         set => SetValue(CommandParameterProperty, value);
      }

      public IValueConverter Converter
      {
         get => (IValueConverter) GetValue(InputConverterProperty);
         set => SetValue(InputConverterProperty, value);
      }

      protected override void OnAttachedTo(View bindable)
      {
         base.OnAttachedTo(bindable);
         RegisterEvent(EventName);
      }

      protected override void OnDetachingFrom(View bindable)
      {
         DeregisterEvent(EventName);
         base.OnDetachingFrom(bindable);
      }

      private static void OnEventNameChanged(BindableObject bindable, object oldvalue, object newvalue)
      {
         var behavior = (EventToCommandBehavior) bindable;
         if (behavior.AssociatedObject == null)
         {
            return;
         }

         var oldEventName = (string) oldvalue;
         var newEventName = (string) newvalue;

         behavior.DeregisterEvent(oldEventName);
         behavior.RegisterEvent(newEventName);
      }

      private void DeregisterEvent(string oldEventName)
      {
         if (string.IsNullOrWhiteSpace(oldEventName))
         {
            return;
         }

         var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(oldEventName);
         if (eventInfo == null)
         {
            throw new ArgumentException(string.Format("EventToCommandBehavior: Can't register the '{0}' event.",
               EventName));
         }

         eventInfo.RemoveEventHandler(AssociatedObject, _eventHandler);
         _eventHandler = null;
      }

      private void RegisterEvent(string newEventName)
      {
         if (string.IsNullOrWhiteSpace(newEventName))
         {
            return;
         }

         var eventInfo = AssociatedObject.GetType().GetRuntimeEvent(newEventName);
         if (eventInfo == null)
         {
            throw new ArgumentException(string.Format("EventToCommandBehavior: Can't register the '{0}' event.",
               EventName));
         }

         var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
         _eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
         eventInfo.AddEventHandler(AssociatedObject, _eventHandler);
      }

      private void OnEvent(object sender, object eventArgs)
      {
         if (Command == null)
         {
            return;
         }

         object resolvedParameter;
         if (CommandParameter != null)
         {
            resolvedParameter = CommandParameter;
         }
         else if (Converter != null)
         {
            resolvedParameter = Converter.Convert(eventArgs, typeof(object), null, null);
         }
         else
         {
            resolvedParameter = eventArgs;
         }

         if (Command.CanExecute(resolvedParameter))
         {
            Command.Execute(resolvedParameter);
         }
      }
   }
}
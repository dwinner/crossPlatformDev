﻿using System;
using System.Windows.Input;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.BehaviorsPack
{
   [Preserve(AllMembers = true)]
   public class DisplayAlertBehavior : ReceiveNotificationBehavior<VisualElement, EventArgs>
   {
      public static readonly BindableProperty TitleProperty =
         BindableProperty.Create(nameof(Title), typeof(string), typeof(DisplayAlertBehavior));

      public static readonly BindableProperty MessageProperty =
         BindableProperty.Create(nameof(Message), typeof(string), typeof(DisplayAlertBehavior));

      public static readonly BindableProperty AcceptProperty =
         BindableProperty.Create(nameof(Accept), typeof(string), typeof(DisplayAlertBehavior));

      public static readonly BindableProperty CancelProperty =
         BindableProperty.Create(nameof(Cancel), typeof(string), typeof(DisplayAlertBehavior));

      internal ICommandExecutor CommandExecutor { get; set; } = new CommandExecutor();

      public string Title
      {
         get => (string) GetValue(TitleProperty);
         set => SetValue(TitleProperty, value);
      }

      public string Message
      {
         get => (string) GetValue(MessageProperty);
         set => SetValue(MessageProperty, value);
      }

      public string Accept
      {
         get => (string) GetValue(AcceptProperty);
         set => SetValue(AcceptProperty, value);
      }

      public string Cancel
      {
         get => (string) GetValue(CancelProperty);
         set => SetValue(CancelProperty, value);
      }

      protected override async void OnReceived(object sender, EventArgs eventArgs)
      {
         var currentPage = AssociatedObject.GetCurrentPage();
         if (currentPage != null)
         {
            var displayAlertRequestEventArgs = eventArgs as DisplayAlertRequestEventArgs;
            var title = displayAlertRequestEventArgs?.Title ?? Title;
            var message = displayAlertRequestEventArgs?.Message ?? Message;
            var acceptButton = displayAlertRequestEventArgs?.Accept ?? CreateAcceptButton(eventArgs);
            var cancelButton = displayAlertRequestEventArgs?.Cancel ?? CreateCancelButton(eventArgs);

            if (string.IsNullOrEmpty(acceptButton.Message))
            {
               await currentPage.DisplayAlert(title, message, cancelButton.Message);
               cancelButton.OnClicked(sender, eventArgs);
            }
            else
            {
               var result = await currentPage.DisplayAlert(title, message, acceptButton.Message, cancelButton.Message);
               if (result)
               {
                  acceptButton.OnClicked(sender, eventArgs);
               }
               else
               {
                  cancelButton.OnClicked(sender, eventArgs);
               }
            }
         }
      }

      private IAlertButton CreateAcceptButton(EventArgs eventArgs)
      {
         return new AlertButton
         {
            Message = Accept,
            Action = () =>
            {
               CommandExecutor.Execute(AcceptCommand, AcceptCommandParameter, eventArgs, AcceptEventArgsConverter,
                  AcceptEventArgsConverterParameter, AcceptEventArgsPropertyPath);
            }
         };
      }

      private IAlertButton CreateCancelButton(EventArgs eventArgs)
      {
         return new AlertButton
         {
            Message = Cancel,
            Action = () =>
            {
               CommandExecutor.Execute(CancelCommand, CancelCommandParameter, eventArgs, CancelEventArgsConverter,
                  CancelEventArgsConverterParameter, CancelEventArgsPropertyPath);
            }
         };
      }

      #region Accept

      public static readonly BindableProperty AcceptCommandProperty =
         BindableProperty.Create(nameof(AcceptCommand), typeof(ICommand), typeof(EventToCommandBehavior));

      public static readonly BindableProperty AcceptCommandParameterProperty =
         BindableProperty.Create(nameof(AcceptCommandParameter), typeof(object), typeof(EventToCommandBehavior));

      public static readonly BindableProperty AcceptEventArgsConverterProperty =
         BindableProperty.Create(nameof(AcceptEventArgsConverter), typeof(IValueConverter),
            typeof(EventToCommandBehavior));

      public static readonly BindableProperty AcceptEventArgsConverterParameterProperty =
         BindableProperty.Create(nameof(AcceptEventArgsConverterParameter), typeof(object),
            typeof(EventToCommandBehavior));

      public static readonly BindableProperty AcceptEventArgsPropertyPathProperty =
         BindableProperty.Create(nameof(AcceptEventArgsPropertyPath), typeof(string), typeof(EventToCommandBehavior));

      public ICommand AcceptCommand
      {
         get => (ICommand) GetValue(AcceptCommandProperty);
         set => SetValue(AcceptCommandProperty, value);
      }

      public object AcceptCommandParameter
      {
         get => GetValue(AcceptCommandParameterProperty);
         set => SetValue(AcceptCommandParameterProperty, value);
      }

      public IValueConverter AcceptEventArgsConverter
      {
         get => (IValueConverter) GetValue(AcceptEventArgsConverterProperty);
         set => SetValue(AcceptEventArgsConverterProperty, value);
      }

      public object AcceptEventArgsConverterParameter
      {
         get => GetValue(AcceptEventArgsConverterParameterProperty);
         set => SetValue(AcceptEventArgsConverterParameterProperty, value);
      }

      public string AcceptEventArgsPropertyPath
      {
         get => (string) GetValue(AcceptEventArgsPropertyPathProperty);
         set => SetValue(AcceptEventArgsPropertyPathProperty, value);
      }

      #endregion

      #region Cancel

      public static readonly BindableProperty CancelCommandProperty =
         BindableProperty.Create(nameof(CancelCommand), typeof(ICommand), typeof(EventToCommandBehavior));

      public static readonly BindableProperty CancelCommandParameterProperty =
         BindableProperty.Create(nameof(CancelCommandParameter), typeof(object), typeof(EventToCommandBehavior));

      public static readonly BindableProperty CancelEventArgsConverterProperty =
         BindableProperty.Create(nameof(CancelEventArgsConverter), typeof(IValueConverter),
            typeof(EventToCommandBehavior));

      public static readonly BindableProperty CancelEventArgsConverterParameterProperty =
         BindableProperty.Create(nameof(CancelEventArgsConverterParameter), typeof(object),
            typeof(EventToCommandBehavior));

      public static readonly BindableProperty CancelEventArgsPropertyPathProperty =
         BindableProperty.Create(nameof(CancelEventArgsPropertyPath), typeof(string), typeof(EventToCommandBehavior));

      public ICommand CancelCommand
      {
         get => (ICommand) GetValue(CancelCommandProperty);
         set => SetValue(CancelCommandProperty, value);
      }

      public object CancelCommandParameter
      {
         get => GetValue(CancelCommandParameterProperty);
         set => SetValue(CancelCommandParameterProperty, value);
      }

      public IValueConverter CancelEventArgsConverter
      {
         get => (IValueConverter) GetValue(CancelEventArgsConverterProperty);
         set => SetValue(CancelEventArgsConverterProperty, value);
      }

      public object CancelEventArgsConverterParameter
      {
         get => GetValue(CancelEventArgsConverterParameterProperty);
         set => SetValue(CancelEventArgsConverterParameterProperty, value);
      }

      public string CancelEventArgsPropertyPath
      {
         get => (string) GetValue(CancelEventArgsPropertyPathProperty);
         set => SetValue(CancelEventArgsPropertyPathProperty, value);
      }

      #endregion
   }
}
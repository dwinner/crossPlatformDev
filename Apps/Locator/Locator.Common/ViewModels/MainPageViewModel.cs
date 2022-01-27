using System;
using System.Windows.Input;
using Locator.Common.Enums;
using Locator.Common.Extras;
using Locator.Common.UI;

namespace Locator.Common.ViewModels
{
   /// <summary>
   ///    Main page view model.
   /// </summary>
   public class MainPageViewModel : ViewModelBase
   {
      /// <summary>
      ///    The methods.
      /// </summary>
      // ReSharper disable once NotAccessedField.Local
      private readonly IMethods _methods;

      /// <summary>
      ///    The description message.
      /// </summary>
      private string _descriptionMessage = "Find your location";

      /// <summary>
      ///    The exit command.
      /// </summary>
      private ICommand _exitCommand;

      /// <summary>
      ///    The exit title.
      /// </summary>
      private string _exitTitle = "Exit";

      /// <summary>
      ///    The location command.
      /// </summary>
      private ICommand _locationCommand;

      /// <summary>
      ///    The location title.
      /// </summary>
      private string _locationTitle = "Find Location";

      /// <summary>
      ///    Initializes a new instance of the <see cref="MainPageViewModel" /> class.
      /// </summary>
      /// <param name="navigation">Navigation.</param>
      /// <param name="commandFactory">Command factory.</param>
      /// <param name="methods">Methods.</param>
      public MainPageViewModel(INavigationService navigation, Func<Action, ICommand> commandFactory,
         IMethods methods) : base(navigation)
      {
         _exitCommand = commandFactory(methods.Exit);
         _locationCommand = commandFactory(() => Navigation.NavigateAsync(PageNames.MapPage, null));
         _methods = methods;
      }

      /// <summary>
      ///    Gets or sets the description message.
      /// </summary>
      /// <value>The description message.</value>
      public string DescriptionMessage
      {
         get => _descriptionMessage;

         set
         {
            if (value.Equals(_descriptionMessage))
            {
               return;
            }

            _descriptionMessage = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the location title.
      /// </summary>
      /// <value>The location title.</value>
      public string LocationTitle
      {
         get => _locationTitle;

         set
         {
            if (value.Equals(_locationTitle))
            {
               return;
            }

            _locationTitle = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the exit title.
      /// </summary>
      /// <value>The exit title.</value>
      public string ExitTitle
      {
         get => _exitTitle;

         set
         {
            if (value.Equals(_exitTitle))
            {
               return;
            }

            _exitTitle = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the location command.
      /// </summary>
      /// <value>The location command.</value>
      public ICommand LocationCommand
      {
         get => _locationCommand;

         set
         {
            if (value.Equals(_locationCommand))
            {
               return;
            }

            _locationCommand = value;
            OnPropertyChanged();
         }
      }

      /// <summary>
      ///    Gets or sets the exit command.
      /// </summary>
      /// <value>The exit command.</value>
      public ICommand ExitCommand
      {
         get => _exitCommand;

         set
         {
            if (value.Equals(_exitCommand))
            {
               return;
            }

            _exitCommand = value;
            OnPropertyChanged();
         }
      }
   }
}
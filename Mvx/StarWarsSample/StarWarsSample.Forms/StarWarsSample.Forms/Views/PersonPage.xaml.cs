using System.Threading.Tasks;
using MvvmCross.Base;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Forms.Presenters.Attributes;
using MvvmCross.ViewModels;
using StarWarsSample.Core.MvxInteraction;
using StarWarsSample.Core.ViewModels;
using Xamarin.Forms;

namespace StarWarsSample.Forms.UI.Views
{
   [MvxMasterDetailPagePresentation(WrapInNavigationPage = true)]
   public partial class PersonPage
   {
      private IMvxInteraction<DestructionAction> _interaction;
      private bool _showing;

      public PersonPage()
      {
         InitializeComponent();
      }

      public IMvxInteraction<DestructionAction> Interaction
      {
         get => _interaction;
         set
         {
            if (_interaction != null)
            {
               _interaction.Requested -= OnInteractionRequested;
            }

            _interaction = value;
            _interaction.Requested += OnInteractionRequested;
         }
      }

      protected override void OnViewModelSet()
      {
         base.OnViewModelSet();

         var set = this.CreateBindingSet<PersonPage, PersonViewModel>();
         set.Bind(this).For(v => v.Interaction).To(vm => vm.Interaction);
         set.Apply();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();

         _showing = true;

         Device.BeginInvokeOnMainThread(async () => await AnimateButton());
      }

      protected override void OnDisappearing()
      {
         _showing = false;

         base.OnDisappearing();

         ViewModel.CloseCompletionSource?.TrySetResult(false);
      }

      private async Task AnimateButton()
      {
         while (_showing)
         {
            await destroy.ScaleTo(1.1d, 600);
            await destroy.ScaleTo(0.9d, 600);
         }
      }

      private void OnInteractionRequested(object sender, MvxValueEventArgs<DestructionAction> eventArgs)
      {
         animationView.IsVisible = true;
         animationView.OnFinishedAnimation += (s, e) => eventArgs.Value.OnDestroyed();
         animationView.PlayAnimation();
      }
   }
}
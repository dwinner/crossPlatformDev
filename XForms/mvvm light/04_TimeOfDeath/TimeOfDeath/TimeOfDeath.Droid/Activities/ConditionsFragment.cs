using System;
using System.Windows.Input;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;

namespace TimeOfDeath.Droid
{
   public partial class ConditionsFragment : Fragment
   {
      private readonly bool falseFlag = false;
      private View view;
      public StateFoundViewModel ViewModel => App.Locator.StateFound;

      private ICommand ItemSelectedCommand
      {
         get
         {
            return new RelayCommand<AdapterView.ItemSelectedEventArgs>(t =>
            {
               if (t != null)
               {
                  var obj = t.Position;
               }
            });
         }
      }

      public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
      {
         var _ = base.OnCreateView(inflater, container, savedInstanceState);
         view = inflater.Inflate(Resource.Layout.conditions, null);
         PropogateSpinners();
         CreateBindings();
         return view;
      }

      public override void OnDestroyView()
      {
         base.OnDestroyView();
         KillViews();
      }

      private void CreateBindings()
      {
         BtnCalculate.SetCommand("Click", ViewModel.BtnCalculateResults);

         //SpinBodyCondition.SetCommand<AdapterView.ItemSelectedEventArgs>("ItemSelected", ItemSelectedCommand);
         SpinBodyCondition.SetBinding(() => SpinBodyCondition.SelectedItemPosition)
            .ObserveSourceEvent<AdapterView.ItemSelectedEventArgs>("ItemSelected").WhenSourceChanges(() =>
               Console.WriteLine("SelectedItem = {0}", SpinBodyCondition.SelectedItemPosition));
         SpinFoundAir.SetCommand<AdapterView.ItemSelectedEventArgs>("ItemSelected", ItemSelectedCommand);
         SpinFromWater.SetCommand<AdapterView.ItemSelectedEventArgs>("ItemSelected", ItemSelectedCommand);
         SpinFoundWater.SetCommand<AdapterView.ItemSelectedEventArgs>("ItemSelected", ItemSelectedCommand);

         // fools the linker into thinking we're using the events in release mode
         if (falseFlag)
         {
            SpinBodyCondition.ItemSelected += (sender, e) => { };
         }
      }

      private void PropogateSpinners()
      {
         var context = BtnCalculate.Context;
         var body = new ArrayAdapter(context, Android.Resource.Layout.SimpleSpinnerItem,
            ViewModel.GetBodyLayers.ToArray());
         body.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
         SpinBodyCondition.Adapter = body;
         var iw = new ArrayAdapter(context, Android.Resource.Layout.SimpleSpinnerItem,
            ViewModel.GetFoundWater.ToArray());
         iw.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
         SpinBodyCondition.Adapter = iw;
         var pw = new ArrayAdapter(context, Android.Resource.Layout.SimpleSpinnerItem,
            ViewModel.GetPulledWater.ToArray());
         pw.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
         SpinBodyCondition.Adapter = pw;
         var ia = new ArrayAdapter(context, Android.Resource.Layout.SimpleSpinnerItem, ViewModel.GetFoundAir.ToArray());
         ia.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
         SpinBodyCondition.Adapter = ia;
      }
   }
}
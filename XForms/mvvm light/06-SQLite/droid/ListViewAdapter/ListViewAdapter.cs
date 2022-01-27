using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using SQLiteExample.Models;
using SQLiteExample.ViewModel;

namespace SQLiteExample.Droid.ListViewAdapter
{
   public class ListViewAdapter : BaseAdapter<PersonalInfo>
   {
      private readonly Activity _context;
      private readonly ShowDataViewModel _viewModel;
      private readonly List<PersonalInfo> _data;

      public ListViewAdapter(Activity c, List<PersonalInfo> dta, ShowDataViewModel vm)
      {
         _context = c;
         _data = dta;
         _viewModel = vm;
      }

      public override int Count => _data?.Count ?? 0;

      public override PersonalInfo this[int position] => _data[position];


      public override long GetItemId(int position) => position;

      public override View GetView(int position, View convertView, ViewGroup parent)
      {
         var view = convertView;
         var item = _data[position];

         if (view == null)
         {
            view = _context.LayoutInflater.Inflate(Resource.Layout.ListData, parent, false);
            var id = view.FindViewById<TextView>(Resource.Id.txtID);
            var name = view.FindViewById<TextView>(Resource.Id.txtName);
            var address = view.FindViewById<TextView>(Resource.Id.txtAddress);
            var email = view.FindViewById<TextView>(Resource.Id.txtEmail);
            var mobile = view.FindViewById<TextView>(Resource.Id.txtMobile);
            var pets = view.FindViewById<Switch>(Resource.Id.swchPets);
            var hobbies = view.FindViewById<Switch>(Resource.Id.swchHobbies);
            pets.Enabled = hobbies.Enabled = false;

            id.Text = item.Id.ToString();
            name.Text = item.Name;
            address.Text = item.AddressLineOne;
            email.Text = item.EmailAddress;
            mobile.Text = item.MobilePhone;

            _viewModel.ParentId = item.Id;
            this.SetBinding(
               () => _viewModel.GetHasPet,
               () => pets.Checked,
               BindingMode.TwoWay);

            this.SetBinding(
               () => _viewModel.GetHasHobbies,
               () => hobbies.Checked,
               BindingMode.TwoWay);
         }

         return view;
      }
   }
}
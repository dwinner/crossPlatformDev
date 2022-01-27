using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DeviceUserName
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        protected async override void OnAppearing()
        {
            await DependencyService.Get<IUserInformation>().GetUserName().ContinueWith(t =>
            {
                Device.BeginInvokeOnMainThread(() => UserNameLabel.Text = t.Result);
            });
            base.OnAppearing();
        }
    }
}

using System;
using Xamarin.Forms;

namespace DataTransfer6
{
    public partial class DataTransfer6HomePage : ContentPage
    {
        public DataTransfer6HomePage()
        {
            InitializeComponent();
        }

        // Button Clicked handler.
        void OnGetInfoButtonClicked(object sender, EventArgs args)
        {
            // Navigate to the info page.
            GoToInfoPage(new InformationViewModel(), true);
        }

        // ListView ItemSelected handler.
        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                // De-select the item.
                listView.SelectedItem = null;

                // Navigate to the info page.
                GoToInfoPage((InformationViewModel)args.SelectedItem, false);
            }
        }

        async void GoToInfoPage(InformationViewModel info, bool isNewItem)
        {
            // Get AppData object (set to BindingContext in XAML file).
            AppData appData = (AppData)BindingContext;

            // Set info item to CurrentInfo property of AppData.
            appData.CurrentInfo = info;

            // Navigate to the info page.
            await Navigation.PushAsync(new DataTransfer6InfoPage());

            // Save new info item to the collection.
            if (isNewItem)
            {
                appData.InfoCollection.Add(info);
            }
        }
    }
}

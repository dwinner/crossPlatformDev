using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CommonViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkingWithText : ContentPage
    {
        public WorkingWithText()
        {
            InitializeComponent();
        }

        private void Entry1_Completed(object sender, EventArgs e)
        {

        }
    }
}
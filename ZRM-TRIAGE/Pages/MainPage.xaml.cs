using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase;
using Firebase.Database;
using Firebase.Database.Query;

namespace ZRM_TRIAGE
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }

        private async void CreateEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventCreatePage());
        }
    }
}

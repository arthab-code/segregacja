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

        private async void SubmitLogin(object sender, EventArgs e)
        {
            MainLoginProcess mLp = new MainLoginProcess(LoginCode.Text);

            if (!mLp.IsLocalizedLoginCode())
            {
                await DisplayAlert("Błąd logowania", "Podany kod jest nieprawidłowy", "OK");
                return;
            }

            var ambulanceFunction = mLp.RetrieveAmbulanceFunction();

            UserInfo.EventId = mLp.GetEventId();
            UserInfo.AmbulanceNumber = mLp.GetLoginCode();

            switch (ambulanceFunction)
            {
                case AmbulanceModel.Function.Major:
                    await Navigation.PushAsync(new KAMPage());
                    break;

                case AmbulanceModel.Function.Red:
                    await Navigation.PushAsync(new MajorRedPage());
                    break;

                case AmbulanceModel.Function.Yellow:
                    await Navigation.PushAsync(new MajorYellowPage());
                    break;

                case AmbulanceModel.Function.Green:
                    await Navigation.PushAsync(new MajorGreenPage());
                    break;

                case AmbulanceModel.Function.Transport:
                    await Navigation.PushAsync(new TransportAmbulancePage());
                    break;
            }

        }
        
    }
}

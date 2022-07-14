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
            FileSystem fs = new FileSystem();
           // fs.DeleteFile();
                      

        }
        protected override void OnAppearing()
        {
            AutoLogin();
        }
        private async void CreateEventClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EventCreatePage());
        }

        private async void LoginProcess(string loginTxt, string _event)
        {
            MainLoginProcess mLp = new MainLoginProcess(loginTxt);

            var ambulanceFunction = mLp.RetrieveAmbulanceFunction(_event);

            FileSystem fs = new FileSystem();

            string eventId = _event;
            string loginCode = loginTxt;

            UserInfo.SetEventId(eventId);
            UserInfo.SetAmbulanceNumber(loginCode);

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

        private void SubmitLogin(object sender, EventArgs e)
        {
            MainLoginProcess mLp = new MainLoginProcess(LoginCode.Text);

            if (!mLp.IsLocalizedLoginCode())
            {
                DisplayAlert("Błąd logowania", "Podany kod jest nieprawidłowy", "OK");
                return;
            }
            string a = mLp.GetEventId();
            LoginProcess(mLp.GetLoginCode(), mLp.GetEventId());

            FileSystem fs = new FileSystem();
            fs.CreateDataFile(LoginCode.Text, UserInfo.EventId);
        }

        private void AutoLogin()
        {
            FileSystem fs = new FileSystem();

            if (!fs.CheckFileExists())
                return;

            string pwd = fs.RetrieveLogin();
            string eventId = fs.RetrieveEventId();
            LoginProcess(pwd, eventId);
        }
        
    }
}

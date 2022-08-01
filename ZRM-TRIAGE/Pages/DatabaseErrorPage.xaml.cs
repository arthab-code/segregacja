using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Firebase;
using Firebase.Database;
using Firebase.Database.Query;
namespace ZRM_TRIAGE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatabaseErrorPage : ContentPage
    {

        public DatabaseErrorPage(string error)
        {
            InitializeComponent();
            Error.Text = "Błąd połączenia z bazą danych. Sprawdź połączenie z internetem lub skontaktuj się z administratorem! " + error;
            
        }

        private bool CheckConnect()
        {
            Database<AmbulanceModel> db = new Database<AmbulanceModel>();

            if (UserInfo.Ambulance == null) return true;

            var response = db.Read("Crews", UserInfo.EventId, UserInfo.Ambulance.DatabaseId);
                
            if (response != null)
                return true;
            else
                return false;

        }

        private void RefreshButtonClicked(object sender, EventArgs e)
        {
            if (CheckConnect())
            {
                Navigation.PushAsync(new MainPage());
                Navigation.PopToRootAsync();
            }
            else
                return;


        }
    }
}
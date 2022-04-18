using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZRM_TRIAGE
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class Procedures : ContentPage
{
    public Procedures()
    {
        InitializeComponent();
    }

        private async void StartProcedureButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TriageProcedures());
        }
    }
}
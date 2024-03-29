﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ZRM_TRIAGE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowHospitalPage : ContentPage
    {
        private HospitalModel _hospitalModel;
        public ShowHospitalPage(HospitalModel hospitalModel)
        {
            InitializeComponent();
            _hospitalModel = hospitalModel;

            HospitalName.Text = hospitalModel.Name;
            HospitalCity.Text = hospitalModel.City;
            HospitalStreet.Text = hospitalModel.Street;

            var _showAmbulanceVM = new ShowAmbulanceViewModel();

            var victimList = _showAmbulanceVM.GetTransportController().GetVictims().Where<VictimModel>(a => a.HospitalId == _hospitalModel.DatabaseId).ToList();
            // BindingContext = victimList;          
            VictimListXAML.ItemsSource = victimList;
            BindingContext = hospitalModel.VictimList;
            VictimListXAML.ItemsSource = victimList;
        }

        private async void DeleteHospitalButtonClicked(object sender, EventArgs e)
        {
            string optionChoice = await DisplayPromptAsync("Uwaga!", "Na pewno chcesz usunąć szpital " + _hospitalModel.Name + "? (wpisz: TAK jeśli chcesz usunąć)", "USUŃ", "ANULUJ", null, 3);

            ShowHospitalViewModel showHospitalViewModel = new ShowHospitalViewModel();

            if (optionChoice != null)
            {
                optionChoice = optionChoice.ToLower();

                if (optionChoice == "tak")
                {
                    showHospitalViewModel.DeleteHospital(_hospitalModel);

                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
        }

        private async void EditHospitalButtonClicked(object sender, EventArgs e)
        {
            HospitalModel hospital = new HospitalModel();

            ShowHospitalViewModel showHospitalViewModel = new ShowHospitalViewModel();

            bool choice = await DisplayAlert("Edycja szpitala " + _hospitalModel.Name, " Dokonać edycji?", "Tak", "nie");

            if (choice)
            {

                if (_hospitalModel.Name != HospitalName.Text)
                {
                    if (showHospitalViewModel.CheckHospitalExists(HospitalName.Text))
                    {
                        await DisplayAlert("Błąd", "Taki szpital już jest dodany", "OK");
                        return;
                    }
                }

            }

            hospital.Name = HospitalName.Text;
            hospital.City = HospitalCity.Text;
            hospital.Street = HospitalStreet.Text;
            hospital.DatabaseId = _hospitalModel.DatabaseId;

            showHospitalViewModel.UpdateHospital(hospital);

            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void VictimsListXAMLItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (VictimListXAML.SelectedItem == null)
                return;

            VictimModel victim = new VictimModel();

            victim = VictimListXAML.SelectedItem as VictimModel;

            await Navigation.PushAsync(new ShowVictimPage(victim));
        }
    }
}
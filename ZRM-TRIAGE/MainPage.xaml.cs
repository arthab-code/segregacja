﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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
            await Navigation.PushAsync(new KAMPage());
        }
    }
}

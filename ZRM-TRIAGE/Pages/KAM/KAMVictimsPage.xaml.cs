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
    public partial class KAMVictimsPage : TabbedPage
    {
        public KAMVictimsPage()
        {
            InitializeComponent();
        }

        private async void AddVictimButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddVictimPage());
        }
    }
}
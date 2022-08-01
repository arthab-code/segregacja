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
    public partial class TriagePage : ContentPage
    {
        private TriagePageViewModel _triagePageViewModel;
        public TriagePage()
        {

            InitializeComponent();

            try
            {
                _triagePageViewModel = new TriagePageViewModel();
                _triagePageViewModel.RefreshTriageData(ref RedInfo, ref YellowInfo, ref GreenInfo, ref BlackInfo);
            }
            catch
            {
                Navigation.PushAsync(new DatabaseErrorPage("Triage"));
                return;
            }

        }

        private void RedDecClicked(object sender, EventArgs e)
        {
            if (TriageModel.Instance.Red <= 0) return;

            _triagePageViewModel.NumberRed(-1);
            _triagePageViewModel.GetTriageModel();
            RedInfo.Text = TriageModel.Instance.Red.ToString();
        }

        private void RedIncClicked(object sender, EventArgs e)
        {
            _triagePageViewModel.NumberRed(1);
            _triagePageViewModel.GetTriageModel();
            RedInfo.Text = TriageModel.Instance.Red.ToString();
        }

        private void YellowDecClicked(object sender, EventArgs e)
        {
            if (TriageModel.Instance.Yellow <= 0) return;

            _triagePageViewModel.NumberYellow(-1);
            _triagePageViewModel.GetTriageModel();
            YellowInfo.Text = TriageModel.Instance.Yellow.ToString();
        }

        private void YellowIncClicked(object sender, EventArgs e)
        {
            _triagePageViewModel.NumberYellow(1);
            _triagePageViewModel.GetTriageModel();
            YellowInfo.Text = TriageModel.Instance.Yellow.ToString();
        }

        private void GreenDecClicked(object sender, EventArgs e)
        {
            if (TriageModel.Instance.Green <= 0) return;

            _triagePageViewModel.NumberGreen(-1);
            _triagePageViewModel.GetTriageModel();
            GreenInfo.Text = TriageModel.Instance.Green.ToString();
        }

        private void GreenIncClicked(object sender, EventArgs e)
        {
            _triagePageViewModel.NumberGreen(1);
            _triagePageViewModel.GetTriageModel();
            GreenInfo.Text = TriageModel.Instance.Green.ToString();
        }

        private void BlackDecClicked(object sender, EventArgs e)
        {

            if (TriageModel.Instance.Black <= 0) return;

            _triagePageViewModel.NumberBlack(-1);
            _triagePageViewModel.GetTriageModel();
            BlackInfo.Text = TriageModel.Instance.Black.ToString();
        }

        private void BlackIncClicked(object sender, EventArgs e)
        {
            _triagePageViewModel.NumberBlack(1);
            _triagePageViewModel.GetTriageModel();
            BlackInfo.Text = TriageModel.Instance.Black.ToString();
        }
    }
}
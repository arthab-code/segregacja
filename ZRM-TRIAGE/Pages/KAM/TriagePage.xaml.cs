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
        private TriageModel _triageModel;
        private TriageRepository _triageRepos;
        public TriagePage(TriageModel triageModel)
        {
            InitializeComponent();
            _triageModel = triageModel;
            _triageRepos = new TriageRepository();

        }
        protected override void OnAppearing()
        {
            _triageRepos.GetTriageModel();
            _triageModel = _triageRepos.GetTriageModelObject();
            RedInfo.Text = _triageModel.Red.ToString();
            YellowInfo.Text = _triageModel.Yellow.ToString();
            GreenInfo.Text = _triageModel.Green.ToString();
            BlackInfo.Text = _triageModel.Black.ToString();
        }

        private void RedDecClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberRed(-1);
            _triageModel = _triageRepos.GetTriageModelObject();
            RedInfo.Text = _triageModel.Red.ToString();
        }

        private void RedIncLicked(object sender, EventArgs e)
        {
            _triageRepos.NumberRed(1);
            _triageModel = _triageRepos.GetTriageModelObject();
            RedInfo.Text = _triageModel.Red.ToString();
        }

        private void YellowDecClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberYellow(-1);
            _triageModel = _triageRepos.GetTriageModelObject();
            YellowInfo.Text = _triageModel.Yellow.ToString();
        }

        private void YellowIncClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberYellow(1);
            _triageModel = _triageRepos.GetTriageModelObject();
            YellowInfo.Text = _triageModel.Yellow.ToString();
        }

        private void GreenDecClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberGreen(-1);
            _triageModel = _triageRepos.GetTriageModelObject();
            GreenInfo.Text = _triageModel.Green.ToString();
        }

        private void GreenIncClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberGreen(1);
            _triageModel = _triageRepos.GetTriageModelObject();
            GreenInfo.Text = _triageModel.Green.ToString();
        }

        private void BlackDecClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberBlack(-1);
            _triageModel = _triageRepos.GetTriageModelObject();
            BlackInfo.Text = _triageModel.Black.ToString();
        }

        private void BlackIncClicked(object sender, EventArgs e)
        {
            _triageRepos.NumberBlack(1);
            _triageModel = _triageRepos.GetTriageModelObject();
            BlackInfo.Text = _triageModel.Black.ToString();
        }
    }
}
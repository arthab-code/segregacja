using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PdfSharp.Xamarin.Forms;
using PdfSharpCore.Pdf;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore;
using PdfSharpCore.Utils;

namespace ZRM_TRIAGE
{
    public class ReportGenerator
    {

        private List<VictimModel> _victims;
        private List<AmbulanceModel> _ambulances;
        private List<HospitalModel> _hospitals;
        private string _eventPlace;
        private string _personalChief;

        public ReportGenerator(string eventPlace, string personalChief)
        {
            _victims = new List<VictimModel>();
            _ambulances = new List<AmbulanceModel>();
            _hospitals = new List<HospitalModel>();
            _eventPlace = eventPlace;
            _personalChief = personalChief;

        }

        private void LoadVictims()
        {
            VictimRepository victimRepository = new VictimRepository();
            List<VictimModel> redVictims = victimRepository.GetAll().Where(a => a.Color == VictimModel.TriageColor.Red).ToList();
            List<VictimModel> yellowVictims = victimRepository.GetAll().Where(a => a.Color == VictimModel.TriageColor.Yellow).ToList();
            List<VictimModel> greenVictims = victimRepository.GetAll().Where(a => a.Color == VictimModel.TriageColor.Green).ToList();
            List<VictimModel> blackVictims = victimRepository.GetAll().Where(a => a.Color == VictimModel.TriageColor.Black).ToList();

            foreach (var item in redVictims)
                _victims.Add(item);

            foreach (var item in yellowVictims)
                _victims.Add(item);

            foreach (var item in greenVictims)
                _victims.Add(item);

            foreach (var item in blackVictims)
                _victims.Add(item);
        }

        private void LoadAmbulances()
        {
            AmbulanceRepository ambulanceRepository = new AmbulanceRepository();

            _ambulances = ambulanceRepository.GetAll();
        }

        private void LoadHospitals()
        {
            HospitalRepository hospitalRepository = new HospitalRepository();

            _hospitals = hospitalRepository.GetAll();
        }

        public void LoadData()
        {
            LoadVictims();
            LoadAmbulances();
            LoadHospitals();
        }

        public void Generate()
        {      
            PdfDocument document = new PdfDocument();
            List <PdfPage> pages = new List<PdfPage> ();
            pages.Add(document.AddPage());

            XGraphics gfx = XGraphics.FromPdfPage(pages[0]);

            XFont font = new XFont("Verdana", 12, XFontStyle.Bold);

            int pointY = 0;
            int pointX = 100;
            int space = 20;
            int pagePosition = 0;
            gfx.DrawString("ZRM-TRIAGE", font, XBrushes.Black, new XRect(pages[pagePosition].Width/2, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += 3*space;
            gfx.DrawString("Miejsce zdarzenia: " + _eventPlace, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;
            gfx.DrawString("KAM: " + _personalChief, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += 2 * space;
            int counter = 1;

            /* Wyświetlanie poszkodowanych wyrażone w liczbach */
            SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);  //new page
            pointY += 2 * space;
            gfx.DrawString("ŁĄCZNA LICZBA POSZKODOWANYCH: " + TriageModel.Instance.Amount, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;
            if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
            {
                SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
            }
            gfx.DrawString("CZERWONI: " + TriageModel.Instance.Red, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;
            if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
            {
                SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
            }
            gfx.DrawString("ŻÓŁCI: " + TriageModel.Instance.Yellow, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;
            if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
            {
                SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
            }
            gfx.DrawString("ZIELONI: " + TriageModel.Instance.Green, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;
            if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
            {
                SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
            }
            gfx.DrawString("CZARNI: " + TriageModel.Instance.Black, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;
            if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
            {
                SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
            }

            /* Wyświetlanie listy poszkodowanych */
            SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document); // new page
            pointY += 2 * space;
            gfx.DrawString("POSZKODOWANI:", font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += 2 * space;
            foreach (var item in _victims)
            {
                gfx.DrawString("Poszkodowany nr: " + counter, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition,ref gfx,ref pages,ref document);
                }
                gfx.DrawString("Imię i nazwisko: " + item.Name + " " + item.Surname, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Kolor: " + item.Color, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Szpital: " + item.Hospital, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Transportowany przez ambulans: " + item.Ambulance, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);            
                pointY += 2 * space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition,ref gfx,ref pages,ref document);
                }
                counter++;

            }

            /*Lista ambulansów bioracych udział w zdarzeniu*/
            SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);  //new page
            pointY += 2 * space;
            gfx.DrawString("LISTA KARETEK BIORĄCYCH UDZIAŁ W ZDARZENIU: ", font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;

            foreach (var item in _ambulances)
            {

                gfx.DrawString("Numer zespołu: " + item.Number, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Kierownik zespołu: " + item.ChiefPersonalData, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Funkcja: " + item.AmbulanceFunction, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += 2*space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }

            }

            /*Lista szpitali bioracych udział w zdarzeniu*/
            SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);  //new page
            pointY += 2 * space;
            gfx.DrawString("SZPITALE PRZYJMUJĄCE POSZKODOWANYCH: ", font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
            pointY += space;

            foreach (var item in _hospitals)
            {
                gfx.DrawString("Nazwa szpitala: " + item.Name, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Miasto: " + item.City, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Ulica: " + item.Street, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                gfx.DrawString("Przyjęci poszkodowani: ", font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                pointY += space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }
                List<VictimModel> victims = _victims.Where<VictimModel>(a => a.HospitalId == item.Id).ToList();

                  foreach(var victim in victims)
                  {
                    gfx.DrawString(victim.Name + " "+ victim.Surname, font, XBrushes.Black, new XRect(pointX, pointY, pages[pagePosition].Width, pages[pagePosition].Height), XStringFormats.TopLeft);
                    pointY += space;
                    if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                    {
                        SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                    }
                 }

                pointY += 2*space;
                if (CheckEndPage(pointY, (int)pages[pagePosition].Height - space))
                {
                    SetNewValues(ref pointY, ref pagePosition, ref gfx, ref pages, ref document);
                }

            }

            var stream = new MemoryStream();

            document.Save(stream);
            document.Close();
               

            Xamarin.Forms.DependencyService.Get<IPdfSave>().Save(UserInfo.EventId + ".pdf","application/pdf",stream);
        }

        private bool CheckEndPage(int pointY, int pageHeight)
        {
            return (pointY >= pageHeight);
        }

        private void SetNewValues(ref int pointY, ref int pagePosition, ref XGraphics gfx, ref List<PdfPage> pages,ref PdfDocument document)
        {
            pointY = 0;
            pagePosition++;
            pages.Add(document.AddPage());
            gfx = XGraphics.FromPdfPage(pages[pagePosition]);
        }
    }
}

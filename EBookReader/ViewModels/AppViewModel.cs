using EBookReader.Commands;
using EBookReader.Helpers;
using EBookReader.Models;
using EBookReader.Services;
using Syncfusion.Pdf;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace EBookReader.ViewModels
{
    public class AppViewModel : BaseViewModel
    {
        private List<InstalledVoice> voices;

        public List<InstalledVoice> Voices
        {
            get { return voices; }
            set { voices = value; }
        }

        private InstalledVoice selectedVoice;

        public InstalledVoice SelectedVoice
        {
            get { return selectedVoice; }
            set { selectedVoice = value;OnPropertyChanged(); }
        }


        private string pageNumbers;

        public string PageNumbers
        {
            get { return pageNumbers; }
            set { pageNumbers = value; }
        }

        public Thread SpeakThread { get; set; }
        public PdfViewerControl BookPdfViewer { get; set; }


        public int Index { get; set; } = 1;

        public RelayCommand StartCurrentPageSpeakCommand { get; set; }
        public RelayCommand StartSelectedPageSpeakCommand { get; set; }
        public RelayCommand StartAllPagesSpeakCommand { get; set; }
        public RelayCommand StopSpeakCommand { get; set; }
        public RelayCommand PauseSpeakCommand { get; set; }
        public RelayCommand ResumeSpeakCommand { get; set; }
        public AppViewModel()
        {
            Voices = TextToSpeechService._ss.GetInstalledVoices().ToList();
            SelectedVoice = Voices[0];
            StartCurrentPageSpeakCommand = new RelayCommand((o) =>
            {
                TextToSpeechService.SelecetedVoice = SelectedVoice;
                SpeakThread = new Thread(() =>
                {
                    var pages = BookPdfViewer.LoadedDocument.Pages;
                    var tex = pages[BookPdfViewer.CurrentPage - 1].ExtractText();
                    TextToSpeechService.TextToSpeech(tex);
                });
                SpeakThread.Start();
            });

            StartSelectedPageSpeakCommand = new RelayCommand((o) =>
            {
                TextToSpeechService.SelecetedVoice = SelectedVoice;
                SpeakThread = new Thread(() =>
                {
                    var pagess = PageNumbers.Split(',');
                    var pages = BookPdfViewer.LoadedDocument.Pages;

                    foreach (var item in pagess)
                    {
                        int p = Convert.ToInt32(item);
                        var tex = pages[p - 1].ExtractText();
                        TextToSpeechService.TextToSpeech(tex);
                    }
                });
                SpeakThread.Start();

            });

            StartAllPagesSpeakCommand = new RelayCommand((o) =>
            {
                TextToSpeechService.SelecetedVoice = SelectedVoice;

                SpeakThread = new Thread(() =>
                {
                    var pages = BookPdfViewer.LoadedDocument.Pages;
                    foreach (PdfPageBase item in pages)
                    {
                        TextToSpeechService.TextToSpeech(item.ExtractText());
                    }
                });
                SpeakThread.Start();
            });

            StopSpeakCommand = new RelayCommand((o) =>
            {
                try
                {
                    SpeakThread.Abort();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });

            PauseSpeakCommand = new RelayCommand((o) =>
            {
                try
                {
                    TextToSpeechService._ss.Pause();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });


            ResumeSpeakCommand = new RelayCommand((o) =>
            {
                TextToSpeechService.SelecetedVoice = SelectedVoice;

                try
                {
                    TextToSpeechService._ss.Resume();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            });


        }
    }
}

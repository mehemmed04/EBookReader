using EBookReader.Commands;
using EBookReader.Helpers;
using EBookReader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReader.ViewModels
{
    public class AppViewModel:BaseViewModel
    {

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value;OnPropertyChanged(); }
        }

        public RelayCommand StartSpeakCommand { get; set; }
        public AppViewModel()
        {
            StartSpeakCommand = new RelayCommand((o) =>
            {
                var text = PDFReader.GetText(Path);
                TextToSpeechService.TextToSpeech(text);
            });
        }
    }
}

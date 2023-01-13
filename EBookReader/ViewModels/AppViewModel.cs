using EBookReader.Commands;
using EBookReader.Helpers;
using EBookReader.Models;
using EBookReader.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReader.ViewModels
{
    public class AppViewModel : BaseViewModel
    {

        public List<Page> BookPages { get; set; }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; OnPropertyChanged(); }
        }

        private Page currentPage;

        public Page CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; OnPropertyChanged(); }
        }

        private Page nextPage;

        public Page NextPage
        {
            get { return nextPage; }
            set { nextPage = value; OnPropertyChanged(); }
        }

        public int Index { get; set; } = 1;

        public RelayCommand StartSpeakCommand { get; set; }
        public RelayCommand GetPdfCommand { get; set; }
        public RelayCommand NextPageCommand { get; set; }
        public AppViewModel()
        {
            GetPdfCommand = new RelayCommand((o) =>
            {
                BookPages = PDFReader.GetPages(Path);
                CurrentPage = BookPages[0];
                NextPage = BookPages[1];
            });

            NextPageCommand = new RelayCommand((o) =>
            {
                CurrentPage = NextPage;
                Index += 1;
                NextPage = BookPages[Index];
            });

            StartSpeakCommand = new RelayCommand((o) =>
            {
                TextToSpeechService.TextToSpeech(CurrentPage.Content);
            });


        }
    }
}

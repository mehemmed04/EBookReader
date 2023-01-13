using EBookReader.Commands;
using EBookReader.Helpers;
using EBookReader.Models;
using EBookReader.Services;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReader.ViewModels
{
    public class AppViewModel : BaseViewModel
    {

        //public List<Page> BookPages { get; set; }

        //private string path;

        //public string Path
        //{
        //    get { return path; }
        //    set { path = value; OnPropertyChanged(); }
        //}

        //private Page currentPage;

        //public Page CurrentPage
        //{
        //    get { return currentPage; }
        //    set { currentPage = value; OnPropertyChanged(); }
        //}

        //private Page nextPage;

        //public Page NextPage
        //{
        //    get { return nextPage; }
        //    set { nextPage = value; OnPropertyChanged(); }
        //}
        //private Image pdfimage;

        //public Image PdfImage
        //{
        //    get { return pdfimage; }
        //    set { pdfimage = value;OnPropertyChanged(); }
        //}

        public PdfViewerControl BookPdfViewer { get; set; }
        

        public int Index { get; set; } = 1;

        public RelayCommand StartSpeakCommand { get; set; }
        //public RelayCommand GetPdfCommand { get; set; }
        //public RelayCommand NextPageCommand { get; set; }
        public AppViewModel()
        {
            //GetPdfCommand = new RelayCommand((o) =>
            //{
            //    BookPdfViewer.Load(Path);
                
            //    //BookPdfViewer.Print();
                
            //    CurrentPage = BookPages[0];
            //   // PdfImage = CurrentPage.Images[0];
            //    NextPage = BookPages[1];
            //});

            //NextPageCommand = new RelayCommand((o) =>
            //{
            //    CurrentPage = NextPage;
            //    Index += 1;
            //    //PdfImage = CurrentPage.Images[0];
            //    NextPage = BookPages[Index];
            //});

            StartSpeakCommand = new RelayCommand((o) =>
            {
                var pages = BookPdfViewer.LoadedDocument.Pages;
                var tex =  pages[BookPdfViewer.CurrentPage-1].ExtractText();
                
                TextToSpeechService.TextToSpeech(tex);
            });


        }
    }
}

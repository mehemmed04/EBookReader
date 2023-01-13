using EBookReader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPdf;



namespace EBookReader.Helpers
{
    public class PDFReader
    {
        public static List<Page> GetPages(string path)
        {
            var pages = new List<Page>();
            ////StringBuilder sb = new StringBuilder();
            //using(PdfReader reader = new PdfReader(path))
            //{
            //    for(int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
            //    {
            //        ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
            //        string text = PdfTextExtractor.GetTextFromPage(reader, pageNo,strategy);
            //        text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
            //        pages.Add(new Page
            //        {
            //            Content = text,
            //            PageNo = pageNo,
            //        });
            //    }
            //}


            // Extracting Image and Text content from Pdf Documents

            // open a 128 bit encrypted PDF
            var pdf = PdfDocument.FromFile(path);

            //// Get all text to put in a search index
            //string text = pdf.ExtractAllText();

            //// Get all Images
            //var allImages = pdf.ExtractAllImages();

            // Or even find the precise text and images for each page in the document
            for (var index = 1; index <= pdf.PageCount; index++)
            {
                string text = pdf.ExtractTextFromPage(index);
                IEnumerable<System.Drawing.Image> images = pdf.ExtractImagesFromPage(index);
                pages.Add(new Page
                {
                    Content = text,
                    PageNo = index,
                    Images = images.ToList()
                });
            }
            return pages;
        }
    }
}

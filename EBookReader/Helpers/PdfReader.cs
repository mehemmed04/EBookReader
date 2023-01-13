using EBookReader.Models;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReader.Helpers
{
    public class PDFReader
    {
        public static List<Page> GetPages(string path)
        {
            var pages = new List<Page>();
            //StringBuilder sb = new StringBuilder();
            using(PdfReader reader = new PdfReader(path))
            {
                for(int pageNo = 1; pageNo <= reader.NumberOfPages; pageNo++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string text = PdfTextExtractor.GetTextFromPage(reader, pageNo,strategy);
                    text = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(text)));
                    pages.Add(new Page
                    {
                        Content = text,
                        PageNo = pageNo,
                    });
                }
            }
            return pages;
        }
    }
}

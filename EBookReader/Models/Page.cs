using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EBookReader.Models
{
    public class Page
    {
        public int PageNo { get; set; }
        public string Content { get; set; }
        public List<Image> Images { get; set; }

    }
}

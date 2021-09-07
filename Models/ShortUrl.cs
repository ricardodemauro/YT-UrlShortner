using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortnerVideo.Models
{
    public class ShortUrl
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string Chunck { get; set; }
    }
}

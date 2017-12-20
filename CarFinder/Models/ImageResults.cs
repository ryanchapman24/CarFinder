using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarFinder.Models
{
    public class ImageResults
    {
        public string ThumbnailLink { get; set; }

        public string ImageLink { get; set; }

        public string FileFormat { get; set; }

        public string Title { get; set; }

        public string ContextLink { get; set; }
    }
}
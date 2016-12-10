using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPA_MoviePosterAwards.Web.Models
{
    public class PosterCreateViewModel
    {
        public string Id { get; set; }
        public string Movie { get; set; }
        public string Poster { get; set; }
        public string Poster_S { get; set; }
        public string Poster_XS { get; set; }
    }
}
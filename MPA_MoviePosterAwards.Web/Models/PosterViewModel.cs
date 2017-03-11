using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPA_MoviePosterAwards.Web.Models
{
    public class PosterViewModel
    {
        public string Id { get; set; }
        public string Movie { get; set; }
        public string Poster { get; set; }
        public string Poster_M { get; set; }
        public string Poster_S { get; set; }
        public string Poster_XS { get; set; }
        public string Time { get; set; }
        public bool Active { get; set; } = false;
    }

    public class PosterCreateViewModel
    {
        public string Id { get; set; }
        public string Movie { get; set; }
        public string Poster { get; set; }
        public string Poster_M { get; set; }
        public string Poster_S { get; set; }
        public string Poster_XS { get; set; }
    }

    public class NewestPosterViewModel
    {
        public string Url { get; set; }
        public string ID { get; set; }
        public string MovieID { get; set; }
        public string MovieTite { get; set; }
        public string Time { get; set; }
    }
}
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
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class PosterIndexViewModel
    {
        public List<PosterViewModel> Posters { get; set; }
        public PosterViewModel ActivePoster { get; set; }
        public MovieViewModel Movie { get; set; }
        public UserViewModel User { get; set; }
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
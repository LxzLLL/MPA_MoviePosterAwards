using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Model
{
    public class Step_Movie_Poster_Info
    {
        public Guid Id { get; set; }
        public Guid Movie { get; set; }
        public string Small { get; set; }
        public string Large { get; set; }
        public string Medium { get; set; }
    }
}

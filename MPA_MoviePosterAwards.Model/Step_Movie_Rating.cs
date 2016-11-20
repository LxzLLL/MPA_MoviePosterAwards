using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Model
{
    public class Step_Movie_Rating
    {
        public Guid Id { get; set; }
        public double Score { get; set; }
        public Guid Movie { get; set; }
        public string Star5 { get; set; }
        public string Star4 { get; set; }
        public string Star3 { get; set; }
        public string Star2 { get; set; }
        public string Star1 { get; set; }
        public long Rated_Num { get; set; }
    }
}

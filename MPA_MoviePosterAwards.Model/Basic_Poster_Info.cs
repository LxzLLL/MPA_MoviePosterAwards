using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Model
{
    public class Basic_Poster_Info
    {
        public Guid Id { get; set; }
        public Guid Movie { get; set; }
        public Guid User { get; set; }
        public string Poster { get; set; }
        public string Poster_M { get; set; }
        public string Poster_S { get; set; }
        public string Poster_XS { get; set; }
        public DateTime Time { get; set; }
    }
}

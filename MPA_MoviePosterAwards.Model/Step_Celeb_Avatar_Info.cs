using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Model
{
    public class Step_Celeb_Avatar_Info
    {
        public Guid Id { get; set; }
        public Guid Celeb { get; set; }
        public string Small { get; set; }
        public string Large { get; set; }
        public string Medium { get; set; }
    }
}

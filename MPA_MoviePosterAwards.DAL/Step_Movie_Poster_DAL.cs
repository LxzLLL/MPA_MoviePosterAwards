using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Movie_Poster_DAL
    {
        public static Step_Movie_Poster_Info ToModel(Step_Movie_Poster poster)
        {
            Step_Movie_Poster_Info info = new Step_Movie_Poster_Info();

            info.Id = poster.Id;
            info.Movie = (Guid)poster.Movie;
            info.Large = poster.Large;
            info.Small = poster.Small;
            info.Medium = poster.Medium;

            return info;
        }

        public static Step_Movie_Poster ToDatabase(Step_Movie_Poster_Info info)
        {
            Step_Movie_Poster poster = new Step_Movie_Poster();

            poster.Id = info.Id;
            poster.Movie = info.Movie;
            poster.Large = info.Large;
            poster.Small = info.Small;
            poster.Medium = info.Medium;

            return poster;
        }
    }
}

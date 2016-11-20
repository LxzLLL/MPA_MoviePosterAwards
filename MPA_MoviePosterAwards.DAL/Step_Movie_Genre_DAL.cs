using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Movie_Genre_DAL
    {
        public static Step_Movie_Genre_Info ToModel(Step_Movie_Genre genre)
        {
            Step_Movie_Genre_Info info = new Step_Movie_Genre_Info();

            info.Id = genre.Id;
            info.Movie = (Guid)genre.Movie;
            info.Genre = genre.Genre;

            return info;
        }

        public static Step_Movie_Genre ToDatabase(Step_Movie_Genre_Info info)
        {
            Step_Movie_Genre genre = new Step_Movie_Genre();

            genre.Id = info.Id;
            genre.Movie = info.Movie;
            genre.Genre = info.Genre;

            return genre;
        }
    }
}

using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Movie_Rating_DAL
    {
        public static Step_Movie_Rating_Info ToModel(Step_Movie_Rating rating)
        {
            Step_Movie_Rating_Info info = new Step_Movie_Rating_Info();

            info.Id = rating.Id;
            info.Movie = (Guid)rating.Movie;
            info.Rated_Num = (long)rating.Rated_Num;
            info.Score = (double)rating.Score;
            info.Star1 = rating.Star1;
            info.Star2 = rating.Star2;
            info.Star3 = rating.Star3;
            info.Star4 = rating.Star4;
            info.Star5 = rating.Star5;

            return info;
        }

        public static Step_Movie_Rating ToDatabase(Step_Movie_Rating_Info info)
        {
            Step_Movie_Rating rating = new Step_Movie_Rating();

            rating.Id = info.Id;
            rating.Movie = info.Movie;
            rating.Rated_Num = info.Rated_Num;
            rating.Score = info.Score;
            rating.Star1 = info.Star1;
            rating.Star2 = info.Star2;
            rating.Star3 = info.Star3;
            rating.Star4 = info.Star4;
            rating.Star5 = info.Star5;

            return rating;
        }
    }
}

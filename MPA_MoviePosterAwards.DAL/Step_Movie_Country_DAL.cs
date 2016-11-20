using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Movie_Country_DAL
    {
        public static Step_Movie_Country_Info ToModel(Step_Movie_Country country)
        {
            Step_Movie_Country_Info info = new Step_Movie_Country_Info();

            info.Id = country.Id;
            info.Movie = (Guid)country.Movie;
            info.Country = country.Country;

            return info;
        }

        public static Step_Movie_Country ToDatabase(Step_Movie_Country_Info info)
        {
            Step_Movie_Country country = new Step_Movie_Country();

            country.Id = info.Id;
            country.Movie = info.Movie;
            country.Country = info.Country;

            return country;
        }
    }
}

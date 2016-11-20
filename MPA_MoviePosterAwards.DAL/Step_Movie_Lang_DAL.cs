using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Movie_Lang_DAL
    {
        public static Step_Movie_Lang_Info ToModel(Step_Movie_Lang lang)
        {
            Step_Movie_Lang_Info info = new Step_Movie_Lang_Info();

            info.Id = lang.Id;
            info.Movie = (Guid)lang.Movie;
            info.Lang = lang.Lang;

            return info;
        }

        public static Step_Movie_Lang ToDatabase(Step_Movie_Lang_Info info)
        {
            Step_Movie_Lang lang = new Step_Movie_Lang();

            lang.Id = info.Id;
            lang.Movie = info.Movie;
            lang.Lang = info.Lang;

            return lang;
        }
    }
}

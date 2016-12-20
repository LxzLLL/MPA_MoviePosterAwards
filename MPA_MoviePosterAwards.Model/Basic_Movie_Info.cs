using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.Model
{
    public class Basic_Movie_Info
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Title_En { get; set; }
        public string Aka { get; set; }
        public short Year { get; set; }
        public string Website { get; set; }
        public short Current_Season { get; set; }
        public short Season_Count { get; set; }
        public int Episode_Count { get; set; }
        public string Pubdate { get; set; }
        public string Duration { get; set; }
        public string Douban { get; set; }
        public string IMDb { get; set; }
        public string Summary { get; set; }

        public ICollection<Step_Movie_Country_Info> Countries { get; set; }
        public ICollection<Step_Movie_Genre_Info> Genres { get; set; }
        public ICollection<Step_Movie_Lang_Info> Langs { get; set; }
        public Step_Movie_Rating_Info Rating { get; set; }
        public Step_Movie_Poster_Info Poster { get; set; }
        public List<Step_Celeb_Movie_Info> Celebs { get; set; }

        public Basic_Movie_Info()
        {
            Countries = new List<Step_Movie_Country_Info>();
            Genres = new List<Step_Movie_Genre_Info>();
            Langs = new List<Step_Movie_Lang_Info>();
            Rating = new Step_Movie_Rating_Info();
            Poster = new Step_Movie_Poster_Info();
            Celebs = new List<Step_Celeb_Movie_Info>();
        }
    }
}

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
        public string Country { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public string Website { get; set; }
        public short Current_Season { get; set; }
        public short Season_Count { get; set; }
        public int Episode_Count { get; set; }
        public string Pubdate { get; set; }
        public string Duration { get; set; }
        public string Douban { get; set; }
        public string IMDb { get; set; }
        public string Summary { get; set; }
        public string Avatar_Large { get; set; }
        public string Avatar_Medium { get; set; }
        public string Avatar_Small { get; set; }
        public double Rating_Score { get; set; }
        public int Rating_Count { get; set; }
        public string Rating_Star5 { get; set; }
        public string Rating_Star4 { get; set; }
        public string Rating_Star3 { get; set; }
        public string Rating_Star2 { get; set; }
        public string Rating_Star1 { get; set; }
        public List<Step_Celeb_Movie_Info> Celebs { get; set; } = new List<Step_Celeb_Movie_Info>();
    }
}

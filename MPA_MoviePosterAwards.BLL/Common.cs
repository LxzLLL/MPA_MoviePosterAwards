using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class MovieJson
    {
        public MovieRatingJson rating { get; set; }
        public string reviews_count { get; set; }
        public string wish_count { get; set; }
        public string douban_site { get; set; }
        public string year { get; set; }
        public MovieImageJson images { get; set; }
        public string alt { get; set; }
        public string id { get; set; }
        public string mobile_url { get; set; }
        public string title { get; set; }
        public string do_count { get; set; }
        public string share_url { get; set; }
        public string seasons_count { get; set; }
        public string schedule_url { get; set; }
        public string episodes_count { get; set; }
        public List<string> countries { get; set; }
        public List<string> genres { get; set; }
        public string collect_count { get; set; }
        public List<MovieCastJson> casts { get; set; }
        public string current_season { get; set; }
        public string original_title { get; set; }
        public string summary { get; set; }
        public string subtype { get; set; }
        public List<MovieCastJson> directors { get; set; }
        public string comments_count { get; set; }
        public string ratings_count { get; set; }
        public List<string> aka { get; set; }
    }

    public class MovieRatingJson
    {
        public double max { get; set; }
        public double average { get; set; }
        public string stars { get; set; }
        public double min { get; set; }
    }

    public class MovieImageJson
    {
        public string small { get; set; }
        public string large { get; set; }
        public string medium { get; set; }
    }

    public class MovieCastJson
    {
        public string alt { get; set; }
        public MovieImageJson avatars { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class CelebJson
    {
        public string mobile_url { get; set; }
        public List<string> aka_en { get; set; }
        public string name { get; set; }
        public string gender { get; set; }

        public MovieImageJson avatars { get; set; }
        public string id { get; set; }
        public List<string> aka { get; set; }
        public string name_en { get; set; }
        public string born_place { get; set; }
        public string alt { get; set; }
    }
}

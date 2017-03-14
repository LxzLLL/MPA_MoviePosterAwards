using MPA_MoviePosterAwards.BLL;
using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPA_MoviePosterAwards.Web.Models
{
    public class MovieViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Title_En { get; set; }
        public string Aka { get; set; }
        public List<LinkItem> Directors { get; set; }
        public List<LinkItem> Casts { get; set; }
        public List<LinkItem> Writers { get; set; }
        public int Year { get; set; }
        public string Pubdates { get; set; }
        public int Episode_Count { get; set; }
        public int Current_Season { get; set; }
        public int Season_Count { get; set; }
        public string Durations { get; set; }
        public string Website { get; set; }
        public string Genres { get; set; }
        public string Languages { get; set; }
        public string Countries { get; set; }
        public string Rating { get; set; }
        //public string RatingCount { get; set; }
        public string Douban { get; set; }
        public string IMDb { get; set; }
        public string Summary { get; set; }
        public string Avatar { get; set; }

        //public List<string> Posters { get; set; }

        public MovieViewModel(Basic_Movie_Info movie)
        {
            Id = movie.Id.ToString();
            Title = movie.Title;
            Title_En = movie.Title_En.IsBlank() ? string.Empty : movie.Title_En;
            Aka = movie.Aka.IsBlank() ? string.Empty : movie.Aka;
            Pubdates = movie.Pubdate.IsBlank() ? string.Empty : movie.Pubdate;
            Year = movie.Year;
            Durations = movie.Duration.IsBlank() ? string.Empty : movie.Duration;
            Website = movie.Website.IsBlank() ? string.Empty : movie.Website;
            Episode_Count = movie.Episode_Count;
            Current_Season = movie.Current_Season;
            Season_Count = movie.Season_Count;

            Genres = string.Empty;
            foreach (var item in movie.Genres)
            {
                Genres += item.Genre + " / ";
            }
            Genres = Genres.Substring(0, Genres.Length - 2);

            Languages = string.Empty;
            foreach (var item in movie.Langs)
            {
                Languages += item.Lang + " / ";
            }
            Languages = Languages.Substring(0, Languages.Length - 2);

            Countries = string.Empty;
            foreach (var item in movie.Countries)
            {
                Countries += item.Country + " / ";
            }
            Countries = Countries.Substring(0, Countries.Length - 2);

            Rating = movie.Rating.Score.ToString().Length == 1 ? movie.Rating.Score.ToString() + ".0" : movie.Rating.Score.ToString();
            //RatingCount = movie.Rating.Rated_Num.ToString();
            Avatar = !movie.Poster.Large.IsBlank() ? movie.Poster.Large : !movie.Poster.Medium.IsBlank() ? movie.Poster.Medium : !movie.Poster.Small.IsBlank() ? movie.Poster.Small : string.Empty;
            Summary = movie.Summary.IsBlank() ? string.Empty : movie.Summary;
            Douban = movie.Douban.IsBlank() ? string.Empty : movie.Douban;
            IMDb = movie.IMDb.IsBlank() ? string.Empty : movie.IMDb;

            Directors = new List<LinkItem>();
            var directors = Basic_Celebrity_BLL.GetList(Guid.Parse(Id), "导演");
            foreach (var item in directors)
            {
                Directors.Add(new LinkItem() { Url = "/Celeb/Index?id=" + item.Id, Title = item.Name });
            }

            Writers = new List<LinkItem>();
            var writers = Basic_Celebrity_BLL.GetList(Guid.Parse(Id), "编剧");
            foreach (var item in writers)
            {
                Writers.Add(new LinkItem() { Url = "/Celeb/Index?id=" + item.Id, Title = item.Name });
            }

            Casts = new List<LinkItem>();
            var casts = Basic_Celebrity_BLL.GetList(Guid.Parse(Id), "主演");
            foreach (var item in casts)
            {
                Casts.Add(new LinkItem() { Url = "/Celeb/Index?id=" + item.Id, Title = item.Name });
            }

            //Posters = new List<string>();
            //var posters = Basic_Poster_BLL.GetList(Guid.Parse(Id));
            //foreach (var item in posters)
            //{
            //    Posters.Add(item.Poster);
            //}
        }
    }

    public class LinkItem
    {
        public string Url { get; set; }
        public string Title { get; set; }
    }

    public class PosterItem
    {
    }
}
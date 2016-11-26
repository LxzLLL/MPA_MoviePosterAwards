using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPA_MoviePosterAwards.Web.Models
{
    public class CelebViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Name_En { get; set; }
        public string Aka { get; set; }
        public string Aka_En { get; set; }
        public string Gender { get; set; }
        public string Birth_Date { get; set; }
        public string Death_Date { get; set; }
        public string Born_Place { get; set; }
        public string Profession { get; set; }
        public string Family { get; set; }
        public string Douban { get; set; }
        public string IMDb { get; set; }
        public string Summary { get; set; }
        public string Avatar { get; set; }

        public CelebViewModel(Basic_Celebrity_Info celeb)
        {
            Id = celeb.Id.ToString();
            Name = celeb.Name;
            Name_En = celeb.Name_En;
            Aka = celeb.Aka;
            Aka_En = celeb.Aka_En;
            Gender = celeb.Gender ? "男" : "女";
            Birth_Date = celeb.Birth_Date;
            Death_Date = celeb.Death_Date;
            Born_Place = celeb.Born_Place;
            Profession = celeb.Profession;
            Family = celeb.Family;
            Douban = celeb.Douban;
            IMDb = celeb.IMDb;
            Summary = celeb.Summary;
            Avatar = celeb.Avatar.Large;
        }
    }
}
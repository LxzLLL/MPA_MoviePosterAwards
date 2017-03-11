using MPA_MoviePosterAwards.BLL;
using MPA_MoviePosterAwards.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPA_MoviePosterAwards.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var topPosters = Basic_Poster_BLL.GetList(string.Empty).OrderByDescending(p => p.Time).Take(20);
            List<NewestPosterViewModel> model = topPosters.Select(p => new NewestPosterViewModel
            {
                Url = p.Poster_M,
                ID = p.Id.ToString(),
                MovieID = p.Movie.ToString(),
                Time = p.Time.ToString("yyyy年MM月dd日 hh:mm:ss"),
                MovieTite = Basic_Movie_BLL.GetSingle(p.Movie).Title
            }).ToList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
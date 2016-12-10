using MPA_MoviePosterAwards.BLL;
using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.Model;
using MPA_MoviePosterAwards.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPA_MoviePosterAwards.Web.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index(string id)
        {
            if (id.IsBlank())
                return null;
            if (!Basic_Movie_BLL.Exist(Guid.Parse(id)))
            {
                return HttpNotFound();
            }
            Basic_Movie_Info _basic_movie = Basic_Movie_BLL.GetSingle(Guid.Parse(id));
            MovieViewModel movie = new MovieViewModel(_basic_movie);
            return View(movie);
        }

        // GET: Movie/List
        public ActionResult List()
        {
            List<MovieViewModel> movies = new List<MovieViewModel>();
            foreach (var item in Basic_Movie_BLL.GetList(string.Empty))
            {
                MovieViewModel movie = new MovieViewModel(item);
                movies.Add(movie);
            }
            return View(movies);
        }
    }
}
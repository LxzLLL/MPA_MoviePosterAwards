﻿using MPA_MoviePosterAwards.BLL;
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

        // GET: Movie/List/
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

        // GET: Movie/Create/
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create/
        [HttpPost]
        public ActionResult Create(string douban)
        {
            foreach (var item in douban.Replace("\r\n", " ").Replace("\r", " ").Replace("\n", " ").Split(' '))
            {
                var result = MovieManager.InsertMovie(item);

                ModelState.AddModelError("", result.Error);
            }
            return View();
        }

        // GET: Movie/Update/
        public ActionResult Update(string id, string returnurl)
        {
            Basic_Movie_Info movie = Basic_Movie_BLL.GetSingle(Guid.Parse(id));
            Basic_Movie_BLL.Delete(movie.Id);
            MovieManager.InsertMovie(movie.Douban, movie.Id);
            return Redirect(returnurl);
        }

        // GET: Movie/Delete/
        public ActionResult Delete(string id, string returnurl)
        {
            Basic_Movie_Info movie = Basic_Movie_BLL.GetSingle(Guid.Parse(id));
            Basic_Movie_BLL.Delete(movie.Id);
            return Redirect(returnurl);
        }
    }
}
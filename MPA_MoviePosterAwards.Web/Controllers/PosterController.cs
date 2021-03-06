﻿using MPA_MoviePosterAwards.BLL;
using MPA_MoviePosterAwards.Model;
using MPA_MoviePosterAwards.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MPA_MoviePosterAwards.Web.Controllers
{
    public class PosterController : Controller
    {
        // GET: Poster
        public ActionResult Index(string id)
        {
            var poster = Basic_Poster_BLL.GetSingle(Guid.Parse(id));

            PosterIndexViewModel model = new PosterIndexViewModel();

            model.Posters = Basic_Poster_BLL.GetList(poster.Movie).Select(p => new PosterViewModel
            {
                Id = p.Id.ToString(),
                Movie = p.Movie.ToString(),
                Poster = p.Poster,
                Poster_M = p.Poster_M,
                Poster_S = p.Poster_S,
                Poster_XS = p.Poster_XS,
                Time = p.Time.ToString("yyyy年MM月dd日 hh:mm:ss"),
                Active = p.Id == poster.Id,
                Height = p.Height,
                Width = p.Width
            }).ToList();
            model.ActivePoster = model.Posters.FirstOrDefault(p => p.Active);
            model.User = new UserViewModel(Basic_User_BLL.GetSingle(poster.User));
            model.Movie = new MovieViewModel(Basic_Movie_BLL.GetSingle(poster.Movie));

            return View(model);
        }

        // GET: Poster/Create/
        public ActionResult Create(string movie)
        {
            PosterViewModel poster = new PosterViewModel();
            poster.Movie = movie;
            return View(poster);
        }

        // POST: Poster/Create/
        [HttpPost]
        public ActionResult Create(PosterViewModel model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (file != null && file.ContentLength > 0)
            {
                string filename = PosterManager.GetPosterName(model.Movie);
                var poster = Path.Combine(Request.MapPath("~/Resources/Poster/"), filename + Path.GetExtension(file.FileName));
                var poster_m = Path.Combine(Request.MapPath("~/Resources/Poster_M/"), filename + "_M" + Path.GetExtension(file.FileName));
                var poster_s = Path.Combine(Request.MapPath("~/Resources/Poster_S/"), filename + "_S" + Path.GetExtension(file.FileName));
                var poster_xs = Path.Combine(Request.MapPath("~/Resources/Poster_XS/"), filename + "_XS" + Path.GetExtension(file.FileName));
                file.SaveAs(poster);
                model.Poster = Path.GetFileName(poster);
                model.Poster_M = Path.GetFileName(poster_m);
                model.Poster_S = Path.GetFileName(poster_s);
                model.Poster_XS = Path.GetFileName(poster_xs);

                Bitmap bmpSrc = new Bitmap(poster);
                Compress(bmpSrc, (int)((double)bmpSrc.Width / bmpSrc.Height * 800), 800, poster_m);
                Compress(bmpSrc, (int)((double)bmpSrc.Width / bmpSrc.Height * 400), 400, poster_s);
                Compress(bmpSrc, (int)((double)bmpSrc.Width / bmpSrc.Height * 200), 200, poster_xs);
                Basic_Poster_Info _basic_poster = new Basic_Poster_Info
                {
                    Id = Guid.NewGuid(),
                    Movie = Guid.Parse(model.Movie),
                    User = Guid.Parse(Common.CookieHelper.GetCookie("userid")),
                    Poster = model.Poster,
                    Poster_M = model.Poster_M,
                    Poster_S = model.Poster_S,
                    Poster_XS = model.Poster_XS,
                    Height = bmpSrc.Height,
                    Width = bmpSrc.Width,
                    Time = DateTime.Now
                };
                Basic_Poster_BLL.Insert(_basic_poster);
            }
            else
            {
                ModelState.AddModelError("", "请选择一张图片");
            }
            return View(model);
        }

        public void Compress(Bitmap source, int width, int height, string filename)
        {
            Bitmap bmpDest = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmpDest);

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            Rectangle rectDest = new Rectangle(0, 0, bmpDest.Width, bmpDest.Height);
            Rectangle rectSrc = new Rectangle(0, 0, source.Width, source.Height);
            g.DrawImage(source, rectDest, rectSrc, GraphicsUnit.Pixel);
            bmpDest.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);//带上Jpeg压缩格式，可以让图片进行压缩

            g.Dispose();
            bmpDest.Dispose();
        }

    }
}
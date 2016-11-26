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
    public class CelebController : Controller
    {
        // GET: Celeb
        public ActionResult Index(string id)
        {
            if (id.IsBlank())
                return null;
            if (!Basic_Celebrity_BLL.Exist(Guid.Parse(id)))
            {
                return HttpNotFound();
            }
            Basic_Celebrity_Info _basic_celeb = Basic_Celebrity_BLL.GetSingle(Guid.Parse(id));
            CelebViewModel movie = new CelebViewModel(_basic_celeb);
            return View(movie);
        }
    }
}
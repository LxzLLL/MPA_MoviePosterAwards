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
                return RedirectToAction("Index", "Home");
            if (!Basic_Celebrity_BLL.Exist(Guid.Parse(id)))
            {
                return HttpNotFound();
            }
            Basic_Celebrity_Info _basic_celeb = Basic_Celebrity_BLL.GetSingle(Guid.Parse(id));
            CelebViewModel movie = new CelebViewModel(_basic_celeb);
            return View(movie);
        }

        // GET: Celeb/List/
        public ActionResult List()
        {
            List<CelebViewModel> celebs = new List<CelebViewModel>();
            foreach (var item in Basic_Celebrity_BLL.GetList(string.Empty))
            {
                CelebViewModel celeb = new CelebViewModel(item);
                celebs.Add(celeb);
            }
            return View(celebs);
        }

        // GET: Celeb/Update/
        public ActionResult Update(string id, string returnurl)
        {
            Basic_Celebrity_Info celeb = Basic_Celebrity_BLL.GetSingle(Guid.Parse(id));
            Basic_Celebrity_BLL.Delete(celeb.Id);
            CelebManager.InsertCeleb(celeb.Douban, celeb.Id);
            return Redirect(returnurl);
        }

        // GET: Celeb/Delete/
        public ActionResult Delete(string id, string returnurl)
        {
            Basic_Celebrity_Info celeb = Basic_Celebrity_BLL.GetSingle(Guid.Parse(id));
            Basic_Celebrity_BLL.Delete(celeb.Id);
            return Redirect(returnurl);
        }
    }
}
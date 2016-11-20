using System.Web;
using System.Web.Mvc;

namespace MPA_MoviePosterAwards.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

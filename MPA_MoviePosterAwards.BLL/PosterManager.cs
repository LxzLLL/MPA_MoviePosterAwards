using MPA_MoviePosterAwards.Common;
using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.BLL
{
    public class PosterManager
    {
        public static void Create(string movie, string poster, string posters, string posterxs)
        {
            Basic_Poster_Info _basic_poster = new Basic_Poster_Info();
            _basic_poster.Movie = Guid.Parse(movie);
            _basic_poster.Poster = poster;
            _basic_poster.Poster_S = posters;
            _basic_poster.Poster_XS = posterxs;
            _basic_poster.Id = Guid.NewGuid();
            _basic_poster.User = Guid.Parse(CookieHelper.GetCookie("userid"));
            Basic_Poster_BLL.Insert(_basic_poster);
        }

        /// <summary>
        /// 为新添加的海报生成文件名称
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public static string GetPosterName(string movie)
        {
            Basic_Movie_Info movieinfo = Basic_Movie_BLL.GetSingle(Guid.Parse(movie));
            int postercount = Basic_Poster_BLL.GetList(movieinfo.Id).Count() + 1;

            string postername = string.Empty;

            if (!movieinfo.Title_En.HasChinese())
                postername = string.Join("_", movieinfo.Title_En.Split(' '));
            else
            {
                string data = movieinfo.Title;
                char[] charArr = data.ToCharArray();
                StringBuilder sb = new StringBuilder();
                foreach (char c in charArr)
                {
                    if (c != ' ')
                    {
                        sb.Append(c);
                        sb.Append("_");
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }
                postername = sb.ToString();

            }
            return postername + "_" + postercount + "_" + movieinfo.Id.ToString("N");
        }
    }
}

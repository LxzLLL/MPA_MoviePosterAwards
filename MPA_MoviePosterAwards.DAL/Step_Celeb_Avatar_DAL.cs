using MPA_MoviePosterAwards.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPA_MoviePosterAwards.DAL
{
    public class Step_Celeb_Avatar_DAL
    {
        public static Step_Celeb_Avatar_Info ToModel(Step_Celeb_Avatar avatar)
        {
            Step_Celeb_Avatar_Info info = new Step_Celeb_Avatar_Info();

            info.Id = avatar.Id;
            info.Celeb = (Guid)avatar.Celeb;
            info.Small = avatar.Small;
            info.Large = avatar.Large;
            info.Medium = avatar.Medium;

            return info;
        }

        public static Step_Celeb_Avatar ToDatabase(Step_Celeb_Avatar_Info info)
        {
            Step_Celeb_Avatar avatar = new Step_Celeb_Avatar();

            avatar.Id = info.Id;
            avatar.Celeb = info.Celeb;
            avatar.Small = info.Small;
            avatar.Large = info.Large;
            avatar.Medium = info.Medium;

            return avatar;
        }
    }
}
